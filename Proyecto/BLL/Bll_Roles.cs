using BLL.Enums;
using DAO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Bll_Roles
    {

        private TESIS_BD BD = null;

        public Bll_Roles()
        {
            BD = new TESIS_BD();
        }
         
        public Roles GetRolByRolId(int? RolId)
        {
            try
            {
                Roles Rol = BD.Roles.Find(RolId);
                if (Rol != null)
                {
                    return Rol;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                throw;
            }
        }
         
        public List<Roles> ListarRoles(EnumEstadoFiltro Filtro)
        {
            try
            {
                List<Roles> Lista = null;

                switch (Filtro)
                {
                    case EnumEstadoFiltro.Activo://Activo
                        Lista = BD.Roles.Where(p => p.Estado == (byte)EnumEstadoFiltro.Activo).ToList();
                        break;

                    case EnumEstadoFiltro.Inactivo://Inactivo
                        Lista = BD.Roles.Where(c => c.Estado == (byte)EnumEstadoFiltro.Inactivo).ToList();
                        break;

                    case EnumEstadoFiltro.Todos:// Todos
                        Lista = BD.Roles.ToList();
                        break;
                }

                return (Lista);// retorna una lista de entidades
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                throw;
            }
        }

        public bool GuardarRol(Roles Rol)
        {
            if (Rol != null)
            {// si el objeto es diferente de nulo
                try
                {
                    BD.Roles.Add(Rol);
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

        public bool ModificarRol(Roles Rol)
        {
            Roles Roles = GetRolByRolId(Rol.RolId);

            if (Roles != null)
            {
                try
                { 
                    Roles.Nombre = Rol.Nombre;
                    Roles.Descripcion = Rol.Descripcion;
                    Roles.Estado = Rol.Estado; 
                    BD.Entry(Roles).State = EntityState.Modified;
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
