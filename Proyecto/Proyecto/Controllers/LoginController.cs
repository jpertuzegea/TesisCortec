﻿using BLL;
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
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Personas Personas)
        {
             
            Bll_Login BLL_Login = new BLL_Login();

            if (BLL_Login.IniciarSesion(Usuarios_Model))
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


    }
}