using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ServicoFaturamento.Application.Applications;
using ServicoFaturamento.Core.Interfaces.Notifications;
using ServicoFaturamento.Core.Interfaces.Repositories;
using ServicoFaturamento.Core.Interfaces.Services;
using ServicoFaturamento.Core.Entities; 
using System.Net;
using System.Timers;
using Xunit;
using ServicoFaturamento.Core.Models.InputModels;

namespace ServicoFaturamento.Tests.Application
{
    public class FecharNotaFiscalApplicationTests
    {
        private readonly Mock<INotaFiscalRepository> _repositoryMock;
        private readonly Mock<IEstoqueIntegrationService> _estoqueServiceMock;
        private readonly Mock<INotifier> _notifierMock;
        private readonly Mock<ILogger<FecharNotaFiscalApplication>> _loggerMock;
        private readonly FecharNotaFiscalApplication _appService;

        public FecharNotaFiscalApplicationTests()
        {
            _repositoryMock = new Mock<INotaFiscalRepository>();
            _estoqueServiceMock = new Mock<IEstoqueIntegrationService>();
            _notifierMock = new Mock<INotifier>();
            _loggerMock = new Mock<ILogger<FecharNotaFiscalApplication>>();

            _appService = new FecharNotaFiscalApplication(
                _repositoryMock.Object,
                _estoqueServiceMock.Object,
                _notifierMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        [Trait("Categoria", "Tratamento de Falhas")]
        public async Task FecharAsync_DeveNotificarErro_QuandoServicoEstoqueRecusarAtualizacao()
        {
            // Arrange
            var notaId = Guid.NewGuid();
            var notaFiscal = new NotaFiscal
            {
                Id = notaId,
                Status = "Aberta",
                Itens = new List<ItensNotaFiscal> { new ItensNotaFiscal(Guid.NewGuid(), 10) }
            };

            _repositoryMock.Setup(r => r.BuscarPorIdAsync(notaId)).ReturnsAsync(notaFiscal);

            _estoqueServiceMock.Setup(s => s.AtualizarSaldoProdutosAsync(It.IsAny<List<BaixaEstoqueInputModel>>()))
                .ReturnsAsync(false);

            // Act
            await _appService.FecharAsync(notaId);

            // Assert
            _notifierMock.Verify(n => n.Handle(
                It.Is<string>(s => s.Contains("O Serviço de Estoque rejeitou")),
                HttpStatusCode.UnprocessableEntity),
                Times.Once);

            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
            notaFiscal.Status.Should().Be("Aberta");
        }

        [Fact]
        [Trait("Categoria", "Tratamento de Falhas")]
        public async Task FecharAsync_DeveTratarExcecao_QuandoMicrosservicoEstoqueEstiverIndisponivel()
        {
            // Arrange
            var notaId = Guid.NewGuid();
            var notaFiscal = new NotaFiscal { Id = notaId, Status = "Aberta", Itens = new List<ItensNotaFiscal> { new ItensNotaFiscal(Guid.NewGuid(), 5) } };

            _repositoryMock.Setup(r => r.BuscarPorIdAsync(notaId)).ReturnsAsync(notaFiscal);

            
            _estoqueServiceMock.Setup(s => s.AtualizarSaldoProdutosAsync(It.IsAny<List<BaixaEstoqueInputModel>>()))
                .ThrowsAsync(new Exception("Erro de conexão com o microsserviço de Estoque."));

            // Act
            await _appService.FecharAsync(notaId);

            // Assert
            _notifierMock.Verify(n => n.Handle(
                It.Is<string>(s => s.Contains("Erro de conexão")),
                HttpStatusCode.BadRequest),
                Times.Once);

         
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Critical,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Erro fatal")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);

     
            notaFiscal.Status.Should().Be("Aberta");
        }
    }
}