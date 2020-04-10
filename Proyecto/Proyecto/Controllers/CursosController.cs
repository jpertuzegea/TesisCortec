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
    public class CursosController : Controller
    {

        // GET: Cursos
        public ActionResult Index()
        {
            Bll_Login.VerificarSesionActiva();

            Bll_Cursos Bll_Cursos = new Bll_Cursos();
            List<Cursos> Cursos = Bll_Cursos.ListarCursos(EnumEstadoFiltro.Todos);

            return View(Cursos);
        }

        // GET: CursoAdd
        public ActionResult CursosAdd()
        {
            Bll_Login.VerificarSesionActiva();

            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text");

            List<SelectListItem> lista = new Bll_Personas().ArmarSelectCursos(EnumEstadoFiltro.Activo);
            ViewBag.Departamentos = lista;

            ViewBag.Docente = lista;
           
            return View();
        }

        // POST: Crear Notas_Rapidas
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CursoAdd(Cursos Curso)
        {
            Bll_Login.VerificarSesionActiva();

            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text", (int)Curso.Estado);

            if (ModelState.IsValid)
            {
                Bll_Cursos Bll_Cursos = new Bll_Cursos();

                if (Bll_Cursos.GuardarCursos(Curso))
                {// pregunta si la funcion de creacion se ejecuto con exito
                    return RedirectToAction("Index");
                }
                else
                {// no creado
                    return View(Curso);
                }
            }
            else
            {
                return View(Curso);
            }
        }


        [HttpGet]
        public ActionResult CursoUpdt(int id)
        {
            Bll_Login.VerificarSesionActiva();

            Bll_Cursos Bll_Cursos = new Bll_Cursos();
            Cursos Curso = Bll_Cursos.GetCursoByCursoId(id);
            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text", (int)Curso.Estado);
            return View(Curso);
        }

        //Update Curso
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CursoUpdt(Cursos Curso)
        {
            Bll_Login.VerificarSesionActiva();

            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text", (int)Curso.Estado);

            if (Curso != null)
            {
                if (ModelState.IsValid)
                {
                    Bll_Cursos Bll_Cursos = new Bll_Cursos();

                    if (Bll_Cursos.ModificarCursos(Curso))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(Curso);
                    }
                }
                else
                {
                    return View(Curso);
                }
            }
            else
            {
                return View(Curso);
            }
        } 
    }
}