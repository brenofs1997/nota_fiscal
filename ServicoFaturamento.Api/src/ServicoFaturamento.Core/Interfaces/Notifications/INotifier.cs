using ServicoFaturamento.Core.Models.ViewModel;
using System.Net;

namespace ServicoFaturamento.Core.Interfaces.Notifications
{
    public interface INotifier
    {
        void LimparNotificacoes();
        bool TemNotificacoes();
        List<NotificacaoViewModel> ObterNotificacoes();
        void Handle(string mensagem, HttpStatusCode statusCode);
    }
}
