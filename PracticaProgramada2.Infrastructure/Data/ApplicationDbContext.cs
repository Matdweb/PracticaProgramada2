using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PracticaProgramada1.Domain;

namespace PracticaProgramada1.Infrastructure.Data;

public partial class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Clientes> Clientes { get; set; }


    public virtual DbSet<Telefonos> Telefonos { get; set; }


    /// <param name="modelBuilder">Proporciona una API para configurar el modelo de EF.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Clientes>(entity =>
        {
            entity.ToTable("Clientes");

            entity.HasKey(e => e.ClienteId);

            entity.HasIndex(e => e.Cedula, "IX_Clientes_Cedula").IsUnique();

            entity.HasIndex(e => e.ClienteId, "idx_clientes_clienteid");

            entity.Property(e => e.ClienteId).ValueGeneratedOnAdd();

            entity.Property(e => e.Cedula)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.PrimerApellido)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.SegundoApellido)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Correo)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(e => e.Direccion)
                .IsRequired()
                .HasMaxLength(300);

            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("DATETIME");

            entity.HasMany(d => d.Telefonos)
                .WithOne(p => p.FkclienteNavigation)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Telefonos>(entity =>
        {
            entity.ToTable("Telefonos");

            entity.HasKey(e => e.TelefonoId);

            entity.HasIndex(e => e.ClienteId, "IX_Telefonos_ClienteId");

            entity.HasIndex(e => e.Numero, "idx_telefonos_numero");

            entity.Property(e => e.TelefonoId).ValueGeneratedOnAdd();

            entity.Property(e => e.ClienteId).IsRequired();

            entity.Property(e => e.Numero)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.Tipo)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("DATETIME");

            entity.HasOne(d => d.FkclienteNavigation)
                .WithMany(p => p.Telefonos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    /// <param name="modelBuilder">El generador del modelo de EF.</param>
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
