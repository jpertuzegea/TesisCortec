using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Enums
{
    public enum EnumEstadoAcceso
    {
        [Description("Acceso Fallido")]
        Acceso_Fallido = 0, // 0

        [Description("Acceso Exitoso")]
        Acceso_Exitoso = 1
    }
}
