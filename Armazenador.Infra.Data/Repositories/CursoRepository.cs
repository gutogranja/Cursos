using Armazenador.Domain.Entities;
using Armazenador.Domain.Entities.Views;
using Armazenador.Domain.Interfaces.Repositories;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace Armazenador.Infra.Data.Repositories
{
    public class CursoRepository : RepositoryBase<Curso, CursoView>, ICursoRepository
    {
        public override IEnumerable<CursoView> ListarTodos()
        {
            return cn.Query<CursoView>("SELECT * FROM Cursos WHERE Ativo = 1 ORDER BY Descricao").ToList();
        }

        public bool VerificaCursoExistente(string descricaoCurso)
        {
            return cn.Query<int>($"SELECT TOP 1 1 FROM Cursos WHERE Descricao = '{descricaoCurso}'").Any();
        }
    }
}
