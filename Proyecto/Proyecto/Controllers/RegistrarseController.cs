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
    public class RegistrarseController : Controller
    {
        public ActionResult PersonaAdd()
        {
            //Bll_Login.VerificarSesionActiva();
            ViewBag.TipoDocumento = new SelectList(FuncionesEnum.ListaEnum<EnumTipoDocumento>(), "Value", "Text");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //[VerificarPerfil(_Perfil: EnumPerfilesActivos.Permite_Acceder_Crear_Persona)]
        public ActionResult PersonaAdd(Personas Persona)
        {
            //Bll_Login.VerificarSesionActiva();
            ViewBag.TipoDocumento = new SelectList(FuncionesEnum.ListaEnum<EnumTipoDocumento>(), "Value", "Text", Persona.TipoDocumento);

            if (ModelState.IsValid)
            {
                Bll_Personas Bll_Personas = new Bll_Personas();

                if (Bll_Personas.GuardarPersona(Persona))
                {// pregunta si la funcion de creacion se ejecuto con exito
                    return RedirectToAction("Index","Login");
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

    }
}