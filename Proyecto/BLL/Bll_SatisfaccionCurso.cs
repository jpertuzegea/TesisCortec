using DAO;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Bll_SatisfaccionCurso
    {

        private TESISCortecEntities BD = null;

        public Bll_SatisfaccionCurso()
        {
            BD = new TESISCortecEntities();
        }


        public ListaPreguntas ObtenerPreguntasSatisfaccion()
        {
            ListaPreguntas ListaPreguntas = new ListaPreguntas(); 
            List<PreguntasRespuestasCalificacionModel> List = new List<PreguntasRespuestasCalificacionModel>();

            try
            {
                List<PreguntasCalificacionCurso> lista = BD.PreguntasCalificacionCurso.ToList();

                if (lista != null)
                {
                    foreach (var item in lista)
                    {
                        PreguntasRespuestasCalificacionModel PreguntasRespuestasCalificacionModel = new PreguntasRespuestasCalificacionModel();
                        PreguntasRespuestasCalificacionModel.PreguntasCalificacionCursoId = item.PreguntasCalificacionCursoId;
                        PreguntasRespuestasCalificacionModel.Pregunta = item.Pregunta;
                        // PreguntasRespuestasCalificacionModel.CursoId = item.PreguntasCalificacionCursoId;
                        // PreguntasRespuestasCalificacionModel.RespuestaPregunta = ;
                        List.Add(PreguntasRespuestasCalificacionModel);
                    }
                }
                ListaPreguntas.ListaRespuestas = List;
                return ListaPreguntas;
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return ListaPreguntas;
            }
        }




    }
}
