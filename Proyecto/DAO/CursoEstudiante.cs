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
    
    public partial class CursoEstudiante
    {
        public int CursoEstudianteId { get; set; }
        public int CursoId { get; set; }
        public int EstudianteId { get; set; }
        public System.DateTime FechaMatricula { get; set; }
        public byte EstadoEvaluacionCursoyDocente { get; set; }
        public Nullable<double> Nota1 { get; set; }
        public Nullable<double> Nota2 { get; set; }
        public Nullable<double> Nota3 { get; set; }
        public byte AprobacionCurso { get; set; }
        public byte Estado { get; set; }
    
        public virtual Cursos Cursos { get; set; }
        public virtual Personas Personas { get; set; }
    }
}
