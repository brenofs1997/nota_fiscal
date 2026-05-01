using ServicoFaturamento.Core.Models.ViewModel;
using ServicoFaturamento.Core.Interfaces.Notifications;
using System.Net;

namespace ServicoFaturamento.Application.Notifications
{
    public class Notifier : INotifier
    {
        private readonly List<NotificacaoViewModel> _notificacoes;

        public Notifier()
        {
            _notificacoes = [];
        }

        public void LimparNotificacoes()
        {
            if (TemNotificacoes())
            {
                _notificacoes.Clear();
            }
        }

        public bool TemNotificacoes()
        {
            return _notificacoes.Count > 0;
        }

        public List<NotificacaoViewModel> ObterNotificacoes()
        {
            return _notificacoes;
        }

        public void Handle(string mensagem, HttpStatusCode statusCode)
        {
            _notificacoes.Add(new(mensagem, statusCode));
        }
    }
}
