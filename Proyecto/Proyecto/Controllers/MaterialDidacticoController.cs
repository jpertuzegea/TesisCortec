using BLL;
using BLL.Enums;
using DAO;
using Proyecto.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class MaterialDidacticoController : Controller
    {
        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Listar_Material_Didactico_Del_Curso)]
        public ActionResult Index(int CursoId)
        {
            //   Bll_Login.VerificarSesionActiva();
            Bll_MaterialDidactico Bll_MaterialDidactico = new Bll_MaterialDidactico();
            List<MaterialDidactico> Lista = Bll_MaterialDidactico.ListarMaterialByCursoId(CursoId);
            Bll_Cursos Bll_Cursos = new Bll_Cursos();

            ViewBag.CursoId = CursoId;

            string NombreCurso = Bll_Cursos.GetCursoByCursoId(CursoId).Nombre;
            ViewBag.NombreCurso = NombreCurso;

            return View(Lista);
        }


        [HttpGet]
        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Crear_Material_Didactico_Del_Curso)]
        public ActionResult MaterialDidacticoAdd(int id)
        {
            //   Bll_Login.VerificarSesionActiva();
            ViewBag.CursoId = id;
            return View();
        }


        [HttpPost]
        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Crear_Material_Didactico_Del_Curso)]
        public ActionResult MaterialDidacticoAdd(MaterialDidactico MaterialDidactico, HttpPostedFileBase file)
        {
            //   Bll_Login.VerificarSesionActiva();
            ViewBag.CursoId = MaterialDidactico.CursoId;

            Bll_MaterialDidactico Bll_MaterialDidactico = new Bll_MaterialDidactico();

            if (Bll_MaterialDidactico.GuardarMaterialDidactico(MaterialDidactico, file))
            {// pregunta si la funcion de creacion se ejecuto con exito
                return RedirectToAction("Index", new { CursoId = MaterialDidactico.CursoId });
            }
            else
            {// no creado
                return View();
            }

        }


        [VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Descargar_Material_Didactico_Del_Curso)]
        public FileContentResult DescargarDocumento(int MaterialDidacticoId)
        {
            //   Bll_Login.VerificarSesionActiva();

            Bll_MaterialDidactico Bll_MaterialDidactico = new Bll_MaterialDidactico();

            MaterialDidactico MaterialDidactico = null;
            MaterialDidactico = Bll_MaterialDidactico.ObtenerDocumentoByMaterialDidacticoId(MaterialDidacticoId);

            return File(MaterialDidactico.Contenido, MaterialDidactico.ContentType, MaterialDidactico.Filename);
        }

    }
}