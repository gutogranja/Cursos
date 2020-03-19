using System;
using System.Collections.Generic;
using System.Text;

namespace Armazenador.Domain.Entities.Views
{
    public class CursoView
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int CargaHoraria { get; set; }
        public double Valor { get; set; }
    }
}
