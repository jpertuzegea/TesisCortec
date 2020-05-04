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
    public class Bll_IngresoAlSistema
    {
        private TESIS_BD BD = null;
        private IngresosAlSistema IngresosAlSistema = null;

        public Bll_IngresoAlSistema()
        {
            BD = new TESIS_BD();
            IngresosAlSistema = new IngresosAlSistema();
        }

        // Metodo para listar los ingresos al sistema existentes
        public List<IngresosAlSistema> ListIngresosAlSistema()
        {
            try
            {
                List<IngresosAlSistema> ListIngresosAlSistema = BD.IngresosAlSistema.ToList();// obtiene una lista de entidades
                return (ListIngresosAlSistema);
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                throw;
            }
        }

        // Metodo para registrar los intentos de accesos al sistema
        public void RegistroIngresoAlSitema(string UsuarioRed, EnumEstadoAcceso Estado)
        {
            try
            {
                IngresosAlSistema.Usuario = UsuarioRed;
                IngresosAlSistema.FechaIntento = UtilitiesCommons.ObtenerHorayFechaActualLocal();
                IngresosAlSistema.IP_Origen = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                IngresosAlSistema.EstadoAcceso = (byte)Estado;

                BD.IngresosAlSistema.Add(IngresosAlSistema);
                BD.SaveChanges();
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
            }
        }
    }
}
