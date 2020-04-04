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

namespace BLL
{
    public class Bll_Personas
    {
        private TESIS_BD BD = null;

        public Bll_Personas()
        {
            BD = new TESIS_BD();
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

        public Personas GetImagenByPersonaId(int PersonaId)
        {
            Personas Persona = BD.Personas.Find(PersonaId);
            return Persona;
        }

        public bool ValidaEmailExiste(string Email)
        {
            bool respuesta = BD.Personas.Count(e => e.Email == Email) > 0;
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

                        Persona.FechaIngreso = DateTime.Now.Date;
                        Persona.Estado = (byte)EnumEstados.Activo;

                        BD.Personas.Add(Persona);
                        Bll_Codigo.GuardarCodigo(Codigo);
                        BD.SaveChanges();

                        string Mesnaje = $"Buen dia señor(a): {Persona.NombreCompleto}." +
                            "\n Se informa que su inscripcion fue realizada de manera exitosa en la plataforma TESIS. \n" +
                            "Sus datos de acceso son : \n " +
                            $"Usuario: {Persona.Email} \n" +
                            $"Clave: {Persona.Clave} \n\n" +
                            "Desde este momento puede disfrutar de nustros curos Online \n" +
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
                    Perso.Clave = Persona.Clave;
                    Perso.Estado = Persona.Estado;

                    BD.Entry(Perso).State = EntityState.Modified;
                    BD.SaveChanges();

                    Bll_File.EscribirLog($"Persona modificada con exito");

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
