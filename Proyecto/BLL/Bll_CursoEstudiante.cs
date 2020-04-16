using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
    public class Bll_CursoEstudiante
    {

        private TESIS_BD BD = null;

        public Bll_CursoEstudiante()
        {
            BD = new TESIS_BD();
        }

        public List<CursoEstudiante> ListarCursosActivosbyPersonaId(int PersonaId)
        {
            try
            {
                List<CursoEstudiante> Lista = null;
                Lista = BD.CursoEstudiante.Include(x => x.Personas).Include(y => y.Cursos).Where(x => x.EstudianteId == PersonaId).ToList();
                 
                return Lista; 
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return null;
            }
        }

        public int ObtenerCantidadCusosActivosByPersonaId(int PersonaId)
        {
            try
            {
                int Cantidad = BD.CursoEstudiante.Where(x => x.EstudianteId == PersonaId).Count();

                if (Cantidad > 0)
                {
                    return Cantidad;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return 0;
            }
        }

        public bool GuardarCursoEstudiante(int CursoId, int EstudianteId)
        {
            if (CursoId > 0 && EstudianteId > 0)
            {// si el objeto es diferente de nulo
                try
                {
                    CursoEstudiante CursoEstudiante = new CursoEstudiante();
                    CursoEstudiante.CursoId = CursoId;
                    CursoEstudiante.EstudianteId = EstudianteId;
                    CursoEstudiante.FechaMatricula = DateTime.Now;

                    BD.CursoEstudiante.Add(CursoEstudiante);
                    BD.SaveChanges();
                    HttpContext.Current.Session["CursosMatriculadosActivos"] = (int)HttpContext.Current.Session["CursosMatriculadosActivos"] + 1;
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
