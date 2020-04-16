using BLL.Enums;
using DAO;
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

        // metodo para listar los Cursos existentes
        public List<Cursos> ListarCursos(EnumEstadoFiltro Filtro)
        {
            try
            {
                List<Cursos> ListCursos = null;

                switch (Filtro)
                {
                    case EnumEstadoFiltro.Activo://Activo
                        ListCursos = BD.Cursos.Where(c => c.Estado == (byte)EnumEstados.Activo).ToList();
                        break;

                    case EnumEstadoFiltro.Inactivo://Inactivo
                        ListCursos = BD.Cursos.Where(c => c.Estado == (byte)EnumEstados.Inactivo).ToList();
                        break;

                    case EnumEstadoFiltro.Todos:// Todos
                        ListCursos = BD.Cursos.ToList();
                        break;
                }

                return (ListCursos);// retorna una lista de entidades
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return null;
            }
        }

        // Metodo para listar los Cursos existentes que se encentren en estado activo y  la fecha este vigente
        public List<Cursos> VisualizarCursos()
        {
            try
            {
                List<Cursos> ListCursos = BD.Cursos.Where(c => c.Estado == (byte)EnumEstados.Activo).Include(x => x.Personas).ToList();
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
                    Cur.Descripcion = Curso.Descripcion;
                    Cur.TituloOtorgado = Curso.TituloOtorgado;
                    Cur.ValorCurso = Curso.ValorCurso;
                    Cur.DuracionHoras = Curso.DuracionHoras;
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


        public bool Matricularse(int CursoId, string Nombre, string Codigo)
        {
            int EstudianteId = (int)HttpContext.Current.Session["IdUsuarioTesis"];
            string Mesnaje =
                         $"Buen dia señor(a): {EstudianteId}.\n" +
                         $"Se informa que su matricula en el curso [ {Nombre} ] con codigo: [ { Codigo} ], se ha realizado de manera exitosa. \n" +
                         $"Fecha Matricula: {DateTime.Now} \n " +
                         "Gracias por su pago, le deseamos exito en este nuevo curso. \n " +

                         "Despues de 24 horas, el curso estara disponible en su perfil. \n" +
                         "Feliz resto de dia.";

            string Email = new Bll_Personas().GetEmailByPersonaId(EstudianteId);
             
            Bll_CursoEstudiante Bll_CursoEstudiante = new Bll_CursoEstudiante();
            Bll_CursoEstudiante.GuardarCursoEstudiante(CursoId, EstudianteId);
             
            Bll_Email Bll_Email = new Bll_Email();
            Bll_Email.EnviarCorreo(Email, "Matricula Exitosa", Mesnaje);

            return true;
        }

        // Arma un select list de Cursos, con la propiedad value y name 
        public List<SelectListItem> ArmarSelectClientes(EnumEstadoFiltro filtro)
        {
            List<Cursos> Lista = null;
            Lista = ListarCursos(EnumEstadoFiltro.Todos);

            List<SelectListItem> result = new List<SelectListItem>();
            foreach (var item in Lista)
            {
                var nombre = item.Nombre;
                var valor = item.CursoId;
                result.Add(new SelectListItem() { Text = nombre, Value = valor.ToString() });
            }
            return result;
        }
    }
}