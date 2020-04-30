using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
    public class Bll_SistemaDeCorreo
    {
        private TESIS_BD BD = null;

        public Bll_SistemaDeCorreo()
        {
            BD = new TESIS_BD();
        }

        public SistemaCorreo ObtenerCorreoBySistemaCorreoId(int SistemaCorreoId)
        {
            SistemaCorreo SistemaCorreo = null;
            try
            {
                if (SistemaCorreoId > 0)
                {
                    SistemaCorreo = BD.SistemaCorreo.Find(SistemaCorreoId);                  
                }
                return SistemaCorreo;
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return SistemaCorreo;
            }
        }

        public bool EnviarCorreo(SistemaCorreo SistemaCorreo)
        {
            try
            {
                SistemaCorreo.FechaEnvio = DateTime.Now;
                SistemaCorreo.RemitenteId = (int)HttpContext.Current.Session["IdUsuarioTesis"];
                BD.SistemaCorreo.Add(SistemaCorreo);
                BD.SaveChanges();
                return true;
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return false;
            }
        }

        public List<SistemaCorreo> BandejaEntrada()
        {
            try
            {
                int DestinatarioId = (int)HttpContext.Current.Session["IdUsuarioTesis"];
                List<SistemaCorreo> Lista = BD.SistemaCorreo.Where(d => d.DestinoId == DestinatarioId).ToList();
                return Lista;
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return null;
            }
        }

        public List<SistemaCorreo> BandejaSalida()
        {
            try
            {
                int RemitenteId = (int)HttpContext.Current.Session["IdUsuarioTesis"];
                List<SistemaCorreo> Lista = BD.SistemaCorreo.Where(d => d.RemitenteId == RemitenteId).ToList();
                return Lista;
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return null;
            }
        }



    }
}
