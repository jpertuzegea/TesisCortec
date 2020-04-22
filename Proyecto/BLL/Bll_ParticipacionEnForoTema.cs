using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

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

        public bool GuardarParticipacionEnForoTema(int ForoTemaId, string Mensaje)
        {
            if (ForoTemaId > 0 && Mensaje != null)
            {// si el objeto es diferente de nulo
                try
                {
                    ParticipacionEnForoTema ParticipacionEnForoTema = new ParticipacionEnForoTema();

                    ParticipacionEnForoTema.ParticipanteId = (int)System.Web.HttpContext.Current.Session["IdUsuarioTesis"];
                    ParticipacionEnForoTema.ForoTemaId = ForoTemaId;
                    ParticipacionEnForoTema.Mensaje = Mensaje;
                    ParticipacionEnForoTema.FechaRegistro = DateTime.Now;

                    BD.ParticipacionEnForoTema.Add(ParticipacionEnForoTema);
                    BD.SaveChanges();
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
