using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web;
using BLL.Enums;

namespace BLL
{
    public class Bll_Foro
    {

        private TESIS_BD BD = null;

        public Bll_Foro()
        {
            BD = new TESIS_BD();
        }

        public List<Foro> ListarForosByCursoId(int CursoId)
        {
            try
            {
                List<Foro> Lista = null;
                Lista = BD.Foro.Include(x => x.Personas).Include(y => y.Cursos).Where(x => x.CursoId == CursoId).ToList();

                return Lista;
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return null;
            }
        }





        public bool GuardarForo(Foro Foro)
        {
            if (Foro != null)
            {// si el objeto es diferente de nulo
                try
                {
                    Foro.DocenteId = (int)HttpContext.Current.Session["IdUsuarioTesis"];
                    Foro.FechaRegistro = DateTime.Now;
                    Foro.Estado = (byte)EnumEstados.Activo;

                    BD.Foro.Add(Foro);
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
