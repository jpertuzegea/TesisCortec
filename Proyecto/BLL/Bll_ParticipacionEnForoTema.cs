using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using BLL.Utilidades;

namespace BLL
{
    public class Bll_ParticipacionEnForoTema
    {

        private TESIS_BD BD = null;

        public Bll_ParticipacionEnForoTema()
        {
            BD = new TESIS_BD();
        }

        public List<ParticipacionEnForoTema> ListarParticipacionEnForoTema(int ForoTemaId)
        {
            try
            {
                List<ParticipacionEnForoTema> Lista = null;
                Lista = BD.ParticipacionEnForoTema.Include(x => x.Personas).Where(x => x.ForoTemaId == ForoTemaId).ToList();

                return Lista;
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return null;
            }
        }

        public bool GuardarParticipacionEnForoTema(int ForoTemaId, string Mensaje, string NombreDocente, string DocenteId, string Curso, string Tema)
        {
            if (ForoTemaId > 0 && Mensaje != null)
            {// si el objeto es diferente de nulo
                try
                {
                    ParticipacionEnForoTema ParticipacionEnForoTema = new ParticipacionEnForoTema();

                    ParticipacionEnForoTema.ParticipanteId = (int)HttpContext.Current.Session["IdUsuarioTesis"];
                    ParticipacionEnForoTema.ForoTemaId = ForoTemaId;
                    ParticipacionEnForoTema.Mensaje = Mensaje;
                    ParticipacionEnForoTema.FechaRegistro = UtilitiesCommons.ObtenerHorayFechaActualLocal();

                    BD.ParticipacionEnForoTema.Add(ParticipacionEnForoTema);
                    BD.SaveChanges();
                     
                    string Mesnaje =
                                 $"Buen dia Docente: {NombreDocente}.\n\n" +
                                 $"Se informa que se ha registrado una participacion en uno de los foros bajo su direccion, los datos son : \n\n" +
                                 $"Nombre Curso: {Curso}\n" +
                                 $"Nombre Participante: {HttpContext.Current.Session["NombreUsuarioTesis"]} \n" +
                                 $"Tema Tratado: {Tema}\n" +
                                 $"Hora de la participacion: {UtilitiesCommons.ObtenerHorayFechaActualLocal()}\n" +
                                 $"Mensaje: {Mensaje}\n\n" +

                                 "Feliz resto de dia. \n\n" +
                                 "Nota: Este mensaje es enviado por el sistema de forma automatica, favor No responderlo.";

                    string Email = new Bll_Personas().GetEmailByPersonaId(Int32.Parse(DocenteId));
                    Bll_Email Bll_Email = new Bll_Email();
                    Bll_Email.EnviarCorreo(Email, "Nueva Participacion en foro", Mesnaje);

                    return true;
                }
                catch (Exception error)
                {
                    Bll_File.EscribirLog(error.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }




    }
}
