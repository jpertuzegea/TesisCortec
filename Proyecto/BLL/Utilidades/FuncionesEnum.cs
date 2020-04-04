using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BLL.Utilidades
{
    public class FuncionesEnum
    {
        public static List<SelectListItem> ListaEnum<T>()
        {
            var t = typeof(T);
            if (!t.IsEnum)
            {
                throw new ApplicationException("El tipo debe ser un Enum");
            }
            var menbers = t.GetFields(BindingFlags.Public | BindingFlags.Static);
            List<SelectListItem> result = new List<SelectListItem>();// se debe importar la dll que esta en el proyecto vista

            foreach (var obj in menbers)
            {
                var attributeDescription = obj.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                var descripcion = obj.Name;

                if (attributeDescription.Any())
                {
                    descripcion = ((System.ComponentModel.DescriptionAttribute)attributeDescription[0]).Description;
                }
                var valor = ((int)Enum.Parse(t, obj.Name));
                result.Add(new SelectListItem() { Text = descripcion, Value = valor.ToString() });
            }
            return result;
        }



    }
}
