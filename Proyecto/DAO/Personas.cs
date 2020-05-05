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
    
    public partial class Personas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Personas()
        {
            this.CalificacionDocenteCursoEstudiante = new HashSet<CalificacionDocenteCursoEstudiante>();
            this.CertificadoEstudianteCurso = new HashSet<CertificadoEstudianteCurso>();
            this.CursoEstudiante = new HashSet<CursoEstudiante>();
            this.Cursos = new HashSet<Cursos>();
            this.ForoTema = new HashSet<ForoTema>();
            this.MaterialDidactico = new HashSet<MaterialDidactico>();
            this.NotasRapidas = new HashSet<NotasRapidas>();
            this.ParticipacionEnForoTema = new HashSet<ParticipacionEnForoTema>();
            this.SistemaCorreo = new HashSet<SistemaCorreo>();
            this.SistemaCorreo1 = new HashSet<SistemaCorreo>();
        }
    
        public int PersonaId { get; set; }
        public byte[] Imagen { get; set; }
        public byte TipoDocumento { get; set; }
        public string NumDocumento { get; set; }
        public string NombreCompleto { get; set; }
        public string CodigoInstitucional { get; set; }
        public string Email { get; set; }
        public string Ciudad { get; set; }
        public string Departamento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Clave { get; set; }
        public byte RolAcademico { get; set; }
        public System.DateTime FechaIngreso { get; set; }
        public byte Estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CalificacionDocenteCursoEstudiante> CalificacionDocenteCursoEstudiante { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CertificadoEstudianteCurso> CertificadoEstudianteCurso { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CursoEstudiante> CursoEstudiante { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cursos> Cursos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ForoTema> ForoTema { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaterialDidactico> MaterialDidactico { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NotasRapidas> NotasRapidas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParticipacionEnForoTema> ParticipacionEnForoTema { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SistemaCorreo> SistemaCorreo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SistemaCorreo> SistemaCorreo1 { get; set; }
    }
}
