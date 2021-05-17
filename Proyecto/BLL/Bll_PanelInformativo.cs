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
    public class Bll_PanelInformativo
    {

        private TESISCortecEntities BD = null;

        public Bll_PanelInformativo()
        {
            BD = new TESISCortecEntities();
        }

        public PanelInformativo ObtenerPanelInformativoByPanelInformativoId()
        {
            try
            {
                PanelInformativo PanelInformativo = BD.PanelInformativo.Where(x => x.PanelInformativoId == 1).FirstOrDefault();
                if (PanelInformativo != null)
                {
                    return PanelInformativo;
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

        public bool ModificarPanelInformativo(PanelInformativo Panel, HttpPostedFileBase file)
        {
            PanelInformativo PanelInformativo = ObtenerPanelInformativoByPanelInformativoId();

            if (PanelInformativo != null)
            {
                try
                {
                    byte[] imagenData = null;
                    using (var FotoCategoria = new BinaryReader(file.InputStream))
                    {
                        imagenData = FotoCategoria.ReadBytes(file.ContentLength);
                    }
                    PanelInformativo.Imagen = imagenData;
                    PanelInformativo.ContetType = file.ContentType;
                    PanelInformativo.Estado = Panel.Estado;

                    BD.Entry(PanelInformativo).State = EntityState.Modified;
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
