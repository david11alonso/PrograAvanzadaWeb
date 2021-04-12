using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace FrontEnd.Models
{
    public partial class PrograAvanzadaWebContext : DbContext
    {
        public PrograAvanzadaWebContext()
        {
        }

        public PrograAvanzadaWebContext(DbContextOptions<PrograAvanzadaWebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Comentario> Comentario { get; set; }
        public virtual DbSet<Departamento> Departamento { get; set; }
        public virtual DbSet<Foro> Foro { get; set; }
        public virtual DbSet<Noticia> Noticia { get; set; }
        public virtual DbSet<Propuesta> Propuesta { get; set; }
        public virtual DbSet<PropuestaDepartamento> PropuestaDepartamento { get; set; }
        public virtual DbSet<UsuarioDepartamento> UsuarioDepartamento { get; set; }
        public virtual DbSet<VotoPropuesta> VotoPropuesta { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:proyectofinalpropuestas.database.windows.net,1433;Database=PrograAvanzadaWeb;User Id=sqlFidelitas;Password=Fidelitas123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.Property(e => e.ComentarioId).HasColumnName("ComentarioID");

                entity.Property(e => e.Comentario1)
                    .IsRequired()
                    .HasColumnName("Comentario")
                    .IsUnicode(false);

                entity.Property(e => e.ForoId).HasColumnName("ForoID");

                entity.Property(e => e.UsuarioId)
                    .IsRequired()
                    .HasColumnName("UsuarioID")
                    .HasMaxLength(450);

                entity.HasOne(d => d.Foro)
                    .WithMany(p => p.Comentario)
                    .HasForeignKey(d => d.ForoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comentario_Foro");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Comentario)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comentario_Usuario");
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.Property(e => e.DepartamentoId).HasColumnName("DepartamentoID");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Foro>(entity =>
            {
                entity.Property(e => e.ForoId).HasColumnName("ForoID");

                entity.Property(e => e.PropuestaId).HasColumnName("PropuestaID");

                entity.HasOne(d => d.Propuesta)
                    .WithMany(p => p.Foro)
                    .HasForeignKey(d => d.PropuestaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Foro_Propuesta");
            });

            modelBuilder.Entity<Noticia>(entity =>
            {
                entity.Property(e => e.NoticiaId).HasColumnName("NoticiaID");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioId)
                    .IsRequired()
                    .HasColumnName("UsuarioID")
                    .HasMaxLength(450);

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Noticia)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Noticia_Usuario");
            });

            modelBuilder.Entity<Propuesta>(entity =>
            {
                entity.Property(e => e.PropuestaId).HasColumnName("PropuestaID");

                entity.Property(e => e.Beneficios)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.FechaFinalizacion).HasColumnType("date");

                entity.Property(e => e.FechaPublicacion).HasColumnType("date");

                entity.Property(e => e.Problema)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.ResultadoEsperado)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Riesgos)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioId)
                    .IsRequired()
                    .HasColumnName("UsuarioID")
                    .HasMaxLength(450);

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Propuesta)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Propuesta_User");
            });

            modelBuilder.Entity<PropuestaDepartamento>(entity =>
            {
                entity.Property(e => e.PropuestaDepartamentoId).HasColumnName("PropuestaDepartamentoID");

                entity.Property(e => e.DepartamentoId).HasColumnName("DepartamentoID");

                entity.Property(e => e.PropuestaId).HasColumnName("PropuestaID");

                entity.HasOne(d => d.Departamento)
                    .WithMany(p => p.PropuestaDepartamento)
                    .HasForeignKey(d => d.DepartamentoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PropuestaDepartamento_Departamento");

                entity.HasOne(d => d.Propuesta)
                    .WithMany(p => p.PropuestaDepartamento)
                    .HasForeignKey(d => d.PropuestaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PropuestaDepartamento_Propuesta");
            });

            modelBuilder.Entity<UsuarioDepartamento>(entity =>
            {
                entity.Property(e => e.UsuarioDepartamentoId).HasColumnName("UsuarioDepartamentoID");

                entity.Property(e => e.DepartamentoId).HasColumnName("DepartamentoID");

                entity.Property(e => e.UsuarioId)
                    .IsRequired()
                    .HasColumnName("UsuarioID")
                    .HasMaxLength(450);

                entity.HasOne(d => d.Departamento)
                    .WithMany(p => p.UsuarioDepartamento)
                    .HasForeignKey(d => d.DepartamentoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuarioDepartamento_Departamento");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.UsuarioDepartamento)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuarioDepartamento_Usuario");
            });

            modelBuilder.Entity<VotoPropuesta>(entity =>
            {
                entity.Property(e => e.VotoPropuestaId).HasColumnName("VotoPropuestaID");

                entity.Property(e => e.Comentario)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.PropuestaId).HasColumnName("PropuestaID");

                entity.Property(e => e.UsuarioId)
                    .IsRequired()
                    .HasColumnName("UsuarioID")
                    .HasMaxLength(450);

                entity.HasOne(d => d.Propuesta)
                    .WithMany(p => p.VotoPropuesta)
                    .HasForeignKey(d => d.PropuestaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Voto_Propuesta");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.VotoPropuesta)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VotoPropuesta_Usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
