using System;
using System.Collections.Generic;
using API_Movies.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Movies.Context;

public partial class ApiMovieContext : DbContext
{
    public ApiMovieContext()
    {
    }

    public ApiMovieContext(DbContextOptions<ApiMovieContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<FilmPersonne> FilmPersonnes { get; set; }

    public virtual DbSet<Personne> Personnes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Film>(entity =>
        {
            entity.HasKey(e => e.FilmId).HasName("PRIMARY");

            entity.ToTable("film");

            entity.Property(e => e.FilmId)
                .HasColumnType("int(11)")
                .HasColumnName("film_id");
            entity.Property(e => e.DateDeParution)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnName("Date_de_parution");
            entity.Property(e => e.Description).HasMaxLength(2048);
            entity.Property(e => e.Nom).HasMaxLength(128);
        });

        modelBuilder.Entity<FilmPersonne>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("film_personne");

            entity.HasIndex(e => e.FilmId, "film_id");

            entity.HasIndex(e => e.PersonneId, "personne_id");

            entity.Property(e => e.FilmId)
                .HasColumnType("int(11)")
                .HasColumnName("film_id");
            entity.Property(e => e.PersonneId)
                .HasColumnType("int(11)")
                .HasColumnName("personne_id");
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .HasColumnName("role");

            entity.HasOne(d => d.Film).WithMany()
                .HasForeignKey(d => d.FilmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("film_personne_ibfk_2");

            entity.HasOne(d => d.Personne).WithMany()
                .HasForeignKey(d => d.PersonneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("film_personne_ibfk_1");
        });

        modelBuilder.Entity<Personne>(entity =>
        {
            entity.HasKey(e => e.PersonneId).HasName("PRIMARY");

            entity.ToTable("personnes");

            entity.Property(e => e.PersonneId)
                .HasColumnType("int(11)")
                .HasColumnName("personne_id");
            entity.Property(e => e.DateNaissance).HasColumnName("date_naissance");
            entity.Property(e => e.Nom)
                .HasMaxLength(10)
                .HasColumnName("nom");
            entity.Property(e => e.Prenom)
                .HasMaxLength(10)
                .HasColumnName("prenom");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
