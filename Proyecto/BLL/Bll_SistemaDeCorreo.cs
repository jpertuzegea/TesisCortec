using BLL.Enums;
using DAO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public int ObtenerCorreosSinLeerByPersonaId()
        {
            int CorreosSinLeer = 0;
            try
            {
                int PersonaId = (int)HttpContext.Current.Session["IdUsuarioTesis"];
                CorreosSinLeer = BD.SistemaCorreo.Where(d => d.DestinoId == PersonaId && d.EstadoLectura == (byte)EnumEstadoLecturaCorreo.SinLeer).Count();
                HttpContext.Current.Session["MensajesNoLeidos"] = CorreosSinLeer;
                return CorreosSinLeer;
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return CorreosSinLeer;
            }
        }


        public bool MarcarCorreoComoLeidoBySistemaCorreoId(SistemaCorreo SistemaCorreo)
        {
            try
            {
                if (SistemaCorreo != null)
                {
                    if (SistemaCorreo.EstadoLectura == (byte)EnumEstadoLecturaCorreo.SinLeer && SistemaCorreo.DestinoId == (int)HttpContext.Current.Session["IdUsuarioTesis"])
                    {
                        SistemaCorreo.EstadoLectura = (byte)EnumEstadoLecturaCorreo.Leido;
                        SistemaCorreo.FechaLectura = DateTime.Now;

                        BD.Entry(SistemaCorreo).State = EntityState.Modified;
                        BD.SaveChanges(); 
                        ObtenerCorreosSinLeerByPersonaId(); 
                        return true;
                    }
                }
                return false;
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return false;
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
