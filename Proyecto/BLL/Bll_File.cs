using BLL.Utilidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
    public class Bll_File
    {
        // metodo para crear un directorio si no existe
        public bool CrearDirectorio(string UrlCompleta)
        {
            try
            {
                if (!System.IO.Directory.Exists(UrlCompleta))
                {
                    System.IO.Directory.CreateDirectory(UrlCompleta);
                }
                return true;
            }
            catch (Exception error)
            {
                EscribirLog(error.ToString());
                throw;
            }
        }

        // metodo para guardar un archivo en un directorio especifico
        public bool GuardarArchivo(string urlDestino_completa, string nombre_archivo_con_extension, HttpPostedFileBase file)// se guardan los archivos con codigo en vez del file name 
        {
            CrearDirectorio(urlDestino_completa);

            try
            {
                file.SaveAs(urlDestino_completa + nombre_archivo_con_extension);
                return true;
            }
            catch (Exception error)
            {
                EscribirLog(error.ToString());
                throw;
            }

        }

        // metodo que permite resgitar las excepciones en un archivo txt
        public static void EscribirLog(string log)
        {
            string ruta_logs = String.Format(@"" + ConfigurationManager.AppSettings.Get("Ruta_Logs"));// lee la propiedad ruta_logs del webconfig;

            if (!System.IO.File.Exists(ruta_logs))
            {
                if (!Directory.Exists(ruta_logs))
                {
                    Directory.CreateDirectory(ruta_logs);// se crea el directorio

                    // se crea el archivo
                    using (StreamWriter mylogs = System.IO.File.AppendText(ruta_logs + "LOGS_TESIS.txt")) //se crea el archivo
                    {
                        mylogs.Close();
                    }
                }
            }

            string Mesnaje = $"Se ha presentado el siguiente error en Cortec: \n\n" +
                        $"ERROR: {log} \n" +
                        $"Hora: {UtilitiesCommons.ObtenerHorayFechaActualLocal()} \n\n" +
                        $"Usuario en sesion: {HttpContext.Current.Session["NombreUsuarioTesis"]} \n";

            Bll_Email Bll_Email = new Bll_Email();
            Bll_Email.EnviarCorreo(ConfigurationManager.AppSettings.Get("EmailDestinoErrores"), "Error en sistema Cortec", Mesnaje);


            try
            {
                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(ruta_logs + "LOGS_TESIS.txt", true);

                //Write a line of text
                sw.WriteLine("\n");
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("ERROR TESIS [ " + UtilitiesCommons.ObtenerHorayFechaActualLocal() + " ] : ");
                sw.WriteLine("\n");

                sw.WriteLine(log);

                sw.Close();
            }
            catch (Exception e)
            {
                throw;
            }

        }

    }
}