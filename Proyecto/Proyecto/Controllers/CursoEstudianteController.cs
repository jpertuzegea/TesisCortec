using BLL;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class CursoEstudianteController : Controller
    {
        // GET: CursoEstudiante
        public ActionResult Index()
        {
            Bll_Login.VerificarSesionActiva();
            int EstudianteId = (int)System.Web.HttpContext.Current.Session["IdUsuarioTesis"];
            Bll_CursoEstudiante Bll_CursoEstudiante = new Bll_CursoEstudiante();
            List<CursoEstudiante> Lista = Bll_CursoEstudiante.ListarCursosActivosbyPersonaId(EstudianteId);

            return View(Lista);
        }




    }
}