using BLL;
using BLL.Enums;
using BLL.Utilidades;
using DAO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class PersonasController : Controller
    {
        // GET: Personas
        public ActionResult Index()
        {
            Bll_Login.VerificarSesionActiva();
            Bll_Personas Bll_Personas = new Bll_Personas();
            List<Personas> Lista = Bll_Personas.ListarPersonas(EnumEstadoFiltro.Todos);
            return View(Lista);
        }


        // GET: PersonaAdd
        public ActionResult PersonaAdd()
        {
            //Bll_Login.VerificarSesionActiva();
            ViewBag.TipoDocumento = new SelectList(FuncionesEnum.ListaEnum<EnumTipoDocumento>(), "Value", "Text");
            return View();
        }

        // POST:   PersonaAdd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PersonaAdd(Personas Persona)
        {
            //Bll_Login.VerificarSesionActiva();
            ViewBag.TipoDocumento = new SelectList(FuncionesEnum.ListaEnum<EnumTipoDocumento>(), "Value", "Text", Persona.TipoDocumento);

            if (ModelState.IsValid)
            {
                Bll_Personas Bll_Personas = new Bll_Personas();

                if (Bll_Personas.GuardarPersona(Persona))
                {// pregunta si la funcion de creacion se ejecuto con exito
                    return RedirectToAction("Index");
                }
                else
                {// no creado
                    return View(Persona);
                }
            }
            else
            {
                return View(Persona);
            }
        }

        //Update
        [HttpGet]
        public ActionResult PersonasUpdt(int id)
        {
            Bll_Login.VerificarSesionActiva();
            Bll_Personas Bll_Personas = new Bll_Personas();
            Personas persona = Bll_Personas.GetPersonaByPersonaId(id);

            ViewBag.TipoDocumento = new SelectList(FuncionesEnum.ListaEnum<EnumTipoDocumento>(), "Value", "Text", (int)persona.TipoDocumento);
            ViewBag.RolAcademico = new SelectList(FuncionesEnum.ListaEnum<EnumRolAcademico>(), "Value", "Text", persona.RolAcademico);

            return View(persona);
        }

        //Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PersonasUpdt(Personas Persona)
        {
            Bll_Login.VerificarSesionActiva();
            ViewBag.TipoDocumento = new SelectList(FuncionesEnum.ListaEnum<EnumTipoDocumento>(), "Value", "Text", (int)Persona.Estado);
            ViewBag.RolAcademico = new SelectList(FuncionesEnum.ListaEnum<EnumRolAcademico>(), "Value", "Text", Persona.RolAcademico);

            if (Persona != null)
            {
                if (ModelState.IsValid)
                {
                    Bll_Personas Bll_Personas = new Bll_Personas();
                    if (Bll_Personas.ModificarPersona(Persona))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(Persona);
                    }
                }
                else
                {
                    return View(Persona);
                }
            }
            else
            {
                return View(Persona);
            }
        }

        public ActionResult ConvertirImagen(int PersonaId)
        {

            Bll_Login.VerificarSesionActiva();

            if (PersonaId == 0)// panel informativo siempre sera 0 (se reutilizo el metodo)
            {
                Bll_PanelInformativo Bll_PanelInformativo = new Bll_PanelInformativo();
                PanelInformativo PanelInformativo = Bll_PanelInformativo.ObtenerPanelInformativoByPanelInformativoId();
                if (PanelInformativo.Imagen != null)
                {
                    return File(PanelInformativo.Imagen, PanelInformativo.ContetType);
                }
                else
                {
                    return null;
                }
            }

            Bll_Personas Bll_Personas = new Bll_Personas();
            byte[] PersonaImagen = Bll_Personas.GetImagenByPersonaId(PersonaId);

            if (PersonaImagen != null)
            {
                if (PersonaImagen != null)
                {
                    return File(PersonaImagen, "image/jpg");
                }
                else
                {
                    return null;
                }
            }
            return null;
        }


        // GET: CambioImagen
        public ActionResult CambioImagen()
        {
            Bll_Login.VerificarSesionActiva();
            return View();
        }

        // POST: CambioImagen
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CambioImagen(HttpPostedFileBase file)
        {
            Bll_Login.VerificarSesionActiva();

            if (ModelState.IsValid)
            {
                Bll_Personas Bll_Personas = new Bll_Personas();

                if (Bll_Personas.CambioImagen(file))
                {
                    return RedirectToAction("Index", "Inicio");
                }
                else
                {// no creado
                    return View();
                }
            }
            else
            {
                return View();
            }
        }


        // GET: CambioClave
        public ActionResult CambioClave()
        {
            Bll_Login.VerificarSesionActiva();
            return View();
        }

        // POST: CambioClave
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CambioClave(string Clave, string NuevaClave)
        {
            Bll_Login.VerificarSesionActiva();

            Bll_Personas Bll_Personas = new Bll_Personas();

            if (Bll_Personas.CambioClave(NuevaClave))
            {
                return RedirectToAction("Index", "Inicio");
            }
            else
            {// no creado
                return View();
            }

        }


    }
}