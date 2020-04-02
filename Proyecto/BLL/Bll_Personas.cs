using BLL.Enums;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Bll_Personas
    {
        private TESIS_BD BD = null;

        public Bll_Personas()
        {
            BD = new TESIS_BD();
        }

        public List<Personas> ListarPersonas(EnumEstados Filtro)
        {
            try
            {
                List<Personas> Lista = null;

                switch (Filtro)
                {
                    case EnumEstados.Activo://Activo
                        Lista = BD.Personas.Where(p => p.Estado == (byte)EnumEstados.Activo).ToList();
                        break;

                    case EnumEstados.Inactivo://Inactivo
                        Lista = BD.Personas.Where(c => c.Estado == (byte)EnumEstados.Inactivo).ToList();
                        break;

                    case EnumEstados.Todos:// Todos
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

        public Personas GetImagenByPersonaId(int PersonaId)
        {
            Personas Persona = BD.Personas.Find(PersonaId);
            return Persona;
        }


    }
}
