using Microsoft.EntityFrameworkCore;
using PracticaProgramada2.Domain;

namespace PracticaProgramada2.Infrastructure.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Categoria> Categorias { get; set; }

    public DbSet<Producto> Productos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.ToTable("Categorias");

            entity.HasKey(categoria => categoria.CategoriaId);

            entity.HasIndex(categoria => categoria.Nombre)
                .IsUnique();

            entity.Property(categoria => categoria.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(categoria => categoria.Descripcion)
                .IsRequired()
                .HasMaxLength(300);

            entity.Property(categoria => categoria.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("DATETIME");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.ToTable("Productos");

            entity.HasKey(producto => producto.ProductoId);

            entity.HasIndex(producto => producto.CategoriaId);

            entity.Property(producto => producto.Nombre)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(producto => producto.Descripcion)
                .IsRequired()
                .HasMaxLength(300);

            entity.Property(producto => producto.Precio)
                .IsRequired()
                .HasColumnType("DECIMAL(18,2)");

            entity.Property(producto => producto.Stock)
                .IsRequired();

            entity.Property(producto => producto.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("DATETIME");

            entity.HasOne(producto => producto.Categoria)
                .WithMany(categoria => categoria.Productos)
                .HasForeignKey(producto => producto.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
