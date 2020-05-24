using BLL;
using DAO;
using DAO.ViewModel;
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
           //   Bll_Login.VerificarSesionActiva();
            int EstudianteId = (int)System.Web.HttpContext.Current.Session["IdUsuarioTesis"];
            Bll_CursoEstudiante Bll_CursoEstudiante = new Bll_CursoEstudiante();
            List<CursoEstudiante> Lista = Bll_CursoEstudiante.ListarCursosActivosbyPersonaId(EstudianteId);

            return View(Lista);
        }


        public ActionResult CalificacionesEstudiante(int CursoId)
        {
           //   Bll_Login.VerificarSesionActiva();
            Bll_CursoEstudiante Bll_CursoEstudiante = new Bll_CursoEstudiante();
            ListaCalificacionestudiantes Lista = Bll_CursoEstudiante.ListaEstudiantesByCursoId(CursoId);

            return View(Lista);
        }
        [HttpPost]
        public ActionResult CalificacionesEstudiante(ListaCalificacionestudiantes ListaCalificacionestudiantes)
        {
           //   Bll_Login.VerificarSesionActiva();
            Bll_CursoEstudiante Bll_CursoEstudiante = new Bll_CursoEstudiante();
            if (Bll_CursoEstudiante.GuargarCalificacionEstudiante(ListaCalificacionestudiantes))
            {// pregunta si la funcion de creacion se ejecuto con exito
                return RedirectToAction("CalificacionesEstudiante", "CursoEstudiante", new { CursoId = ListaCalificacionestudiantes.ListaCursoEstudiante[0].CursoId });
            }
            else
            {// no creado
                return View(ListaCalificacionestudiantes);
            }
        }


        public ActionResult VerNotasEstudiante(int CursoId, int EstudianteId)
        {
           //   Bll_Login.VerificarSesionActiva();
            Bll_CursoEstudiante Bll_CursoEstudiante = new Bll_CursoEstudiante();
            CursoEstudiante CursoEstudiante = Bll_CursoEstudiante.ObtenerNotasByCursoIdEstudianteId(CursoId, EstudianteId);
            return View(CursoEstudiante); 
        }

    }
}