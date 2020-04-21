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
    public class Bll_ForoTema
    {

        private TESIS_BD BD = null;

        public Bll_ForoTema()
        {
            BD = new TESIS_BD();
        }

        public ForoTema ObtenerForoTemaByForoTemaId(int ForoTemaId)
        {
            try
            {
                ForoTema ForoTema = null;
                ForoTema = BD.ForoTema.Find(ForoTemaId);

                return ForoTema;
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return null;
            }
        }

        public List<ForoTema> ListarForosTemaByCursoId(int CursoId)
        {
            try
            {
                List<ForoTema> Lista = null;
                Lista = BD.ForoTema.Include(x => x.Personas).Include(y => y.Cursos).Where(x => x.CursoId == CursoId).ToList();

                return Lista;
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return null;
            }
        }

        public bool GuardarForoTema(ForoTema ForoTema)
        {
            if (ForoTema != null)
            {// si el objeto es diferente de nulo
                try
                {
                    ForoTema.DocenteId = (int)HttpContext.Current.Session["IdUsuarioTesis"];
                    ForoTema.FechaRegistro = DateTime.Now;
                    ForoTema.Estado = (byte)EnumEstados.Activo;

                    BD.ForoTema.Add(ForoTema);
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

        public bool ModificarForoTema(ForoTema Foro)
        {
            ForoTema ForoTema = ObtenerForoTemaByForoTemaId(Foro.ForoTemaId);

            if (ForoTema != null)
            {
                try
                {                 
                    ForoTema.Descripcion = Foro.Descripcion;
                    ForoTema.Tema = Foro.Tema;
                    ForoTema.Estado = (byte)Foro.Estado;

                    BD.Entry(ForoTema).State = EntityState.Modified;
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
