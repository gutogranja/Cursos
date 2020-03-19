using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using Armazenador.Domain.Entities.Views;
using Armazenador.Domain.Interfaces.Services;
using Armazenador.Domain.Services;
using Armazenador.Infra.Data.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Armazenador.Domain.Entities.Requests;

namespace Armazenador.Apresentation.Wpf.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        IDialogCoordinator dialog;
        private readonly ICursoService cursoService;

        public DelegateCommand IncluirCommand { get; set; }
        public DelegateCommand AlterarCommand { get; set; }
        public DelegateCommand InativarCommand { get; set; }
        public DelegateCommand LimparTelaCommand { get; set; }
        public ProgressDialogController Progresso { get; set; }

        private bool _modoEdicao = false;
        public bool ModoEdicao
        {
            get { return _modoEdicao; }
            set
            {
                SetProperty(ref _modoEdicao, value);
            }
        }

        private bool _editarCurso = true;
        public bool EditarCurso
        {
            get { return _editarCurso; }
            set
            {
                SetProperty(ref _editarCurso, value);
            }
        }

        private CursoRequest _request = new CursoRequest();
        public CursoRequest Request
        {
            get { return _request; }
            set
            {
                SetProperty(ref _request, value);
            }
        }

        private List<CursoView> _listaCurso;
        public List<CursoView> ListaCurso
        {
            get { return _listaCurso; }
            set { SetProperty(ref _listaCurso, value); }
        }

        private CursoView _view = new CursoView();
        public CursoView View
        {
            get { return _view; }
            set
            {
                SetProperty(ref _view, value);
                ModoEdicao = _view?.Id > 0;
                EditarCurso = !_modoEdicao;
            }
        }

        public MainWindowViewModel(IDialogCoordinator dialog)
        {
            this.dialog = dialog;
            cursoService = new CursoService(new CursoRepository());
            IncluirCommand = new DelegateCommand(Incluir, () => !ModoEdicao).ObservesProperty(() => ModoEdicao);
            AlterarCommand = new DelegateCommand(Alterar, () => ModoEdicao).ObservesProperty(() => ModoEdicao);
            InativarCommand = new DelegateCommand(Inativar, () => ModoEdicao).ObservesProperty(() => ModoEdicao);
            LimparTelaCommand = new DelegateCommand(BuscarCursos);
            BuscarCursos();
        }

        private async void Incluir()
        {
            Request.Descricao = View.Descricao;
            Request.CargaHoraria = View.CargaHoraria;
            Request.Valor = View.Valor;
            var cursoCriado = cursoService.Incluir(Request, "Carlosg");
            if (!cursoService.Validar)
            {
                var linq = cursoService.Notificacoes.Select(msg => msg.Mensagem);
                await this.dialog.ShowMessageAsync(this, "Atenção", string.Join("\r\n", linq));
                cursoService.LimparNotificacoes();
            }
            if (cursoCriado != null)
            {
                Progresso = await dialog.ShowProgressAsync(this, "Progresso", "Incluindo dados do curso. Aguarde...");
                Progresso.SetIndeterminate();
                var t = Task.Factory.StartNew(() => { BuscarCursos(); });
                await t;
                await Progresso?.CloseAsync();
                await this.dialog.ShowMessageAsync(this, "Atenção", "Curso cadastrado com sucesso !!!");
                Limpar();
            }
        }

        private async void Alterar()
        {
            if (View != null && View.Id > 0)
            {
                var cursoExistente = cursoService.ObterPorId(View.Id);
                if (cursoExistente != null)
                {
                    Request.Id = View.Id;
                    Request.CargaHoraria = View.CargaHoraria;
                    Request.Valor = View.Valor;
                    cursoExistente = cursoService.Alterar(Request, "Carlosg");
                    if (cursoService.Validar)
                    {
                        Progresso = await dialog.ShowProgressAsync(this, "Progresso", "Alterando dados do curso. Aguarde...");
                        Progresso.SetIndeterminate();
                        await Progresso?.CloseAsync();
                        await this.dialog.ShowMessageAsync(this, "Atenção", "Curso alterado com sucesso !!!");
                        Limpar();
                    }
                    else
                    {
                        await this.dialog.ShowMessageAsync(this, "Atenção", string.Join("\r\n", cursoService.Notificacoes.Select(s => s.Mensagem)));
                        cursoService.LimparNotificacoes();
                    }
                    BuscarCursos();
                }
            }
        }

        private async void Inativar()   
        {
            if (View != null && View.Id > 0)
            {
                var cursoExistente = cursoService.ObterPorId(View.Id);
                if (cursoExistente != null)
                {
                    Progresso = await dialog.ShowProgressAsync(this, "Progresso", "Inativando curso. Aguarde...");
                    Progresso.SetIndeterminate();
                    var t = Task.Factory.StartNew(() => { cursoService.Inativar(View.Id,"Carlosg"); });
                    await t;
                    await Progresso?.CloseAsync();
                    await this.dialog.ShowMessageAsync(this, "Atenção", "Curso inativado com sucesso !!!");
                    Limpar();
                    BuscarCursos();
                }
                else
                {
                    await this.dialog.ShowMessageAsync(this, "Atenção", string.Join("\r\n", cursoExistente.Notificacoes.Select(s => s.Mensagem)));
                    cursoService.LimparNotificacoes();
                }
            }
        }

        private void BuscarCursos()
        {
            ListaCurso = cursoService.ListarTodos().ToList();
        }

        private void Limpar()
        {
            View = new CursoView();
        }
    }
}
