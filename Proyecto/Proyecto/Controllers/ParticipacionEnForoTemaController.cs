using BLL;
using DAO;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class ParticipacionEnForoTemaController : Controller
    {
        public ActionResult IngresarAlForoTema(int ForoTemaId)
        {
            Bll_Login.VerificarSesionActiva();

            Bll_ParticipacionEnForoTema Bll_ParticipacionEnForoTema = new Bll_ParticipacionEnForoTema();
            List<ParticipacionEnForoTema> Lista = Bll_ParticipacionEnForoTema.ListarParticipacionEnForoTema(ForoTemaId);

            ViewBag.ForoTemaId = ForoTemaId;
            ViewBag.Cantidad = Lista.Count;

            return View(Lista);
        }

        public ActionResult ParticiparEnForoTemaAdd(int id)//ForoTemaId
        {
            Bll_Login.VerificarSesionActiva();
            Bll_ForoTema Bll_ForoTema = new Bll_ForoTema();
            ForoTema ForoTema = Bll_ForoTema.ObtenerForoTemaByForoTemaId(id);

            ViewBag.Tema = ForoTema.Tema;
            ViewBag.Curso = ForoTema.Cursos.Nombre;
            ViewBag.Docente = ForoTema.Personas.NombreCompleto;
            ViewBag.DocenteId = ForoTema.Cursos.Docente;
            ViewBag.ForoTemaId = id;

            return View();
        }
        [HttpPost]
        public ActionResult ParticiparEnForoTemaAdd(ParticipacionEnForoTema ParticipacionEnForoTema, string NombreDocente, string DocenteId, string NombreCurso, string Tema)
        {
            Bll_Login.VerificarSesionActiva();
            Bll_ParticipacionEnForoTema Bll_ParticipacionEnForoTema = new Bll_ParticipacionEnForoTema();

            if (Bll_ParticipacionEnForoTema.GuardarParticipacionEnForoTema(ParticipacionEnForoTema.ForoTemaId, ParticipacionEnForoTema.Mensaje, NombreDocente, DocenteId, NombreCurso, Tema))
            {// pregunta si la funcion de creacion se ejecuto con exito
                ViewBag.ForoTemaId = ParticipacionEnForoTema.ForoTemaId;
                return RedirectToAction("IngresarAlForoTema", new { ForoTemaId = ParticipacionEnForoTema.ForoTemaId });
            }
            else
            {// no creado
                return View(ParticipacionEnForoTema);
            }
        }

    }
}