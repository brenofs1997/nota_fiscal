using ServicoEstoque.Core.Models.ViewModel;
using System.Net;

namespace ServicoEstoque.Core.Interfaces.Notifications
{
    public interface INotifier
    {
        void LimparNotificacoes();
        bool TemNotificacoes();
        List<NotificacaoViewModel> ObterNotificacoes();
        void Handle(string mensagem, HttpStatusCode statusCode);
    }
}
