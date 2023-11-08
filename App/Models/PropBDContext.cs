﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PropAPI.Controllers;

namespace WebApplication1.Models;

public partial class PropBDContext : DbContext
{
    private readonly DbContextOptionsBuilder optionsBuilder;
    public PropBDContext()
    {
        optionsBuilder = new DbContextOptionsBuilder();
        optionsBuilder.UseNpgsql("User Id=postgres;Password=4gcr4D4VpKwYK5Wz;Server=db.cgqvfaotdatwfllyfmhr.supabase.co;Port=5432;Database=postgres");
    }

    public PropBDContext(DbContextOptions<PropBDContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comercio> comercio { get; set; }

    public virtual DbSet<Anuncio> anuncio { get; set; }
    public virtual DbSet<Lista> lista { get; set; }
    public virtual DbSet<Productos> productos { get; set; }

    public virtual DbSet<Tipo> tipo { get; set; }

    public virtual DbSet<Usuario> usuario { get; set; }
    public virtual DbSet<Imagen> imagenes { get; set; }
    public virtual DbSet<Reseña> reseña { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("User Id=postgres;Password=4gcr4D4VpKwYK5Wz;Server=db.cgqvfaotdatwfllyfmhr.supabase.co;Port=5432;Database=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comercio>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__Comercio__3214EC079FCCB8A1");

            entity.Property(e => e.contraseña)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.valoracionpromedio)
                .HasMaxLength(200);
            entity.Property(e => e.descripcion)
                .IsRequired()
                .HasMaxLength(250);
            entity.Property(e => e.direccion)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.facebook).HasMaxLength(200);
            entity.Property(e => e.horario)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.instagram).HasMaxLength(200);
            entity.Property(e => e.mail)
                .IsRequired()
                .HasMaxLength(250);
            entity.Property(e => e.nombre)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.provincia)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.web).HasMaxLength(200);

            entity.HasMany(d => d.tipo_id).WithMany(p => p.comercio_id)
                .UsingEntity<Dictionary<string, object>>(
                    "tipocomercio",
                    r => r.HasOne<Tipo>().WithMany()
                        .HasForeignKey("tipo_id")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TipoComercio_Tipo"),
                    l => l.HasOne<Comercio>().WithMany()
                        .HasForeignKey("comercio_id")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TipoComercio_Comercio"),
                    j =>
                    {
                        j.HasKey("comercio_id", "tipo_id").HasName("PK__TipoCome__E4130A5B45C7B26D");
                        j.IndexerProperty<int>("comercio_id").HasColumnName("comercio_id");
                        j.IndexerProperty<int>("tipo_id").HasColumnName("tipo_id");
                    });

        });

        modelBuilder.Entity<Anuncio>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__Anuncio__3214EC07A03641EC");

            entity.Property(e => e.idcomercio).IsRequired();
            entity.Property(e => e.fecha).IsRequired();
            entity.Property(e => e.titulo).IsRequired().HasMaxLength(255);
            entity.Property(e => e.descripcion).IsRequired().HasMaxLength(255);
            entity.Property(e => e.imagenes).HasMaxLength(255);
            entity.Property(e => e.tipo).IsRequired().HasMaxLength(255);

            entity.HasOne(e => e.Comercio).WithMany(p => p.idcomercio).HasForeignKey(e => e.idcomercio).HasConstraintName("fk_Anuncio_Comercio");
        });


        modelBuilder.Entity<Reseña>(entity =>
        {
            entity.HasKey(e => e.comercio).HasName("PK__Tipo__3214EC07FBE66B73");
            entity.HasKey(e => e.usuario).HasName("PK__Tipo__3214EC07FBE66B76");

            entity.Property(e => e.descripcion).HasMaxLength(200);
            entity.Property(e => e.titulo).HasMaxLength(100);
            entity.Property(e => e.puntuacion).HasMaxLength(1);


            entity.HasOne(e => e.comercioObject).WithMany(p => p.reseñas).HasForeignKey(e => e.comercio).HasConstraintName("fk_Reseña_Comercio");
            entity.HasOne(e => e.usuarioObject).WithMany(p => p.reseñas).HasForeignKey(e => e.usuario).HasConstraintName("fk_Reseña_Usuario");

        });

        modelBuilder.Entity<Tipo>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__Tipo__3214EC07FBE66B73");

            entity.Property(e => e.descripcion).HasMaxLength(200);
            entity.Property(e => e.nombre).HasMaxLength(100);


        });

        modelBuilder.Entity<Lista>(entity =>
        {
            entity.HasKey(e => e.id);

            entity.HasOne(e => e.usuario)
                .WithMany()
                .HasForeignKey(e => e.idusuario);

            entity.HasMany(e => e.Comercio).WithMany(p => p.lista_id)
                .UsingEntity<Dictionary<string, object>>(
                   "listacomercio",
                   r => r.HasOne<Comercio>().WithMany()
                        .HasForeignKey("comercio")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_listacomercio_comercio"),
                   l => l.HasOne<Lista>().WithMany()
                        .HasForeignKey("lista")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_listacomercio_lista"),
                   j =>
                   {
                       j.HasKey("lista", "comercio");
                   });


        });

        modelBuilder.Entity<Usuario>(entity =>
    {
        entity.HasKey(e => e.id).HasName("PK__Usuario__3214EC074C5B678F");

        entity.Property(e => e.contraseña)
            .IsRequired()
            .HasMaxLength(200);
        entity.Property(e => e.mail)
            .IsRequired()
            .HasMaxLength(250);
        entity.Property(e => e.nickname)
            .IsRequired()
            .HasMaxLength(200);
        entity.Property(e => e.nombre)
            .IsRequired()
            .HasMaxLength(100);

        entity.HasMany(d => d.idcomercio).WithMany(p => p.idusuario)
            .UsingEntity<Dictionary<string, object>>(
                "comerciosseguidos",
                r => r.HasOne<Comercio>().WithMany()
                    .HasForeignKey("idcomercio")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComerciosSeguidos_Comercio"),
                l => l.HasOne<Usuario>().WithMany()
                    .HasForeignKey("idusuario")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComerciosSeguidos_Usuario"),
                j =>
                {
                    j.HasKey("idusuario", "idcomercio").HasName("PK__Comercio__EB987982DF082A6B");
                });

        entity.HasMany(d => d.idseguido).WithMany(p => p.idseguidor)
            .UsingEntity<Dictionary<string, object>>(
                "usuariosseguidos",
                r => r.HasOne<Usuario>().WithMany()
                    .HasForeignKey("idseguido")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuariosSeguidos_IdSeguido"),
                l => l.HasOne<Usuario>().WithMany()
                    .HasForeignKey("idseguidor")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuariosSeguidos_IdSeguidor"),
                j =>
                {
                    j.HasKey("idseguidor", "idseguido").HasName("PK__Usuarios__70A1204396F3F294");
                });

        entity.HasMany(d => d.idseguidor).WithMany(p => p.idseguido)
            .UsingEntity<Dictionary<string, object>>(
                "usuariosseguidos",
                r => r.HasOne<Usuario>().WithMany()
                    .HasForeignKey("idseguidor")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuariosSeguidos_IdSeguidor"),
                l => l.HasOne<Usuario>().WithMany()
                    .HasForeignKey("idseguido")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuariosSeguidos_IdSeguido"),
                j =>
                {
                    j.HasKey("idseguidor", "idseguido").HasName("PK__Usuarios__70A1204396F3F294");
                });
    });

        OnModelCreatingGeneratedProcedures(modelBuilder);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}