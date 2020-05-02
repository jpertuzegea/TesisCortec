using BCrypt.Net;
using BLL.Enums;
using DAO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace BLL
{
    public class Bll_Login
    {

        private TESIS_BD BD = new TESIS_BD();

        public bool IniciarSesion(Personas Persona)
        {
            try
            {
                VerificarPeriodoDeEvaluacion();
                var MyClave = Persona.Clave.ComputeHash(HashType.SHA256);
                Personas login = BD.Personas.Where(u => u.Email.ToUpper().Equals(Persona.Email.ToUpper())
                   &&
                   u.Clave.Equals(MyClave)
                   &&
                   u.Estado == (byte)EnumEstadoFiltro.Activo).FirstOrDefault();

                Bll_IngresoAlSistema Bll_IngresoAlSistema = new Bll_IngresoAlSistema();

                if ((login != null))
                {
                    HttpContext.Current.Session["IdUsuarioTesis"] = login.PersonaId;
                    HttpContext.Current.Session["NombreUsuarioTesis"] = login.NombreCompleto;

                    Bll_CursoEstudiante Bll_CursoEstudiante = new Bll_CursoEstudiante();
                    Bll_SistemaDeCorreo Bll_SistemaDeCorreo = new Bll_SistemaDeCorreo();

                    HttpContext.Current.Session["CursosMatriculadosActivos"] = Bll_CursoEstudiante.ObtenerCantidadCusosActivosByPersonaId(login.PersonaId);
                    Bll_SistemaDeCorreo.ObtenerCorreosSinLeerByPersonaId();
                    Bll_IngresoAlSistema.RegistroIngresoAlSitema(Persona.Email.ToUpper(), EnumEstadoAcceso.Acceso_Exitoso);

                    return true;
                }
                else
                {
                    Bll_IngresoAlSistema.RegistroIngresoAlSitema(Persona.Email.ToUpper(), EnumEstadoAcceso.Acceso_Fallido);
                    return false;
                }
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return false;
            }

        }

        public void CerrarSesion()
        {
            System.Web.HttpContext.Current.Session["IdUsuarioTesis"] = null;
            System.Web.HttpContext.Current.Session["NombreUsuarioTesis"] = null;
            System.Web.HttpContext.Current.Session.Abandon();// destruye los objetos de sesion existentes
            FormsAuthentication.SignOut();
        }

        public static void VerificarSesionActiva()
        {
            String NombreUsuarioTesis = (String)HttpContext.Current.Session["NombreUsuarioTesis"];// se captura la variable de sesion con la que se validara que el usuario este logueado 

            if (NombreUsuarioTesis != null)
            {
                if (NombreUsuarioTesis.Length < 3)
                {
                    HttpContext.Current.Response.Redirect("/login");
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("/login");
            }


            // Esta parte es solo para actualizar el icono de mensajes no leidos en la aplicaicon 
            Bll_SistemaDeCorreo Bll_SistemaDeCorreo = new Bll_SistemaDeCorreo();
            Bll_SistemaDeCorreo.ObtenerCorreosSinLeerByPersonaId(); 

        }

        public void CrearCookie(string NombreCookie, string ValorCookie, int ExpiracionSegundos)
        {
            System.Web.HttpCookie Cookie = new System.Web.HttpCookie(NombreCookie);
            Cookie.Value = ValorCookie;
            Cookie.Expires = DateTime.Now.AddSeconds(ExpiracionSegundos);
            System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
        }

        public static void VerificarPeriodoDeEvaluacion()
        { 
            try
            {
                DateTime fechaQuemada = DateTime.ParseExact("31-12-2020", "dd-MM-yyyy", CultureInfo.InvariantCulture);// Permite Convertir de forma Excata la el formato de fecha 
                DateTime FechActual = DateTime.Now.Date;

                if (fechaQuemada < FechActual) // Si la fecha quemada es menor a la fecha actual
                {
                    HttpContext.Current.Response.Redirect("Login/Expiracion");// si la sesion no existe, lo direcciona al login
                }
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
            }
        }

    }
}
