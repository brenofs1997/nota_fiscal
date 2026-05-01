using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServicoEstoque.Api.Controllers;
using ServicoEstoque.Core.Interfaces.Applications;
using ServicoEstoque.Core.Interfaces.Notifications;
using ServicoEstoque.Core.Models.InputModels;
using ServicoEstoque.Core.Models.ViewModel;
using Xunit;

namespace ServicoEstoque.Tests.Controllers
{
    public class ProdutoControllerTests
    {
        private readonly Mock<ICadastrarProdutoApplication> _cadastrarAppMock;
        private readonly Mock<IBuscarProdutosApplication> _buscarAppMock;
        private readonly Mock<IBuscarProdutoPorIdApplication> _buscarPorIdAppMock;
        private readonly Mock<IAtualizarProdutoApplication> _atualizarAppMock;
        private readonly Mock<IAtualizarSaldoProdutoApplication> _atualizarSaldoAppMock;
        private readonly Mock<IDeletarProdutoApplication> _deletarAppMock;
        private readonly Mock<INotifier> _notifierMock;
        private readonly ProdutoController _controller;

        public ProdutoControllerTests()
        {
            _cadastrarAppMock = new Mock<ICadastrarProdutoApplication>();
            _buscarAppMock = new Mock<IBuscarProdutosApplication>();
            _buscarPorIdAppMock = new Mock<IBuscarProdutoPorIdApplication>();
            _atualizarAppMock = new Mock<IAtualizarProdutoApplication>();
            _atualizarSaldoAppMock = new Mock<IAtualizarSaldoProdutoApplication>();
            _deletarAppMock = new Mock<IDeletarProdutoApplication>();
            _notifierMock = new Mock<INotifier>();

            _controller = new ProdutoController(
                _cadastrarAppMock.Object,
                _buscarAppMock.Object,
                _buscarPorIdAppMock.Object,
                _atualizarAppMock.Object,
                _atualizarSaldoAppMock.Object,
                _deletarAppMock.Object,
                _notifierMock.Object
            );
        }

        [Fact]
        public async Task CadastrarAsync_DeveRetornarOk_QuandoCadastroForBemSucedido()
        {
            var input = new ProdutoInputModel("PROD01", "Teclado", 10);
            var idGerado = Guid.NewGuid();

            _cadastrarAppMock
                .Setup(app => app.CadastrarAsync(It.IsAny<ProdutoInputModel>()))
                .ReturnsAsync(idGerado);

            _notifierMock.Setup(n => n.TemNotificacoes()).Returns(false);

            // Act 
            var result = await _controller.CadastrarAsync(input);

            // Assert
      
            var actionResult = result.Should().BeOfType<OkObjectResult>().Subject;

           
            actionResult.Value.Should().Be(idGerado);

            _cadastrarAppMock.Verify(app => app.CadastrarAsync(It.IsAny<ProdutoInputModel>()), Times.Once);
        }

        [Fact]
        public async Task BuscarPorId_DeveRetornarProduto_QuandoIdExistir()
        {
            // Arrange
            var idExistente = Guid.NewGuid();
            var produtoFake = new ProdutoViewModel(idExistente, "PROD001", "Teclado Mecânico", 50);

          
            _buscarPorIdAppMock
                .Setup(app => app.BuscarPorIdAsync(idExistente))
                .ReturnsAsync(produtoFake);

            // Act
            var result = await _controller.BuscarPorIdAsync(idExistente);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(produtoFake);
        }

        [Fact]
        public async Task BuscarPorIdAsync_DeveRetornarNotFound_QuandoProdutoNaoExistir()
        {
            // Arrange
            var idInexistente = Guid.NewGuid();

            _buscarPorIdAppMock
                .Setup(app => app.BuscarPorIdAsync(idInexistente))
                .ReturnsAsync((ProdutoViewModel?)null);

            // Act
            var result = await _controller.BuscarPorIdAsync(idInexistente);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}