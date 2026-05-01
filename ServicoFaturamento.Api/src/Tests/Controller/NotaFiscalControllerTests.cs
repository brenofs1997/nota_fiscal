using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServicoFaturamento.Api.Controllers;
using ServicoFaturamento.Application.Notifications;
using ServicoFaturamento.Core.Interfaces.Applications;
using ServicoFaturamento.Core.Interfaces.Notifications;
using ServicoFaturamento.Core.Models.ViewModel;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ServicoFaturamento.Tests.Api
{
    public class NotaFiscalControllerTests
    {
        private readonly Mock<ICadastrarNotaFiscalApplication> _cadastrarMock;
        private readonly Mock<IBuscarNotasFiscaisApplication> _buscarMock;
        private readonly Mock<IBuscarNotaFiscalPorIdApplication> _buscarPorIdMock;
        private readonly Mock<IFecharNotaFiscalApplication> _fecharMock;
        private readonly Mock<INotifier> _notifierMock;
        private readonly NotaFiscalController _controller;

        public NotaFiscalControllerTests()
        {
            _cadastrarMock = new Mock<ICadastrarNotaFiscalApplication>();
            _buscarMock = new Mock<IBuscarNotasFiscaisApplication>();
            _buscarPorIdMock = new Mock<IBuscarNotaFiscalPorIdApplication>();
            _fecharMock = new Mock<IFecharNotaFiscalApplication>();
            _notifierMock = new Mock<INotifier>();

            _controller = new NotaFiscalController(
                _cadastrarMock.Object,
                _buscarMock.Object,
                _buscarPorIdMock.Object,
                _fecharMock.Object,
                _notifierMock.Object
            );
        }

        [Fact]
        public async Task FecharAsync_DeveRetornarBadRequest_QuandoApplicationIdentificarFalha()
        {
            var notaId = Guid.NewGuid();

            _notifierMock.Setup(n => n.TemNotificacoes()).Returns(true);
            _notifierMock.Setup(n => n.ObterNotificacoes()).Returns(new List<NotificacaoViewModel>
            {
                new NotificacaoViewModel("Erro de teste", HttpStatusCode.UnprocessableEntity)
            });

            var result = await _controller.FecharAsync(notaId);

            var jsonResult = result.Should().BeOfType<JsonResult>().Subject;
            jsonResult.StatusCode.Should().Be((int)HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task FecharAsync_DeveRetornarOk_QuandoSucessoNoFechamento()
        {
            var notaId = Guid.NewGuid();

            _notifierMock.Setup(n => n.TemNotificacoes()).Returns(false);
            _notifierMock.Setup(n => n.ObterNotificacoes()).Returns(new List<NotificacaoViewModel>());

            var result = await _controller.FecharAsync(notaId);

            result.Should().BeOfType<NoContentResult>();
        }
    }
}