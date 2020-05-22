using BLL.Enums;
using DAO;
using DAO.ViewModel;
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
                return null;
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
         
        public ListaPerfilesDelRol ListarPerfilesDeUnRol(int RolId)
        {
            try
            {
                ListaPerfilesDelRol ListaPerfilesDelRol = new ListaPerfilesDelRol();
                Bll_Perfiles Bll_Perfiles = new Bll_Perfiles();
                List<Perfiles> Perfiles = Bll_Perfiles.ListarPerfiles();
                if (Perfiles != null)
                {
                    foreach (var item in Perfiles)
                    {
                        item.EstadoChecbox = VerificarPerfilDelRol(RolId, item.PerfilId);
                    }
                    ListaPerfilesDelRol.RolId = RolId;
                    ListaPerfilesDelRol.ListaPerfiles = Perfiles;
                }
                return ListaPerfilesDelRol;
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return null;
            }
        }

        public bool GestionarPerfilesDelRol(ListaPerfilesDelRol Lista)
        {
            try
            {
                EliminarPerfilesDelRol(Lista.RolId);
                foreach (var item in Lista.ListaPerfiles.Where(x => x.EstadoChecbox == true))
                {
                    AgregarPerfilAlRol(Lista.RolId, item.PerfilId);
                }
                return true;
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return false;
            } 
        }
         
        public bool EliminarPerfilesDelRol(int RolId)
        {
            if (RolId > 0)
            {
                try
                {
                    ListaPerfilesDelRol lista = ListarPerfilesDeUnRol(RolId);
                    if (lista != null)
                    {
                        foreach (var item in lista.ListaPerfiles.Where(x => x.EstadoChecbox == true))
                        {
                            var RolPerfil = GetRolPerfilByRolIdPerfilId(RolId, item.PerfilId);

                            BD.RolPerfil.Remove(RolPerfil);
                        }
                        BD.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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

        public RolPerfil GetRolPerfilByRolIdPerfilId(int RolId, int PerfilId)
        {
            try
            {
                RolPerfil RolPerfil = BD.RolPerfil.Where(x => x.RolId == RolId && x.PerfilId == PerfilId).FirstOrDefault();
                if (RolPerfil != null)
                {
                    return RolPerfil;
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
          
        public bool AgregarPerfilAlRol(int RolId, int PerfilId)
        {
            if (PerfilId > 0 && RolId > 0)
            {// si el objeto es mayor a 0 osea que es un id valido
                try
                {
                    RolPerfil RolPerfil = new RolPerfil();
                    RolPerfil.PerfilId = PerfilId;
                    RolPerfil.RolId = RolId;
                    BD.RolPerfil.Add(RolPerfil);
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
         
        public bool VerificarPerfilDelRol(int RolId, int PerfilId)
        {
            if (RolId > 0 && PerfilId > 0)
            {// si el objeto es mayor a 0 osea que es un id valido
                try
                {
                    List<RolPerfil> resultado = BD.RolPerfil.Where(r => r.PerfilId == PerfilId && r.RolId == RolId).ToList();
                    if (resultado.Count() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
