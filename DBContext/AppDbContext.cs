using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SophosUniversityApi.Models;

namespace SophosUniversityApi.DBContext;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asignatura> Asignaturas { get; set; }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Facultad> Facultads { get; set; }

    public virtual DbSet<Inscripcion> Inscripcions { get; set; }

    public virtual DbSet<Profesor> Profesors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asignatura>(entity =>
        {
            entity.HasKey(e => e.IdAsignatura).HasName("PK__Asignatu__33A22F4C2D86B1FF");

            entity.ToTable("Asignatura");

            entity.Property(e => e.IdAsignatura).HasColumnName("id_asignatura");
            entity.Property(e => e.Creditos).HasColumnName("creditos");
            entity.Property(e => e.IdFacultad).HasColumnName("id_facultad");
            entity.Property(e => e.IdPreRequisito).HasColumnName("id_pre_requisito");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdFacultadNavigation).WithMany(p => p.Asignaturas)
                .HasForeignKey(d => d.IdFacultad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Asignatur__id_fa__3E52440B");

            entity.HasOne(d => d.IdPreRequisitoNavigation).WithMany(p => p.InverseIdPreRequisitoNavigation)
                .HasForeignKey(d => d.IdPreRequisito)
                .HasConstraintName("FK__Asignatur__id_pr__3F466844");
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.IdCurso).HasName("PK__Curso__5D3F7502CB96F82E");

            entity.ToTable("Curso");

            entity.Property(e => e.IdCurso).HasColumnName("id_curso");
            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("activo");
            entity.Property(e => e.Cupos).HasColumnName("cupos");
            entity.Property(e => e.IdAsignatura).HasColumnName("id_asignatura");
            entity.Property(e => e.IdProfesor).HasColumnName("id_profesor");
            entity.Property(e => e.Periodo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("periodo");

            entity.HasOne(d => d.IdAsignaturaNavigation).WithMany(p => p.Cursos)
                .HasForeignKey(d => d.IdAsignatura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Curso__id_asigna__4316F928");

            entity.HasOne(d => d.IdProfesorNavigation).WithMany(p => p.Cursos)
                .HasForeignKey(d => d.IdProfesor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Curso__id_profes__440B1D61");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.IdEstudiante).HasName("PK__Estudian__E0B2763CC12AEB2A");

            entity.ToTable("Estudiante");

            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.CreditosInscritos).HasColumnName("creditos_inscritos");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.IdFacultad).HasColumnName("id_facultad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.SemestreActual).HasColumnName("semestre_actual");

            entity.HasOne(d => d.IdFacultadNavigation).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.IdFacultad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estudiant__id_fa__3B75D760");
        });

        modelBuilder.Entity<Facultad>(entity =>
        {
            entity.HasKey(e => e.IdFacultad).HasName("PK__Facultad__B583ABCE3D8AEFB2");

            entity.ToTable("Facultad");

            entity.Property(e => e.IdFacultad).HasColumnName("id_facultad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Inscripcion>(entity =>
        {
            entity.HasKey(e => e.IdInscripcion).HasName("PK__Inscripc__CB0117BAACEBDFC2");

            entity.ToTable("Inscripcion");

            entity.Property(e => e.IdInscripcion).HasColumnName("id_inscripcion");
            entity.Property(e => e.IdCurso).HasColumnName("id_curso");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.Inscripcions)
                .HasForeignKey(d => d.IdCurso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inscripci__id_cu__47DBAE45");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Inscripcions)
                .HasForeignKey(d => d.IdEstudiante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inscripci__id_es__46E78A0C");
        });

        modelBuilder.Entity<Profesor>(entity =>
        {
            entity.HasKey(e => e.IdProfesor).HasName("PK__Profesor__159ED61743CBDD7C");

            entity.ToTable("Profesor");

            entity.Property(e => e.IdProfesor).HasColumnName("id_profesor");
            entity.Property(e => e.AniosExperiencia).HasColumnName("anios_experiencia");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.TituloMaximo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("titulo_maximo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
