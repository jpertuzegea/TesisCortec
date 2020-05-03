using BLL;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class CalificacionDocenteCursoEstudianteController : Controller
    {

        public ActionResult ReporteCalificaciones(int CursoId)
        {
            Bll_Login.VerificarSesionActiva();
            Bll_CalificacionDocenteCursoEstudiante Bll_CalificacionDocenteCursoEstudiante = new Bll_CalificacionDocenteCursoEstudiante();
            List<ReportePreguntasByCurso> Lista = Bll_CalificacionDocenteCursoEstudiante.ReporteCalificacionesByCuirsoId(CursoId);
            return View(Lista);
        }
    }
}