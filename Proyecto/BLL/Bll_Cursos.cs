using BLL.Enums;
using DAO;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BLL
{
    public class Bll_Cursos
    {
        private TESIS_BD BD = null;

        public Bll_Cursos()
        {
            BD = new TESIS_BD();
        }

        // Metodo para listar los Cursos existentes
        public List<Cursos> ListarCursos(EnumEstadoFiltro Filtro, EnumEstadosCurso EstadosCurso)
        {
            List<Cursos> ListCursos = new List<Cursos>();
            try
            {
                int IdUsuarioTesis = 0;
                IdUsuarioTesis = (int)HttpContext.Current.Session["IdUsuarioTesis"];

                switch (Filtro)
                {
                    case EnumEstadoFiltro.Activo://Activo
                        ListCursos = BD.Cursos.Where(c => c.Estado == (byte)EnumEstados.Activo && c.EstadoAcademico == (byte)EstadosCurso).ToList();
                        break;

                    case EnumEstadoFiltro.Inactivo://Inactivo
                        ListCursos = BD.Cursos.Where(c => c.Estado == (byte)EnumEstados.Inactivo && c.EstadoAcademico == (byte)EstadosCurso).ToList();
                        break;

                    case EnumEstadoFiltro.Todos:// Todos
                        ListCursos = BD.Cursos.ToList();
                        break;
                }

                if (IdUsuarioTesis > 0)
                {
                    List<CursoEstudiante> Lista;
                    Lista = BD.CursoEstudiante.ToList();

                    List<Cursos> ListCursosNoMatriculados = new List<Cursos>();

                    foreach (var item in ListCursos)
                    {
                        bool EstaMatriculado = Lista.Where(x => x.CursoId == item.CursoId && x.EstudianteId == IdUsuarioTesis).Count() > 0;

                        if (!EstaMatriculado)
                        {
                            ListCursosNoMatriculados.Add(item);
                        }
                    }
                    return (ListCursosNoMatriculados); 
                }
                else {
                    return (ListCursos);// retorna una lista de entidades 
                } 
            }
            catch (Exception error)
            {
                // Bll_File.EscribirLog(error.ToString());
                return ListCursos;
            }
        }

        public List<Cursos> ListarCursosByDocenteId(EnumEstadoFiltro Filtro, int DocenteId)
        {
            try
            {
                List<Cursos> ListCursos = null;

                switch (Filtro)
                {
                    case EnumEstadoFiltro.Activo://Activo
                        ListCursos = BD.Cursos.Where(c => c.Estado == (byte)EnumEstados.Activo && c.Docente == DocenteId).ToList();
                        break;

                    case EnumEstadoFiltro.Inactivo://Inactivo
                        ListCursos = BD.Cursos.Where(c => c.Estado == (byte)EnumEstados.Activo && c.Docente == DocenteId).ToList();
                        break;

                    case EnumEstadoFiltro.Todos:// Todos
                        ListCursos = BD.Cursos.ToList();
                        break;
                }

                return (ListCursos);
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return null;
            }
        }

        // metodo para buscar un solo Curso por id
        public Cursos GetCursoByCursoId(int CursoId)
        {
            try
            {
                Cursos Cursos = BD.Cursos.Find(CursoId);
                if (Cursos != null)
                {
                    return (Cursos);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return null;
            }
        }

        public byte[] GetImagenByCursoId(int CursoId)
        {
            try
            {
                Cursos Cursos = BD.Cursos.Find(CursoId);
                if (Cursos != null)
                {
                    return (Cursos.Imagen);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return null;
            }
        }

        // metodo para crear un Curso
        public bool GuardarCursos(Cursos Curso, HttpPostedFileBase file)
        {
            if (Curso != null)
            {// si el objeto es diferente de nulo

                if (file != null && file.ContentLength > 0)
                {
                    byte[] imagenData = null;
                    using (var foto_Persona = new BinaryReader(file.InputStream))
                    {
                        imagenData = foto_Persona.ReadBytes(file.ContentLength);
                    }
                    Curso.Imagen = imagenData;
                }

                try
                {
                    Curso.EstadoAcademico = (byte)EnumEstadosCurso.Ofertado;
                    BD.Cursos.Add(Curso);
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

        public bool ModificarCursos(Cursos Curso, HttpPostedFileBase file)
        {
            Cursos Cur = GetCursoByCursoId(Curso.CursoId);

            if (Cur != null)
            {
                try
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        byte[] imagenData = null;
                        using (var foto_Persona = new BinaryReader(file.InputStream))
                        {
                            imagenData = foto_Persona.ReadBytes(file.ContentLength);
                        }
                        Cur.Imagen = imagenData;
                    }

                    Cur.Nombre = Curso.Nombre;
                    Cur.Docente = Curso.Docente;
                    Cur.Descripcion = Curso.Descripcion;
                    Cur.TituloOtorgado = Curso.TituloOtorgado;
                    Cur.ValorCurso = Curso.ValorCurso;
                    Cur.DuracionHoras = Curso.DuracionHoras;
                    Cur.EstadoAcademico = Curso.EstadoAcademico;
                    Cur.Estado = Curso.Estado;

                    BD.Entry(Cur).State = EntityState.Modified;
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