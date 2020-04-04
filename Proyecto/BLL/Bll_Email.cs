using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Bll_Email
    {

        // Metodo que arma el Email y lo envia 
        public Boolean EnviarCorreo(string EmailDestino, string Asunto, string Mensaje)
        {
            // aca se leen los valores del webconfig
            string EmailRemitente = ConfigurationManager.AppSettings.Get("EmailRemitente");
            string PasswordEmailRemitente = ConfigurationManager.AppSettings.Get("PasswordEmailRemitente");
            int PuertoEmail = int.Parse(ConfigurationManager.AppSettings.Get("EmailPuerto"));
            string EmailHost = ConfigurationManager.AppSettings.Get("EmailHost");

            // para enviar correos electronicos, se debe crear una cuenta de gmail (preferiblemente), iniciar sesion en ella  y luego dirigirse a esta direccion 
            //  www.google.com/settings/security/lesssecureapps
            // y darle en la opcion Permitir el acceso de aplicaciones menos seguras: SÍ
            // luego colocar los datos de la cuenta y la clave

            /*-------------------------MENSAJE DE CORREO----------------------*/

            //Creamos un nuevo Objeto de mensaje
            System.Net.Mail.MailMessage mensaje = new System.Net.Mail.MailMessage();

            //Direccion de correo electronico a la que queremos enviar el mensaje

            mensaje.To.Add(EmailDestino);  
            //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

            //Asunto
            mensaje.Subject = Asunto;
            mensaje.SubjectEncoding = System.Text.Encoding.UTF8;

            //Direccion de correo electronico que queremos que reciba una copia del mensaje
            //mensaje.Bcc.Add("manuelgualdron@gmail.com"); //Opcional 

            //Cuerpo del Mensaje
            mensaje.Body = Mensaje;
            mensaje.BodyEncoding = System.Text.Encoding.UTF8;
            mensaje.IsBodyHtml = false; //Si no queremos que se envíe como HTML

            //Correo electronico desde el que se envia el mensaje
            mensaje.From = new MailAddress(EmailRemitente, "Sistema - TESIS", System.Text.Encoding.UTF8);
            mensaje.Priority = MailPriority.Normal;

            /*-------------------------CLIENTE DE CORREO----------------------*/
            //Creamos un objeto de cliente de correo
            System.Net.Mail.SmtpClient SmtpClient = new System.Net.Mail.SmtpClient();
            //Hay que crear las credenciales del correo emisor
            SmtpClient.UseDefaultCredentials = false;

            SmtpClient.Credentials = new System.Net.NetworkCredential(EmailRemitente, PasswordEmailRemitente); // ususario y clave de la cuenta desde la que se envian los msj

            SmtpClient.Port = PuertoEmail; // puerto del servidor para gmail es 587
            SmtpClient.EnableSsl = true;
            SmtpClient.Host = EmailHost; //Para Gmail "smtp.gmail.com";

            //// se llenan los datos de evidencia de correo
            //EvidenciaCorreo EvidenciaCorreo = new EvidenciaCorreo();
            //EvidenciaCorreo.Asunto = Asunto;
            //EvidenciaCorreo.EmailDestino = EmailDestino;
            //EvidenciaCorreo.EmailRemitente = EmailRemitente;
            //EvidenciaCorreo.Fecha = FuncionesSeguridad.FechaActual();
            //EvidenciaCorreo.Mensaje = Mensaje;
            //EvidenciaCorreo.TipoCorrespondencia = (byte)TipoCorrespondencia;

            /*-------------------------ENVIO DE CORREO----------------------*/
            try
            {
                //Se envia el mensaje      
                SmtpClient.Send(mensaje);
                SmtpClient.Dispose();

                //EvidenciaCorreo.Estado = (byte)Enum_EstadoAcceso.Acceso_Exitoso;
                //RegistroEvidenciaEmail(EvidenciaCorreo);

                return true;
            }
            catch (System.Net.Mail.SmtpException Error)// no puede detener el proceso por lo tanto no se coloca un Throw
            {
                //EvidenciaCorreo.Estado = (byte)Enum_EstadoAcceso.Acceso_Fallido;
                //RegistroEvidenciaEmail(EvidenciaCorreo);

                Bll_File.EscribirLog(Error.ToString());
                return false;
            }
        }

        // Metodo para registrar los envios de correos
        //public void RegistroEvidenciaEmail(EvidenciaCorreo EvidenciaCorreo)
        //{
        //    try
        //    {
        //        bd.EvidenciaCorreo.Add(EvidenciaCorreo);
        //        bd.SaveChanges();
        //    }
        //    catch (Exception error)// no puede detener el proceso por lo tanto no se coloca un throw
        //    {
        //        BLL_File.EscribirLog(error.ToString());
        //    } 
        //}

    }
}
