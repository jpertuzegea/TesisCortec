using BLL;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class ForoController : Controller
    {

        public ActionResult Index(int CursoId)
        {
            Bll_Login.VerificarSesionActiva();
            Bll_Foro Bll_Foro = new Bll_Foro();
            List<Foro> Lista = Bll_Foro.ListarForosByCursoId(CursoId);

            ViewBag.CursoId = CursoId;

            Bll_Cursos Bll_Cursos = new Bll_Cursos();
            string NombreCurso = Bll_Cursos.GetCursoByCursoId(CursoId).Nombre;
            ViewBag.NombreCurso = NombreCurso;

            return View(Lista);
        }

        [HttpGet]
        public ActionResult ForoAdd(int id)
        {
            Bll_Login.VerificarSesionActiva();
            ViewBag.CursoId = id;
            return View();
        }


        [HttpPost]
        public ActionResult ForoAdd(Foro Foro)
        {
            Bll_Login.VerificarSesionActiva();
            ViewBag.CursoId = Foro.CursoId;

            Bll_Foro Bll_Foro = new Bll_Foro();

            if (Bll_Foro.GuardarForo(Foro))
            {// pregunta si la funcion de creacion se ejecuto con exito
                return RedirectToAction("Index", new { CursoId = Foro.CursoId });
            }
            else
            {// no creado
                return View();
            }

        }


        [HttpGet]
        public ActionResult ForoUpdt(int id)
        {
            //Bll_Login.VerificarSesionActiva();
            //ViewBag.CursoId = id;
            return View();
        }


        [HttpPost]
        public ActionResult ForoUpdt(Foro Foro)
        {
            //Bll_Login.VerificarSesionActiva();
            //ViewBag.CursoId = Foro.CursoId;

            //Bll_Foro Bll_Foro = new Bll_Foro();

            //if (Bll_Foro.GuardarForo(Foro))
            //{// pregunta si la funcion de creacion se ejecuto con exito
            //    return RedirectToAction("Index", new { CursoId = Foro.CursoId });
            //}
            //else
            //{// no creado
            return View();
            //}
        }


    }
}