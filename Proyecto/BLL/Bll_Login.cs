using BCrypt.Net;
using BLL.Enums;
using BLL.Utilidades;
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
            try
            {
                System.Web.HttpContext.Current.Session["IdUsuarioTesis"] = null;
                System.Web.HttpContext.Current.Session["NombreUsuarioTesis"] = null;
                System.Web.HttpContext.Current.Session.Abandon();// destruye los objetos de sesion existentes
                //FormsAuthentication.SignOut(); FormsAuthentication.SignOut();
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString()); 
            }
          
        }

    
        public void CrearCookie(string NombreCookie, string ValorCookie, int ExpiracionSegundos)
        {
            System.Web.HttpCookie Cookie = new System.Web.HttpCookie(NombreCookie);
            Cookie.Value = ValorCookie;
            Cookie.Expires = DateTime.Now.AddSeconds(ExpiracionSegundos);
            HttpContext.Current.Response.Cookies.Add(Cookie);

        }

        public static void VerificarPeriodoDeEvaluacion()
        {
            try
            {
                DateTime fechaQuemada = DateTime.ParseExact("31-12-2021", "dd-MM-yyyy", CultureInfo.InvariantCulture);// Permite Convertir de forma Excata la el formato de fecha 
                DateTime FechActual = UtilitiesCommons.ObtenerHorayFechaActualLocal().Date;

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
