using Armazenador.Domain.Helpers;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace Armazenador.Domain.Interfaces.Notifications   
{
    public interface INotificationService
    {
        [Write(false)]
        IReadOnlyCollection<Notification> Notificacoes { get; } 
        [Write(false)]
        bool Validar { get; }
        void AdicionarNotificacao(IReadOnlyCollection<Notification> notificacoes);
        void LimparNotificacoes();
    }
}
