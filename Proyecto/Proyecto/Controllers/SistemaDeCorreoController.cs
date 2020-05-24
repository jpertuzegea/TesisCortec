using BLL;
using BLL.Enums;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class SistemaDeCorreoController : Controller
    {
        public ActionResult BandejaEntrada() 
        {
           //   Bll_Login.VerificarSesionActiva();
            Bll_SistemaDeCorreo Bll_SistemaDeCorreo = new Bll_SistemaDeCorreo();
            List<SistemaCorreo> Lista = Bll_SistemaDeCorreo.BandejaEntrada();
            ViewBag.CantidadMensajes = Lista.Count();
            return View(Lista);
        }
        public ActionResult BandejaSalida()
        {
           //   Bll_Login.VerificarSesionActiva();
            Bll_SistemaDeCorreo Bll_SistemaDeCorreo = new Bll_SistemaDeCorreo();
            List<SistemaCorreo> Lista = Bll_SistemaDeCorreo.BandejaSalida();
            ViewBag.CantidadMensajes = Lista.Count();
            return View(Lista);
        }

        [HttpGet]
        public ActionResult LeerCorreo(string SistemaCorreoId)
        {
           //   Bll_Login.VerificarSesionActiva();
            Bll_SistemaDeCorreo Bll_SistemaDeCorreo = new Bll_SistemaDeCorreo();
            SistemaCorreo SistemaCorreo = Bll_SistemaDeCorreo.ObtenerCorreoBySistemaCorreoId(Int32.Parse(SistemaCorreoId));
            Bll_SistemaDeCorreo.MarcarCorreoComoLeidoBySistemaCorreoId(SistemaCorreo);

            return View(SistemaCorreo);
        }


        [HttpGet]
        public ActionResult EnviarCorreo()
        {
           //   Bll_Login.VerificarSesionActiva();
            Bll_Personas Bll_Personas = new Bll_Personas();
            List<SelectListItem> lista = Bll_Personas.ArmarSelectPersona(EnumEstadoFiltro.Activo, EnumRolAcademico.Estudiante, true);

            ViewBag.DestinoId = new SelectList(lista, "Value", "Text");
            return View();
        }



        [HttpPost]
        public ActionResult EnviarCorreo(SistemaCorreo SistemaCorreo)
        {
           //   Bll_Login.VerificarSesionActiva();

            if (ModelState.IsValid)
            {
                Bll_SistemaDeCorreo Bll_SistemaDeCorreo = new Bll_SistemaDeCorreo();

                if (Bll_SistemaDeCorreo.EnviarCorreo(SistemaCorreo))
                {// pregunta si la funcion de creacion se ejecuto con exito
                    return RedirectToAction("BandejaSalida");
                }
                else
                {// no creado
                    return View(SistemaCorreo);
                }
            }
            else
            {
                return View(SistemaCorreo);
            }
        }
    }
}