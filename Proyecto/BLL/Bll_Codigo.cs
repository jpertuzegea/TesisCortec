using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Bll_Codigo
    {

        private TESISCortecEntities BD = null;

        public Bll_Codigo()
        {
            BD = new TESISCortecEntities();
        }

        public int ObtenerCodigo()
        {
            try
            {
                var Codigo = BD.Codigos.OrderByDescending(x => x.Contador).First().Contador;
                if (Codigo > 0)
                {
                    return Codigo + 1;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return 0;
            }
        }
        public bool GuardarCodigo(int Codigo)
        { 
            if (Codigo > 0)
            {// si el objeto es diferente de nulo
                try
                {
                    Codigos Codigos = new Codigos();
                    Codigos.Contador = Codigo;
                    BD.Codigos.Add(Codigos);
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