using BLL.Enums;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace BLL
{
    public class Bll_Login
    {

        private TESIS_BD BD = new TESIS_BD();


        // Metodo para validar Inicio de Sesion al sistema
        public Boolean IniciarSesion(Personas Persona)
        {
            // Persona.Clave = Funciones_Seguridad.EncriptarPassword(Persona.Clave);

            Personas login = BD.Personas.Where(u => u.Email.ToUpper().Equals(Persona.Email.ToUpper())
               &&
               u.Clave.Equals(Persona.Clave)
               &&
               u.Estado == (byte)EnumEstados.Activo).FirstOrDefault();

            if ((login != null))
            {
                System.Web.HttpContext.Current.Session["IdUsuariosigecorTesis"] = login.PersonaId;
                System.Web.HttpContext.Current.Session["NombreUsuarioTesis"] = login.NombreComplpeto;

                return true;
            }
            else
            {
                return false;
            }
        }

        // Metodo para cerrar la sesiojn y eliminar las variables de Sesion
        public void CerrarSesion()
        {
            System.Web.HttpContext.Current.Session["IdUsuariosigecorTesis"] = null;
            System.Web.HttpContext.Current.Session["NombreUsuarioTesis"] = null; 
            System.Web.HttpContext.Current.Session.Abandon();// destruye los objetos de sesion existentes
            FormsAuthentication.SignOut();
        }

        // Metodo que valida si existe una sesion activa
        public static void VerificarSesionActiva()
        {
            String NombreUsuarioTesis = (String)System.Web.HttpContext.Current.Session["NombreUsuarioTesis"];// se captura la variable de sesion con la que se validara que el usuario este logueado 

            if (NombreUsuarioTesis != null)// si se poden las 2 condiciones en el mismo if se explota porque la 2° siempre llega nula
            { // si esta condicion se cumple el usuario es dirigido al Login
                if (NombreUsuarioTesis.Length < 3) // Se verifica que la sesion exista y que el nombre del usuario logueado sea mayor a 3 (numero minimo para un nombre [ana])
                {
                    System.Web.HttpContext.Current.Response.Redirect("/login");// si la sesion no existe, lo direcciona al login
                }
            }
            else
            { 
                System.Web.HttpContext.Current.Response.Redirect("/login");// si la sesion no existe, lo direcciona al login
            }
        }

        // Metodo para crear Cookies
        public void CrearCookie(string NombreCookie, string ValorCookie, int ExpiracionSegundos)
        {

            System.Web.HttpCookie Cookie = new System.Web.HttpCookie(NombreCookie);
            Cookie.Value = ValorCookie;
            Cookie.Expires = DateTime.Now.AddSeconds(ExpiracionSegundos);
            System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);



        }
    }
}
