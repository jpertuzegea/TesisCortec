using BLL;
using Proyecto.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Filtros
{

    public class VerificarSesion : ActionFilterAttribute
    {


        // Toca registrar el filtro en FilterConfig
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            try
            {
                string NombreUsuarioTesis = (string)HttpContext.Current.Session["NombreUsuarioTesis"];// se captura la variable de sesion con la que se validara que el usuario este logueado 

                if (NombreUsuarioTesis == null)
                { 
                    if (filterContext.Controller is LoginController == false && filterContext.Controller is RegistrarseController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/login");
                    } 

                }
                else
                {
                    Bll_SistemaDeCorreo Bll_SistemaDeCorreo = new Bll_SistemaDeCorreo();
                    Bll_SistemaDeCorreo.ObtenerCorreosSinLeerByPersonaId();

                    if (filterContext.Controller is LoginController == true)
                    {
                        filterContext.HttpContext.Response.Redirect("/login");
                    }
                }
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
            }
        }
    }
}