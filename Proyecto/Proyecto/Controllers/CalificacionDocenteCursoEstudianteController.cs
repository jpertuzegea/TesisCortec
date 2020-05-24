using BLL;
using BLL.Enums;
using DAO.ViewModel;
using Proyecto.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class CalificacionDocenteCursoEstudianteController : Controller
    {
        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Calificaciones_Del_Curso_y_Docente)]
        public ActionResult ReporteCalificaciones(int CursoId)
        {
           //   Bll_Login.VerificarSesionActiva();
            Bll_CalificacionDocenteCursoEstudiante Bll_CalificacionDocenteCursoEstudiante = new Bll_CalificacionDocenteCursoEstudiante();
            List<ReportePreguntasByCurso> Lista = Bll_CalificacionDocenteCursoEstudiante.ReporteCalificacionesByCuirsoId(CursoId);
            return View(Lista);
        }
    }
}