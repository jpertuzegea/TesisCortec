using BLL.Enums;
using DAO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        // metodo para crear un Curso
        public Boolean GuardarCursos(Cursos Curso)
        {
            if (Curso != null)
            {// si el objeto es diferente de nulo
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

        public Boolean ModificarCursos(Cursos Curso)
        {
            Cursos Cur = GetCursoByCursoId(Curso.CursoId);

            if (Cur != null)
            {
                try
                {
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