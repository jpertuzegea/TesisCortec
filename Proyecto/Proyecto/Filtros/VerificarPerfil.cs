using BLL;
using BLL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Filtros
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class VerificarPerfil : AuthorizeAttribute
    {
        private EnumPerfilesActivos Perfil;

        public VerificarPerfil(EnumPerfilesActivos _Perfil)
        {
            Perfil = _Perfil;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {  
                if (!Bll_Perfiles.VerificarPerfil(Perfil))
                {
                    filterContext.HttpContext.Response.Redirect("/ErrorPerfil");
                }
            }
            catch (Exception Error)
            {
                Bll_File.EscribirLog(Error.ToString());
            }

            //base.OnAuthorization(filterContext);// Si se coloca el metodo Base se bloquea el acceso
        }
         
    }
}