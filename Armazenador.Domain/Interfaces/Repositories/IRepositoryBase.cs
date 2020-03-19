using Armazenador.Domain.Entities;
using System.Collections.Generic;

namespace Armazenador.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<T, TView> where T : Entity where TView : class
    {
        IEnumerable<TView> ListarTodos();
        T ObterPorId(int id);
        T Incluir(T curso);
        T Alterar(T curso);
        //void Inativar(int id, string usuario);
    }
}
