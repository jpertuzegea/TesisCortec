using BLL.Enums;
using DAO;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
    public class Bll_CalificacionDocenteCursoEstudiante
    {

        private TESIS_BD BD = null;

        public Bll_CalificacionDocenteCursoEstudiante()
        {
            BD = new TESIS_BD();
        }

        public bool GuardarCalificacion(ListaPreguntas ListaPreguntas)
        {
            try
            {
                int CursoId = ListaPreguntas.ListaRespuestas[0].CursoId;
                int EstudianteEnSesionId = (int)HttpContext.Current.Session["IdUsuarioTesis"];
                foreach (var item in ListaPreguntas.ListaRespuestas)
                {
                    CalificacionDocenteCursoEstudiante CalificacionDocenteCursoEstudiante = new CalificacionDocenteCursoEstudiante();
                    CalificacionDocenteCursoEstudiante.CursoId = item.CursoId;
                    CalificacionDocenteCursoEstudiante.EstudianteId = EstudianteEnSesionId;
                    CalificacionDocenteCursoEstudiante.PreguntasCalificacionCursoId = item.PreguntasCalificacionCursoId;
                    CalificacionDocenteCursoEstudiante.RespuestaPregunta = item.RespuestaPregunta;
                    CalificacionDocenteCursoEstudiante.FechaEvaluacion = DateTime.Now;

                    BD.CalificacionDocenteCursoEstudiante.Add(CalificacionDocenteCursoEstudiante);
                    BD.SaveChanges();
                }
                // Se deshabilita el boton de caliifcacion y se activa la opcion de descargar certificado para el estudiante 
                CursoEstudiante CursoEstudiante = BD.CursoEstudiante.Where(x => x.CursoId == CursoId && x.EstudianteId == EstudianteEnSesionId).FirstOrDefault();
                CursoEstudiante.EstadoEvaluacionCursoyDocente = (byte)EnumEstadoEvaluacionCursoyDocente.Realizada;
                BD.Entry(CursoEstudiante).State = EntityState.Modified;
                BD.SaveChanges();
                return true;
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return false;
            }
        }


    }
}
