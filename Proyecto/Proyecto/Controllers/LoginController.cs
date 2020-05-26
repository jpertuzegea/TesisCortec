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
    public class LoginController : Controller
    {
         
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Personas Persona)
        {
            Bll_Login BLL_Login = new Bll_Login();

            if (BLL_Login.IniciarSesion(Persona))
            {// Si las credenciales son correctas
                return RedirectToAction("Index", "Inicio");
            }
            else
            {
                ViewBag.Mensaje = "Usuario y Clave No Coinciden ";
                ViewBag.Acceso = "Acceso Denegado";
                return View();
            }
        }
         
        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Bll_Login Bll_Login = new Bll_Login();
            Bll_Login.CerrarSesion();
            return RedirectToAction("Index", "Login");
        } 

        [HttpGet]
        public ActionResult Expiracion()
        {
            return View();
        }



    }
}