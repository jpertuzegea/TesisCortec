using BLL.Enums;
using BLL.Utilidades;
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

        private TESISCortecEntities BD = null;

        public Bll_CalificacionDocenteCursoEstudiante()
        {
            BD = new TESISCortecEntities();
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
                    CalificacionDocenteCursoEstudiante.FechaEvaluacion = UtilitiesCommons.ObtenerHorayFechaActualLocal();

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

        public List<ReportePreguntasByCurso> ReporteCalificacionesByCuirsoId(int CursoId)
        {
            List<ReportePreguntasByCurso> Lista = new List<ReportePreguntasByCurso>();
            try
            {
                var Calificaciones = BD.CalificacionDocenteCursoEstudiante.Where(x => x.CursoId == CursoId).ToList();

                foreach (var item in Calificaciones)
                {
                    ReportePreguntasByCurso ReportePreguntasByCurso = new ReportePreguntasByCurso();
                    ReportePreguntasByCurso.Pregunta = item.PreguntasCalificacionCurso.Pregunta;
 
                    float gg = (float)((Calificaciones.Count(x => x.RespuestaPregunta == (byte)EnumRangoCalificacion.MuyBueno)) * 7) / 100;
                    ReportePreguntasByCurso.PorcentajeMuyBueno = gg;
                    ReportePreguntasByCurso.PorcentajeBueno = 10;
                    ReportePreguntasByCurso.PorcentajeRegular = 10;
                    ReportePreguntasByCurso.PorcentajeMalo = 10;
                    ReportePreguntasByCurso.PorcentajeMuyMalo = 10;

                    Lista.Add(ReportePreguntasByCurso);
                }
                return (Lista);
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return null;
            }
        }


    }
}
