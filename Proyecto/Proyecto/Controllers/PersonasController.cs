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
            Bll_Personas Bll_Personas = new Bll_Personas();
            List<Personas> Lista = Bll_Personas.ListarPersonas(BLL.Enums.EnumEstadoFiltro.Activo);
            return View(Lista);
        }


        // GET: PersonaAdd
        public ActionResult PersonaAdd()
        {
            ViewBag.TipoDocumento = new SelectList(FuncionesEnum.ListaEnum<EnumTipoDocumento>(), "Value", "Text");
            return View();
        }

        // POST:   PersonaAdd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PersonaAdd(Personas Persona, HttpPostedFileBase file)
        {
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
            Bll_Personas Bll_Personas = new Bll_Personas();
            Personas persona = Bll_Personas.GetPersonaByPersonaId(id);
            ViewBag.TipoDocumento = new SelectList(FuncionesEnum.ListaEnum<EnumTipoDocumento>(), "Value", "Text", (int)persona.Estado);
            return View(persona);
        }

        //Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PersonasUpdt(Personas Persona)
        {
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
                    var errors = ModelState.Select(x => x.Value.Errors)
     .Where(y => y.Count > 0)
     .ToList();
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