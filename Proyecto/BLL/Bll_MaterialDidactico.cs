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
    public class Bll_MaterialDidactico
    {
        private TESIS_BD BD = null;

        public Bll_MaterialDidactico()
        {
            BD = new TESIS_BD();
        }

        public bool GuardarMaterialDidactico(int CursoId)
        {
            if (CursoId > 0  )
            {// si el objeto es diferente de nulo
                try
                {
                    MaterialDidactico MaterialDidactico = new MaterialDidactico();
                    MaterialDidactico.CursoId = CursoId;
                    MaterialDidactico.DocenteId = (int)System.Web.HttpContext.Current.Session["IdUsuarioTesis"];
                     //MaterialDidactico.Contenido = "";
                    MaterialDidactico.Filename = "";
                    MaterialDidactico.ContentType = "";
                  //  MaterialDidactico.Version = "";
                    MaterialDidactico.FechaRegistro = DateTime.Now;
                    MaterialDidactico.Estado = (byte)EnumEstados.Activo;

                    BD.MaterialDidactico.Add(MaterialDidactico);
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
