using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class PreguntasRespuestasCalificacionModel
    {
        public int PreguntasCalificacionCursoId { get; set; }
        public string Pregunta { get; set; } 
        public int CursoId { get; set; }  
        public byte RespuestaPregunta { get; set; } 
         
    }

}
