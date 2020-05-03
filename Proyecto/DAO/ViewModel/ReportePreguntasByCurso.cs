using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class ReportePreguntasByCurso
    {
        public string Pregunta { get; set; }
        public float PorcentajeMuyBueno { get; set; }

        public float PorcentajeBueno { get; set; }

        public float PorcentajeRegular { get; set; }

        public float PorcentajeMalo { get; set; }
        public float PorcentajeMuyMalo { get; set; }
       
    }
}
