using System;
using System.Collections.Generic;
using System.Text;

namespace Armazenador.Domain.Entities.Requests
{
    public class CursoRequest
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int CargaHoraria { get; set; }
        public double Valor { get; set; }
    }
}
