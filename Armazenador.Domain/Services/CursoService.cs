using Armazenador.Domain.Entities;
using Armazenador.Domain.Entities.Requests;
using Armazenador.Domain.Entities.Views;
using Armazenador.Domain.Interfaces.Repositories;
using Armazenador.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Armazenador.Domain.Services
{
    public class CursoService : BaseService, ICursoService
    {
        private readonly ICursoRepository repositorio;

        public CursoService(ICursoRepository repositorio)
        {
            this.repositorio = repositorio;
        }

        public IEnumerable<CursoView> ListarTodos()
        {
            return repositorio.ListarTodos();
        }

        public Curso ObterPorId(int id)
        {
            return repositorio.ObterPorId(id);
        }

        public Curso Incluir(CursoRequest request, string usuarioCadastro)
        {
            var novoCurso = new Curso(request.Descricao, request.CargaHoraria, request.Valor, usuarioCadastro);
            ValidarCurso(novoCurso);
            if (Validar)
            {
                bool cursoExistente = repositorio.VerificaCursoExistente(request.Descricao);
                if (!cursoExistente)
                {
                    return repositorio.Incluir(novoCurso);
                }
                else
                    AdicionarNotificacao("curso", "Curso já existe.");
            }
            return null;
        }

        public Curso Alterar(CursoRequest request, string usuarioCadastro)
        {
            var cursoExistente = repositorio.ObterPorId(request.Id);
            if (cursoExistente != null)
            {
                cursoExistente.AlterarCargaHoraria(request.CargaHoraria);
                cursoExistente.AlterarValor(request.Valor);
                ValidarCurso(cursoExistente);
                if (Validar)
                {
                    return repositorio.Alterar(cursoExistente);
                }
            }
            else
                AdicionarNotificacao("curso", Mensagem.Curso);
            return null;
        }

        public void Inativar(int id, string usuario)
        {
            if (id <= 0)
                AdicionarNotificacao("curso", Mensagem.Curso);
            var cursoExistente = repositorio.ObterPorId(id);
            if (cursoExistente != null)
            {
                cursoExistente.Inativar(usuario);
                repositorio.Alterar(cursoExistente);
            }
            else
                AdicionarNotificacao("curso", "Não é possível inativar. Pois não existe curso");
        }

        private void ValidarCurso(Curso curso)  
        {
            if (!curso.Validar)
                AdicionarNotificacao(curso.Notificacoes);
        }
    }
}
