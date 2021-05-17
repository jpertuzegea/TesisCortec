using BLL.Enums;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Bll_EvidenciaCorreo
    {
        private TESISCortecEntities BD = null;

        public Bll_EvidenciaCorreo()
        {
            BD = new TESISCortecEntities();
        }

        public List<EvidenciaCorreo> ListarEvidenciaCorreo()
        {
            try
            {
                List<EvidenciaCorreo> Lista = null;
                Lista = BD.EvidenciaCorreo.ToList();

                return (Lista);
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return null;
            }
        }

        public bool GuardarEvidenciaCorreo(EvidenciaCorreo EvidenciaCorreo)
        {
            if (EvidenciaCorreo != null)
            {// si el objeto es diferente de nulo
                try
                {
                    BD.EvidenciaCorreo.Add(EvidenciaCorreo);
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
