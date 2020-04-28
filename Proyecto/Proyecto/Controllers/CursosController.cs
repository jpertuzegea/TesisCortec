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
            List<Cursos> Cursos = Bll_Cursos.ListarCursos(EnumEstadoFiltro.Todos, EnumEstadosCurso.Ofertado);// aca no importa el segundo parametro porque va a traer el total de los cursos

            return View(Cursos);
        }

        // GET: CursosOfertados
        public ActionResult CursosOfertados()
        {
            Bll_Login.VerificarSesionActiva();
            Bll_Cursos Bll_Cursos = new Bll_Cursos();
            List<Cursos> Cursos = Bll_Cursos.ListarCursos(EnumEstadoFiltro.Activo, EnumEstadosCurso.Ofertado);

            return View(Cursos);
        }

        [HttpGet]
        public ActionResult DetalleCurso(int id)
        {
            Bll_Login.VerificarSesionActiva();

            Bll_Cursos Bll_Cursos = new Bll_Cursos();
            Cursos Curso = Bll_Cursos.GetCursoByCursoId(id);
            ViewBag.EstadoAcademico = ((EnumEstadosCurso)Curso.EstadoAcademico).ToString();

            return View(Curso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DetalleCurso(int CursoId, string Nombre, string Codigo)
        {
            Bll_Login.VerificarSesionActiva();

            Bll_CursoEstudiante Bll_CursoEstudiante = new Bll_CursoEstudiante();
          
            if (Bll_CursoEstudiante.Matricularse(CursoId, Nombre, Codigo))
            {// pregunta si la funcion de creacion se ejecuto con exito
                return RedirectToAction("CursosOfertados", "Cursos");
            }
            else
            {// no creado
                return View();
            }

        }
         

        [HttpGet]
        public ActionResult CursosDictados()
        {
            Bll_Login.VerificarSesionActiva();

            int DocenteId = (int)System.Web.HttpContext.Current.Session["IdUsuarioTesis"];
            Bll_Cursos Bll_Cursos = new Bll_Cursos();
            List<Cursos> Curso = Bll_Cursos.ListarCursosByDocenteId(EnumEstadoFiltro.Activo, DocenteId);
            return View(Curso);
        }
         

        // GET: CursoAdd
        public ActionResult CursosAdd()
        {
            Bll_Login.VerificarSesionActiva();

            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text");

            List<SelectListItem> lista = new Bll_Personas().ArmarSelectPersona(EnumEstadoFiltro.Activo, EnumRolAcademico.Docente);
            ViewBag.Docente = new SelectList(lista, "Value", "Text");

            return View();
        }

        // POST: Crear Notas_Rapidas
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CursosAdd(Cursos Curso, HttpPostedFileBase file)
        {
            Bll_Login.VerificarSesionActiva();

            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text", (int)Curso.Estado);

            List<SelectListItem> lista = new Bll_Personas().ArmarSelectPersona(EnumEstadoFiltro.Activo, EnumRolAcademico.Docente);
            ViewBag.Docente = new SelectList(lista, "Value", "Text", Curso.Docente);

            if (ModelState.IsValid)
            {
                Bll_Cursos Bll_Cursos = new Bll_Cursos();

                if (Bll_Cursos.GuardarCursos(Curso, file))
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

            List<SelectListItem> lista = new Bll_Personas().ArmarSelectPersona(EnumEstadoFiltro.Activo, EnumRolAcademico.Docente);
            ViewBag.Docente = new SelectList(lista, "Value", "Text", Curso.Docente);
            ViewBag.EstadoAcademico = new SelectList(FuncionesEnum.ListaEnum<EnumEstadosCurso>(), "Value", "Text", (int)Curso.EstadoAcademico);

            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text", (int)Curso.Estado);
            return View(Curso);
        }

        //Update Curso
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CursoUpdt(Cursos Curso, HttpPostedFileBase file)
        {
            Bll_Login.VerificarSesionActiva();

            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text", (int)Curso.Estado);
            ViewBag.EstadoAcademico = new SelectList(FuncionesEnum.ListaEnum<EnumEstadosCurso>(), "Value", "Text", (int)Curso.EstadoAcademico);
            List<SelectListItem> lista = new Bll_Personas().ArmarSelectPersona(EnumEstadoFiltro.Activo, EnumRolAcademico.Docente);
            ViewBag.Docente = new SelectList(lista, "Value", "Text", Curso.Docente);

            if (Curso != null)
            {
                if (ModelState.IsValid)
                {
                    Bll_Cursos Bll_Cursos = new Bll_Cursos();

                    if (Bll_Cursos.ModificarCursos(Curso, file))
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


        public ActionResult MostrarImagenCurso(int CursoId)
        {
            Bll_Login.VerificarSesionActiva();
            Bll_Cursos Bll_Cursos = new Bll_Cursos();
            byte[] CursoImagen = Bll_Cursos.GetImagenByCursoId(CursoId);

            if (CursoImagen != null)
            {
                if (CursoImagen != null)
                {
                    return File(CursoImagen, "image/jpg");
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
    }
}