using BCrypt.Net;
using BLL.Enums;
using BLL.Utilidades;
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
    public class Bll_Personas
    {
        private TESISCortecEntities BD = null;

        public Bll_Personas()
        {
            BD = new TESISCortecEntities();
        }

        public Personas GetPersonaByPersonaId(int? PersonaId)
        {
            try
            {
                Personas Persona = BD.Personas.Find(PersonaId);
                if (Persona != null)
                {
                    return Persona;
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

        public byte[] GetImagenByPersonaId(int PersonaId)
        {
            try
            {
                Personas Persona = BD.Personas.Find(PersonaId);
                if (Persona != null)
                {
                    return (Persona.Imagen);
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
        public string GetEmailByPersonaId(int PersonaId)
        {
            try
            {
                Personas Persona = BD.Personas.Find(PersonaId);
                if (Persona != null)
                {
                    return (Persona.Email);
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

        public bool ValidaEmailExiste(string Email)
        {
            bool respuesta = BD.Personas.Count(e => e.Email == Email) > 0;
            return respuesta;
        }

        public bool EsCorrectaClave(string ClaveActual, int PersonaId)
        {
            ClaveActual = ClaveActual.ComputeHash(HashType.SHA256);
            bool respuesta = BD.Personas.Count(e => e.Clave == ClaveActual && e.PersonaId == PersonaId) > 0;
            return respuesta;
        }

        public List<Personas> ListarPersonas(EnumEstadoFiltro Filtro)
        {
            try
            {
                List<Personas> Lista = null;

                switch (Filtro)
                {
                    case EnumEstadoFiltro.Activo://Activo
                        Lista = BD.Personas.Where(p => p.Estado == (byte)EnumEstadoFiltro.Activo).ToList();
                        break;

                    case EnumEstadoFiltro.Inactivo://Inactivo
                        Lista = BD.Personas.Where(c => c.Estado == (byte)EnumEstadoFiltro.Inactivo).ToList();
                        break;

                    case EnumEstadoFiltro.Todos:// Todos
                        Lista = BD.Personas.ToList();
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

        public bool GuardarPersona(Personas Persona)
        {
            if (Persona != null)
            {// si el objeto es diferente de nulo
                try
                {
                    if (!ValidaEmailExiste(Persona.Email))
                    {
                        Bll_Codigo Bll_Codigo = new Bll_Codigo();
                        int Codigo = Bll_Codigo.ObtenerCodigo();

                        Persona.CodigoInstitucional = "Cod-" + Codigo;

                        Persona.Clave = Persona.Clave.ComputeHash(HashType.SHA256);
                        Persona.FechaIngreso = UtilitiesCommons.ObtenerHorayFechaActualLocal().Date;
                        Persona.Estado = (byte)EnumEstados.Activo;
                        Persona.RolAcademico = (byte)EnumRolAcademico.Estudiante;

                        // ----------- Aca se guarda la imagen por defecto de Usuario sin Foto  -----------
                        string dataFile = HttpContext.Current.Server.MapPath("~/Imagenes/Sin Foto.jpg");
                        FileStream foto = new FileStream(dataFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        Byte[] arreglo = new Byte[foto.Length];
                        BinaryReader reader = new BinaryReader(foto);
                        arreglo = reader.ReadBytes(Convert.ToInt32(foto.Length));
                        Persona.Imagen = arreglo;
                        // ---------------------------------------------------------------------------------

                        BD.Personas.Add(Persona);
                        Bll_Codigo.GuardarCodigo(Codigo);
                        BD.SaveChanges();

                        AgregaRolAlaPersona(Persona.PersonaId, 3);// Toda nueva persona ingresa con Rol Estudiante

                        string Mesnaje = $"Buen dia señor(a): {Persona.NombreCompleto}." +
                            "\n Se informa que su inscripcion fue realizada de manera exitosa en la plataforma TESIS. \n" +
                            "Sus datos de acceso son : \n " +
                            $"Usuario: {Persona.Email} \n" +
                            $"Clave: ************  \n\n" +
                            "Desde este momento puede disfrutar de nuestros cursos Online \n" +
                            "Feliz resto de dia.";

                        Bll_Email Bll_Email = new Bll_Email();
                        Bll_Email.EnviarCorreo(Persona.Email, "Inscripcion Exitosa Tesis", Mesnaje);
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

        public bool ModificarPersona(Personas Persona)
        {
            Personas Perso = GetPersonaByPersonaId(Persona.PersonaId);

            if (Perso != null)
            {
                try
                {
                    Perso.TipoDocumento = Persona.TipoDocumento;
                    Perso.NumDocumento = Persona.NumDocumento;
                    Perso.NombreCompleto = Persona.NombreCompleto;
                    Perso.CodigoInstitucional = Persona.CodigoInstitucional;
                    Perso.Email = Persona.Email;
                    Perso.Ciudad = Persona.Ciudad;
                    Perso.Departamento = Persona.Departamento;
                    Perso.Direccion = Persona.Direccion;
                    Perso.Telefono = Persona.Telefono;
                    Perso.RolAcademico = (byte)Persona.RolAcademico;

                    if (Persona.Clave != null)
                    {
                        Perso.Clave = Persona.Clave.ComputeHash(HashType.SHA256);
                    }

                    Perso.Estado = Persona.Estado;

                    BD.Entry(Perso).State = EntityState.Modified;
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

        public bool CambioImagen(HttpPostedFileBase file)
        {
            Personas Perso = GetPersonaByPersonaId((int)HttpContext.Current.Session["IdUsuarioTesis"]);

            if (Perso != null)
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

                        Perso.Imagen = imagenData;
                    }

                    BD.Entry(Perso).State = EntityState.Modified;
                    BD.SaveChanges();

                    Bll_File.EscribirLog("Avatar modificado con exito");

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

        public bool CambioClave(string NuevaClave)
        {
            Personas Perso = GetPersonaByPersonaId((int)HttpContext.Current.Session["IdUsuarioTesis"]);

            if (Perso != null)
            {
                try
                {
                    if (NuevaClave != null)
                    {
                        Perso.Clave = NuevaClave.ComputeHash(HashType.SHA256);
                    }

                    BD.Entry(Perso).State = EntityState.Modified;
                    BD.SaveChanges();

                    Bll_File.EscribirLog("Clave modificada con exito");

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

        public List<SelectListItem> ArmarSelectPersona(EnumEstadoFiltro filtro, EnumRolAcademico RolAcademico = EnumRolAcademico.Estudiante, bool Todos = false)
        {
            List<Personas> Lista = null;
            List<SelectListItem> result = new List<SelectListItem>();

            if (Todos)
            {
                Lista = ListarPersonas(EnumEstadoFiltro.Todos).ToList();
            }
            else
            {
                Lista = ListarPersonas(EnumEstadoFiltro.Todos).Where(p => p.RolAcademico == (byte)RolAcademico).ToList();
            }


            if (Lista.Count > 0)
            {
                foreach (var item in Lista)
                {
                    var nombre = item.NombreCompleto;
                    var valor = item.PersonaId;
                    result.Add(new SelectListItem() { Text = nombre, Value = valor.ToString() });
                }
            }

            return result;
        }



        // Roles Y Perfiles
        public ListaRolesDelaPersona ListarRolesDeUnaPersona(int PersonaId)
        {
            try
            {
                ListaRolesDelaPersona ListaRolesDelaPersona = new ListaRolesDelaPersona();

                Bll_Roles Bll_Roles = new Bll_Roles();
                List<Roles> Roles = Bll_Roles.ListarRoles(EnumEstadoFiltro.Todos);
                if (Roles != null)
                {
                    foreach (var item in Roles)
                    {
                        item.EstadoChecbox = VerificarPerfilDelRol(PersonaId, item.RolId);
                    }
                    ListaRolesDelaPersona.PersonaId = PersonaId;
                    ListaRolesDelaPersona.ListaRoles = Roles;
                }
                return ListaRolesDelaPersona;
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return null;
            }
        }

        public bool GestionarRolesDeUnaPersona(ListaRolesDelaPersona Lista)
        {
            try
            {
                if (EliminarRolDelaPersona(Lista.PersonaId))
                {
                    foreach (var item in Lista.ListaRoles.Where(x => x.EstadoChecbox == true))
                    {
                        AgregaRolAlaPersona(Lista.PersonaId, item.RolId);
                    }
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

        public bool EliminarRolDelaPersona(int PersonaId)
        {
            if (PersonaId > 0)
            {
                try
                {
                    ListaRolesDelaPersona lista = ListarRolesDeUnaPersona(PersonaId);
                    if (lista != null)
                    {
                        foreach (var item in lista.ListaRoles.Where(x => x.EstadoChecbox == true))
                        {
                            var PersonaRol = GetPersonaRolByPersonaIdRolId(PersonaId, item.RolId);

                            BD.RolPersona.Remove(PersonaRol);
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

        public RolPersona GetPersonaRolByPersonaIdRolId(int PersonaId, int RolId)
        {
            try
            {
                RolPersona RolPersona = BD.RolPersona.Where(x => x.PersonaId == PersonaId && x.RolId == RolId).FirstOrDefault();
                if (RolPersona != null)
                {
                    return RolPersona;
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

        public bool AgregaRolAlaPersona(int PersonaId, int RolId)
        {
            if (PersonaId > 0 && RolId > 0)
            {// si el objeto es mayor a 0 osea que es un id valido
                try
                {
                    RolPersona RolPersona = new RolPersona();
                    RolPersona.PersonaId = PersonaId;
                    RolPersona.RolId = RolId;
                    BD.RolPersona.Add(RolPersona);
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

        public bool VerificarPerfilDelRol(int PersonaId, int RolId)
        {
            if (RolId > 0 && PersonaId > 0)
            {// si el objeto es mayor a 0 osea que es un id valido
                try
                {
                    List<RolPersona> resultado = BD.RolPersona.Where(r => r.PersonaId == PersonaId && r.RolId == RolId).ToList();
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
