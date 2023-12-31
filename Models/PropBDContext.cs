﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public partial class PropBDContext : DbContext
{
    public PropBDContext()
    {
    }

    public PropBDContext(DbContextOptions<PropBDContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comercio> Comercio { get; set; }

    public virtual DbSet<Anuncio> Anuncio { get; set; }

    public virtual DbSet<Productos> Productos { get; set; }

    public virtual DbSet<Tipo> Tipo { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=globalserverjesus.database.windows.net;Initial Catalog=ProyectoPIN_BD;Persist Security Info=True;User ID=jesusAdmin;Password=Administrador2002");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comercio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comercio__3214EC079FCCB8A1");

            entity.Property(e => e.Contraseña)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(250);
            entity.Property(e => e.Direccion)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.Facebook).HasMaxLength(200);
            entity.Property(e => e.Horario)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Instagram).HasMaxLength(200);
            entity.Property(e => e.Mail)
                .IsRequired()
                .HasMaxLength(250);
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Provincia)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Web).HasMaxLength(200);

            entity.HasMany(d => d.Tipo).WithMany(p => p.Comercio)
                .UsingEntity<Dictionary<string, object>>(
                    "TipoComercio",
                    r => r.HasOne<Tipo>().WithMany()
                        .HasForeignKey("TipoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TipoID_ID"),
                    l => l.HasOne<Comercio>().WithMany()
                        .HasForeignKey("ComercioId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ComercioID_ID"),
                    j =>
                    {
                        j.HasKey("ComercioId", "TipoId").HasName("PK__TipoCome__E4130A5B45C7B26D");
                        j.IndexerProperty<int>("ComercioId").HasColumnName("Comercio_ID");
                        j.IndexerProperty<int>("TipoId").HasColumnName("Tipo_ID");
                    });

        });

        modelBuilder.Entity<Anuncio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Anuncio__3214EC07A03641EC");

            entity.Property(e => e.IdComercio).IsRequired();
            entity.Property(e => e.Fecha).IsRequired();
            entity.Property(e => e.Titulo).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(255);
            entity.Property(e => e.NombreImagenes).HasMaxLength(255);
            entity.Property(e => e.Tipo).IsRequired().HasMaxLength(255);

            entity.HasOne(e => e.Comercio).WithMany(p => p.IdAnuncio).HasForeignKey(e => e.IdComercio).HasConstraintName("fk_Oferta_Comercio");
        });

        modelBuilder.Entity<Productos>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PRODUCTOS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });



        modelBuilder.Entity<Tipo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tipo__3214EC07FBE66B73");

            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.Nombre).HasMaxLength(100);


        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC074C5B678F");

            entity.Property(e => e.Contraseña)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.Mail)
                .IsRequired()
                .HasMaxLength(250);
            entity.Property(e => e.NickName)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasMany(d => d.IdComercio).WithMany(p => p.IdUsuario)
                .UsingEntity<Dictionary<string, object>>(
                    "ComerciosSeguidos",
                    r => r.HasOne<Comercio>().WithMany()
                        .HasForeignKey("IdComercio")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_IdComercio_ID"),
                    l => l.HasOne<Usuario>().WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_IdUsuario_ID"),
                    j =>
                    {
                        j.HasKey("IdUsuario", "IdComercio").HasName("PK__Comercio__EB987982DF082A6B");
                    });

            entity.HasMany(d => d.IdSeguido).WithMany(p => p.IdSeguidor)
                .UsingEntity<Dictionary<string, object>>(
                    "UsuariosSeguidos",
                    r => r.HasOne<Usuario>().WithMany()
                        .HasForeignKey("IdSeguido")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_IdSeguido_ID"),
                    l => l.HasOne<Usuario>().WithMany()
                        .HasForeignKey("IdSeguidor")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_IdSeguidor_ID"),
                    j =>
                    {
                        j.HasKey("IdSeguidor", "IdSeguido").HasName("PK__Usuarios__70A1204396F3F294");
                    });

            entity.HasMany(d => d.IdSeguidor).WithMany(p => p.IdSeguido)
                .UsingEntity<Dictionary<string, object>>(
                    "UsuariosSeguidos",
                    r => r.HasOne<Usuario>().WithMany()
                        .HasForeignKey("IdSeguidor")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_IdSeguidor_ID"),
                    l => l.HasOne<Usuario>().WithMany()
                        .HasForeignKey("IdSeguido")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_IdSeguido_ID"),
                    j =>
                    {
                        j.HasKey("IdSeguidor", "IdSeguido").HasName("PK__Usuarios__70A1204396F3F294");
                    });
        });

        OnModelCreatingGeneratedProcedures(modelBuilder);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}