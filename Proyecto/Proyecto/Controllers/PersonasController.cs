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
            List<Personas> Lista = Bll_Personas.ListarPersonas(BLL.Enums.EnumEstadoFiltro.Todos);
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
        public ActionResult PersonaAdd(Personas Persona, HttpPostedFileBase file)
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
            return View(persona);
        }

        //Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PersonasUpdt(Personas Persona)
        {
            Bll_Login.VerificarSesionActiva();
            ViewBag.TipoDocumento = new SelectList(FuncionesEnum.ListaEnum<EnumTipoDocumento>(), "Value", "Text", (int)Persona.Estado);

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
                if (Proyecto.Models.Variables.Imagen != null)
                {
                    return File(Proyecto.Models.Variables.Imagen, Proyecto.Models.Variables.ContetType);
                }
                else
                {
                    return null;
                } 
            }
             
           
            Bll_Personas Bll_Personas = new Bll_Personas();
            Personas Persona = Bll_Personas.GetImagenByPersonaId(PersonaId);

            if (Persona != null)
            {
                if (Persona.Imagen != null)
                {
                    return File(Persona.Imagen, "image/jpg");
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