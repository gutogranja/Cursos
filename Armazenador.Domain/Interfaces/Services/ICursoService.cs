using Armazenador.Domain.Entities;
using Armazenador.Domain.Entities.Requests;
using Armazenador.Domain.Entities.Views;
using Armazenador.Domain.Interfaces.Notifications;
using System.Collections.Generic;

namespace Armazenador.Domain.Interfaces.Services
{
    public interface ICursoService : INotificationService
    {
        IEnumerable<CursoView> ListarTodos();
        Curso ObterPorId(int id);
        Curso Incluir(CursoRequest curso, string usuarioCadastro);
        Curso Alterar(CursoRequest curso, string usuarioCadastro);
        void Inativar(int id, string usuarioCadastro); 
    }
}
