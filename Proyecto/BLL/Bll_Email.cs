using DAO;
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
        private TESIS_BD BD = null;
        private Bll_EvidenciaCorreo Bll_EvidenciaCorreo = null;

        public Bll_Email()
        {
            BD = new TESIS_BD();
            Bll_EvidenciaCorreo = new Bll_EvidenciaCorreo();
        }

        // Metodo que arma el Email y lo envia 
        public bool EnviarCorreo(string EmailDestino, string Asunto, string Mensaje)
        {
            string EmailRemitente = ConfigurationManager.AppSettings.Get("EmailRemitente");
            string PasswordEmailRemitente = ConfigurationManager.AppSettings.Get("PasswordEmailRemitente");
            int PuertoEmail = int.Parse(ConfigurationManager.AppSettings.Get("EmailPuerto"));
            string EmailHost = ConfigurationManager.AppSettings.Get("EmailHost");

            // para enviar correos electronicos, se debe crear una cuenta de gmail (preferiblemente), iniciar sesion en ella  y luego dirigirse a esta direccion 
            //  www.google.com/settings/security/lesssecureapps
            // y darle en la opcion Permitir el acceso de aplicaciones menos seguras: SÍ
            // luego colocar los datos de la cuenta y la clave

            MailMessage mensaje = new System.Net.Mail.MailMessage();

            // mensaje.To.Add("katherin3125 @hotmail.com");
            mensaje.To.Add("Luzkduco@gmail.com");
            mensaje.To.Add(EmailDestino);
            mensaje.Subject = Asunto;
            mensaje.SubjectEncoding = System.Text.Encoding.UTF8;
            mensaje.Body = Mensaje;
            mensaje.BodyEncoding = System.Text.Encoding.UTF8;
            mensaje.IsBodyHtml = false;
            mensaje.From = new MailAddress(EmailRemitente, "Sistema - TESIS", System.Text.Encoding.UTF8);
            mensaje.Priority = MailPriority.Normal;
            System.Net.Mail.SmtpClient SmtpClient = new System.Net.Mail.SmtpClient();
            SmtpClient.UseDefaultCredentials = false;
            SmtpClient.Credentials = new System.Net.NetworkCredential(EmailRemitente, PasswordEmailRemitente); // ususario y clave de la cuenta desde la que se envian los msj
            SmtpClient.Port = PuertoEmail;
            SmtpClient.EnableSsl = true;
            SmtpClient.Host = EmailHost;

            // se llenan los datos de evidencia de correo
            EvidenciaCorreo EvidenciaCorreo = new EvidenciaCorreo();
            EvidenciaCorreo.Asunto = Asunto;
            EvidenciaCorreo.EmailDestino = EmailDestino;
            EvidenciaCorreo.EmailRemitente = EmailRemitente;
            EvidenciaCorreo.Fecha = DateTime.Now;
            EvidenciaCorreo.Mensaje = Mensaje;

            /*-------------------------ENVIO DE CORREO----------------------*/
            try
            {
                //Se envia el mensaje      
                SmtpClient.Send(mensaje);
                SmtpClient.Dispose();

                EvidenciaCorreo.EstadoEnvio = "Enviado Exitoso";
                Bll_EvidenciaCorreo.GuardarEvidenciaCorreo(EvidenciaCorreo);


                return true;
            }
            catch (System.Net.Mail.SmtpException Error)// no puede detener el proceso por lo tanto no se coloca un Throw
            {
                EvidenciaCorreo.EstadoEnvio = "Enviado Fallido";
                Bll_EvidenciaCorreo.GuardarEvidenciaCorreo(EvidenciaCorreo);

                Bll_File.EscribirLog(Error.ToString());
                return false;
            }
        }



    }
}
