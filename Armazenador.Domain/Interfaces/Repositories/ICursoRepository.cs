using Armazenador.Domain.Entities;
using Armazenador.Domain.Entities.Views;

namespace Armazenador.Domain.Interfaces.Repositories
{
    public interface ICursoRepository : IRepositoryBase<Curso,CursoView>
    {
        bool VerificaCursoExistente(string descricao);
    }
}
