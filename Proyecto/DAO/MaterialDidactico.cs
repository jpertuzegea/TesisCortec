//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAO
{
    using System;
    using System.Collections.Generic;
    
    public partial class MaterialDidactico
    {
        public int MaterialDidacticoId { get; set; }
        public int CursoId { get; set; }
        public int DocenteId { get; set; }
        public byte[] Contenido { get; set; }
        public string Filename { get; set; }
        public string Mensaje { get; set; }
        public string ContentType { get; set; }
        public int Version { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public byte Estado { get; set; }
    
        public virtual Cursos Cursos { get; set; }
        public virtual Personas Personas { get; set; }
    }
}
