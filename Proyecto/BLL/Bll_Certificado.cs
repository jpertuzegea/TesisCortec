using BLL.Enums;
using BLL.Utilidades;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
    public class Bll_Certificado
    {
        private TESIS_BD BD = null;

        public Bll_Certificado()
        {
            BD = new TESIS_BD();
        }
         
        public CertificadoEstudianteCurso ObtenerCertificadoEstudianteCursoByEstudianteIdCursoId(int CursoId, int EstudianteId)
        {
            CertificadoEstudianteCurso CertificadoEstudianteCurso = null;
            try
            {
                CertificadoEstudianteCurso = BD.CertificadoEstudianteCurso.Where(x => x.CursoId == CursoId && x.EstudianteId == EstudianteId).FirstOrDefault();

                return CertificadoEstudianteCurso;
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return null;
            }
        }

        public bool ImprimirCertificado(int CursoId, int EstudianteId = 0)
        {
            try
            {
                Bll_Login Bll_Login = new Bll_Login();
                Bll_Cursos Bll_Cursos = new Bll_Cursos();
                Bll_Personas Bll_Personas = new Bll_Personas();

                int DuracionCookie = 15;
                Personas Persona = null;
                CertificadoEstudianteCurso CertificadoEstudianteCurso = null;

                if (EstudianteId == 0)
                {
                    EstudianteId = (int)HttpContext.Current.Session["IdUsuarioTesis"];
                }

                CertificadoEstudianteCurso = ObtenerCertificadoEstudianteCursoByEstudianteIdCursoId(CursoId, EstudianteId);

                if (CertificadoEstudianteCurso != null)
                {
                    Bll_Login.CrearCookie("Respuesta", "true", DuracionCookie);
                    Bll_Login.CrearCookie("NombreCurso", CertificadoEstudianteCurso.Cursos.Nombre, DuracionCookie);
                    Bll_Login.CrearCookie("CodigoCurso", CertificadoEstudianteCurso.Cursos.Codigo, DuracionCookie);
                    Bll_Login.CrearCookie("NombreEstudiante", CertificadoEstudianteCurso.Personas.NombreCompleto, DuracionCookie);
                    Bll_Login.CrearCookie("TipoDocumentoEstudiante", CertificadoEstudianteCurso.Personas.TipoDocumento.ToString(), DuracionCookie);
                    Bll_Login.CrearCookie("NumeroDocumentoEstudiante", CertificadoEstudianteCurso.Personas.NumDocumento, DuracionCookie);

                    return true;
                }
                else
                {
                    Cursos Curso = Bll_Cursos.GetCursoByCursoId(CursoId);
                    Persona = Bll_Personas.GetPersonaByPersonaId(EstudianteId);
                    EnumTipoDocumento TipoDocumento = (EnumTipoDocumento)Persona.TipoDocumento;

                    if (Curso != null && Persona != null)
                    {
                        CertificadoEstudianteCurso certificadoEstudianteCurso = new CertificadoEstudianteCurso();
                        certificadoEstudianteCurso.CursoId = Curso.CursoId;
                        certificadoEstudianteCurso.EstudianteId = EstudianteId;
                        certificadoEstudianteCurso.FechaAprobacion = UtilitiesCommons.ObtenerHorayFechaActualLocal();
                        BD.CertificadoEstudianteCurso.Add(certificadoEstudianteCurso);
                        BD.SaveChanges();

                        Bll_Login.CrearCookie("Respuesta", "true", DuracionCookie);
                        Bll_Login.CrearCookie("NombreCurso", Curso.Nombre, DuracionCookie);
                        Bll_Login.CrearCookie("CodigoCurso", Curso.Codigo, DuracionCookie);
                        Bll_Login.CrearCookie("NombreEstudiante", Persona.NombreCompleto, DuracionCookie);
                        Bll_Login.CrearCookie("TipoDocumentoEstudiante", TipoDocumento.ToString(), DuracionCookie);
                        Bll_Login.CrearCookie("NumeroDocumentoEstudiante", Persona.NumDocumento, DuracionCookie);

                        return true;
                    }
                    else
                    {
                        Bll_Login.CrearCookie("Respuesta", "false", DuracionCookie);// si NO encontro el codigo se envia false 
                        return false;
                    }
                }
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return false;
            }

        }
    }
}
