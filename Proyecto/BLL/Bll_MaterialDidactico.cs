using BLL.Enums;
using DAO;
using System;
using System.Collections.Generic;
using System.IO;
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

        public bool GuardarMaterialDidactico(MaterialDidactico MaterialDidactico, HttpPostedFileBase file)
        {
            if (MaterialDidactico.CursoId > 0)
            {// si el objeto es diferente de nulo

                if (file != null && file.ContentLength > 0)
                {
                    BinaryReader bx = new BinaryReader(file.InputStream);
                    byte[] contenido = bx.ReadBytes(file.ContentLength);
                    string filename_con_extension = file.FileName;
                    // string filename = Path.GetFileNameWithoutExtension(filename_con_extension);
                    string ContentType = file.ContentType;
                    string extencion = Path.GetExtension(file.FileName);

                    MaterialDidactico.CursoId = MaterialDidactico.CursoId;
                    MaterialDidactico.DocenteId = (int)System.Web.HttpContext.Current.Session["IdUsuarioTesis"];
                    MaterialDidactico.Contenido = contenido;
                    MaterialDidactico.Filename = MaterialDidactico.Filename + extencion;
                    MaterialDidactico.ContentType = ContentType;
                    MaterialDidactico.Version = 1;
                    MaterialDidactico.FechaRegistro = DateTime.Now;
                    MaterialDidactico.Estado = (byte)EnumEstados.Activo;
                }

                try
                {
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


        public List<MaterialDidactico> ListarMaterialByCursoId(int CursoId)
        {
            try
            {
                List<MaterialDidactico> MaterialDidactico = null;
                MaterialDidactico = BD.MaterialDidactico.Where(x => x.CursoId == CursoId).ToList();
                return (MaterialDidactico);
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return null;
            }

        }

        public MaterialDidactico ObtenerDocumentoByMaterialDidacticoId(int MaterialDidacticoId)
        {
            try
            {
                MaterialDidactico MaterialDidactico = null;
                MaterialDidactico = BD.MaterialDidactico.Find(MaterialDidacticoId);
                return (MaterialDidactico);
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return null;
            }

        }






    }
}
