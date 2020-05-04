using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Utilidades
{
    public class UtilitiesCommons
    { 
        public static DateTime ObtenerHorayFechaActualLocal()
        { 
            return (DateTime.UtcNow).AddHours(-5); // para colombia, se deben restar 5 horas a la hora utc (Meridiano de Greenwich)
        }

    }
}
