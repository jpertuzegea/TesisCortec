using BLL;
using BLL.Enums;
using BLL.Utilidades;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class ForoTemaController : Controller
    {

        public ActionResult Index(int CursoId)
        {
            Bll_Login.VerificarSesionActiva();
            Bll_ForoTema Bll_Foro = new Bll_ForoTema();
            List<ForoTema> Lista = Bll_Foro.ListarForosTemaByCursoId(CursoId);

            ViewBag.CursoId = CursoId;

            Bll_Cursos Bll_Cursos = new Bll_Cursos();
            string NombreCurso = Bll_Cursos.GetCursoByCursoId(CursoId).Nombre;
            ViewBag.NombreCurso = NombreCurso;

            return View(Lista);
        }

        [HttpGet]
        public ActionResult ForoTemaAdd(int id)
        {
            Bll_Login.VerificarSesionActiva();
            ViewBag.CursoId = id;
            return View();
        }


        [HttpPost]
        public ActionResult ForoTemaAdd(ForoTema ForoTema)
        {
            Bll_Login.VerificarSesionActiva();
            ViewBag.CursoId = ForoTema.CursoId;

            Bll_ForoTema Bll_Foro = new Bll_ForoTema();

            if (Bll_Foro.GuardarForoTema(ForoTema))
            {// pregunta si la funcion de creacion se ejecuto con exito
                return RedirectToAction("Index", new { CursoId = ForoTema.CursoId });
            }
            else
            {// no creado
                return View();
            }

        }


        [HttpGet]
        public ActionResult ForoTemaUpdt(int id)
        {
            Bll_Login.VerificarSesionActiva();

            Bll_ForoTema Bll_ForoTema = new Bll_ForoTema();
            ForoTema ForoTema = Bll_ForoTema.ObtenerForoTemaByForoTemaId(id);

            ViewBag.CursoId = ForoTema.CursoId;
            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text", (int)ForoTema.Estado);
            ViewBag.ForoTemaId = id;
            return View(ForoTema);
        }


        [HttpPost]
        public ActionResult ForoTemaUpdt(ForoTema ForoTema)
        {
            Bll_Login.VerificarSesionActiva();

            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text", (int)ForoTema.Estado);
            ViewBag.ForoTemaId = ForoTema.ForoTemaId;
            ViewBag.CursoId = ForoTema.CursoId;

            Bll_ForoTema Bll_ForoTema = new Bll_ForoTema();

            if (Bll_ForoTema.ModificarForoTema(ForoTema))
            {// pregunta si la funcion de creacion se ejecuto con exito
                return RedirectToAction("Index", new { CursoId = ForoTema.CursoId });
            }
            else
            {// no creado
                return View();
            }
        }


    }
}