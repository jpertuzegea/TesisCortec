using BLL.Enums;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
    public class Bll_Perfiles
    {
        private TESISCortecEntities BD = null;

        public Bll_Perfiles()
        {
            BD = new TESISCortecEntities();
        }

        public List<Perfiles> ListarPerfiles()
        {
            try
            {
                List<Perfiles> Lista = BD.Perfiles.ToList();
                return (Lista);
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                throw;
            }
        }

        public static bool VerificarPerfil(EnumPerfilesActivos EnumPerfilesActivos)
        { 
            try
            {
                int PerfilId = (int)EnumPerfilesActivos;
                TESISCortecEntities Bd = new TESISCortecEntities();
                int UserId = (int)HttpContext.Current.Session["IdUsuarioTesis"];

                // esta consulta es para verificar si un usuario ingresado tiene el perfil por el cual se solicita permiso
                var resultado = from perf in Bd.Perfiles
                                join ro_pe in Bd.RolPerfil on perf.PerfilId equals ro_pe.PerfilId
                                join rol in Bd.Roles on ro_pe.RolId equals rol.RolId
                                join rol_usu in Bd.RolPersona on rol.RolId equals rol_usu.RolId
                                join usu in Bd.Personas on rol_usu.PersonaId equals usu.PersonaId
                                where perf.PerfilId == PerfilId && usu.PersonaId == UserId
                                select new
                                {
                                    nombre = perf.NombrePerfil,
                                    usu.NombreCompleto
                                }; //  query de validacion de usuario y perfil

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

    }
}
