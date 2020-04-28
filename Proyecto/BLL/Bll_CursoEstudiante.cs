using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BLL.Enums;

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

        public bool Matricularse(int CursoId, string Nombre, string Codigo)
        {
            if (CursoId > 0 && Nombre.Length > 3 && Codigo.Length > 0)
            {// si el objeto es diferente de nulo
                try
                {
                    int EstudianteId = (int)HttpContext.Current.Session["IdUsuarioTesis"];

                    CursoEstudiante CursoEstudiante = new CursoEstudiante();

                    CursoEstudiante.CursoId = CursoId;
                    CursoEstudiante.EstudianteId = EstudianteId;
                    CursoEstudiante.FechaMatricula = DateTime.Now;
                    CursoEstudiante.AprobacionCurso = (byte)EnumAprobacionCurso.Cursando;
                    CursoEstudiante.EstadoEvaluacionCursoyDocente = (byte)EnumEstadoEvaluacionCursoyDocente.Pendiente;
                    CursoEstudiante.Estado = (byte)EnumEstados.Activo;

                    BD.CursoEstudiante.Add(CursoEstudiante);
                    BD.SaveChanges();

                    HttpContext.Current.Session["CursosMatriculadosActivos"] = (int)HttpContext.Current.Session["CursosMatriculadosActivos"] + 1;

                    string Mesnaje =
                                 $"Buen dia señor(a): {EstudianteId}.\n" +
                                 $"Se informa que su matricula en el curso [ {Nombre} ] con codigo: [ { Codigo} ], se ha realizado de manera exitosa. \n" +
                                 $"Fecha Matricula: {DateTime.Now} \n " +
                                 "Gracias por su pago, le deseamos exito en este nuevo curso. \n " +

                                 "Despues de 24 horas, el curso estara disponible en su perfil. \n" +
                                 "Feliz resto de dia.";

                    string Email = new Bll_Personas().GetEmailByPersonaId(EstudianteId);
                    Bll_Email Bll_Email = new Bll_Email();
                    Bll_Email.EnviarCorreo(Email, "Matricula Exitosa", Mesnaje);

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
