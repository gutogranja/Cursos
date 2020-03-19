using Dapper.Contrib.Extensions;

namespace Armazenador.Domain.Entities
{
    [Table("Cursos")]
    public class Curso : Entity
    {
        private Curso() : base("")
        {
        }

        public string Descricao { get; set; }
        public int CargaHoraria { get; set; }
        public double Valor { get; set; }

        public Curso(string descr, int carga, double valor, string usuarioCadastro) : base(usuarioCadastro)
        {
            Descricao = descr;
            CargaHoraria = carga;
            Valor = valor;
            if (string.IsNullOrEmpty(descr))
                AdicionarNotificacao("curso", Mensagem.Descricao);
            if (carga <= 0)
                AdicionarNotificacao("curso", Mensagem.CargaHoraria);
            if (valor <= 0)
                AdicionarNotificacao("curso", Mensagem.Valor);
        }

        public void AlterarCargaHoraria(int carga)
        {
            if (carga <= 0)
                AdicionarNotificacao("curso", Mensagem.CargaHoraria);
            else
                CargaHoraria = carga;
        }

        public void AlterarValor(double valor)
        {
            if (valor <= 0)
                AdicionarNotificacao("curso", "Valor do curso é obrigatório e não pode ser zero");
            else
                Valor = valor;
        }
    }
}
