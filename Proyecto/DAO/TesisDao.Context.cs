﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TESIS_BD : DbContext
    {
        public TESIS_BD()
            : base("name=TESIS_BD")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CalificacionDocenteCursoEstudiante> CalificacionDocenteCursoEstudiante { get; set; }
        public virtual DbSet<CalificacioneEstudiante> CalificacioneEstudiante { get; set; }
        public virtual DbSet<CertificadoEstudianteCurso> CertificadoEstudianteCurso { get; set; }
        public virtual DbSet<Codigos> Codigos { get; set; }
        public virtual DbSet<CronogramaActividadesCurso> CronogramaActividadesCurso { get; set; }
        public virtual DbSet<CursoEstudiante> CursoEstudiante { get; set; }
        public virtual DbSet<Cursos> Cursos { get; set; }
        public virtual DbSet<EvidenciaCorreo> EvidenciaCorreo { get; set; }
        public virtual DbSet<ForoTema> ForoTema { get; set; }
        public virtual DbSet<IngresosAlSistema> IngresosAlSistema { get; set; }
        public virtual DbSet<MaterialDidactico> MaterialDidactico { get; set; }
        public virtual DbSet<NotasRapidas> NotasRapidas { get; set; }
        public virtual DbSet<PanelInformativo> PanelInformativo { get; set; }
        public virtual DbSet<ParticipacionEnForoTema> ParticipacionEnForoTema { get; set; }
        public virtual DbSet<Personas> Personas { get; set; }
        public virtual DbSet<PreguntasCalificacionCurso> PreguntasCalificacionCurso { get; set; }
        public virtual DbSet<SistemaCorreo> SistemaCorreo { get; set; }
    }
}
