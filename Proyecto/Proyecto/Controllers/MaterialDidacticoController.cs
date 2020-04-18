using BLL;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class MaterialDidacticoController : Controller
    {
        // GET: MaterialDidactico
        public ActionResult Index(int CursoId)
        {
            Bll_Login.VerificarSesionActiva();
            Bll_MaterialDidactico Bll_MaterialDidactico = new Bll_MaterialDidactico();
            List<MaterialDidactico> Lista = Bll_MaterialDidactico.ListarMaterialByCursoId(CursoId);
            Bll_Cursos Bll_Cursos = new Bll_Cursos();

            ViewBag.CursoId = CursoId;

            string NombreCurso = Bll_Cursos.GetCursoByCursoId(CursoId).Nombre;
            ViewBag.NombreCurso = NombreCurso;

            return View(Lista);
        }

        [HttpGet]
        public ActionResult MaterialDidacticoAdd(int id)
        {
            Bll_Login.VerificarSesionActiva();
            ViewBag.CursoId = id;
            return View();
        }


        [HttpPost]
        public ActionResult MaterialDidacticoAdd(HttpPostedFileBase file, int CursoId)
        {
            Bll_Login.VerificarSesionActiva();
            ViewBag.CursoId = CursoId;

            Bll_MaterialDidactico Bll_MaterialDidactico = new Bll_MaterialDidactico();

            if (Bll_MaterialDidactico.GuardarMaterialDidactico(CursoId, file))
            {// pregunta si la funcion de creacion se ejecuto con exito
                return RedirectToAction("Index", new { CursoId = CursoId });
            }
            else
            {// no creado
                return View();
            }

        }

        public FileContentResult DescargarDocumento(int MaterialDidacticoId)
        {
            Bll_Login.VerificarSesionActiva();

            Bll_MaterialDidactico Bll_MaterialDidactico = new Bll_MaterialDidactico();

            MaterialDidactico MaterialDidactico = null;
            MaterialDidactico = Bll_MaterialDidactico.ObtenerDocumentoByMaterialDidacticoId(MaterialDidacticoId);

            return File(MaterialDidactico.Contenido, MaterialDidactico.ContentType, MaterialDidactico.Filename);
        }

    }
}