using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiPruebas.Models;

public partial class DbapiContext : DbContext
{
    public DbapiContext()
    {
    }

    public DbapiContext(DbContextOptions<DbapiContext> options) : base(options)
    {
    }

    public  DbSet<Categoria> Categoria { get; set; }

    public  DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_bin")
            .HasCharSet("utf8");

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PRIMARY");

            entity.ToTable("categoria");

            entity.Property(e => e.IdCategoria).HasColumnType("int(11)");
            entity.Property(e => e.Descripcion).HasMaxLength(50);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PRIMARY");

            entity.ToTable("producto");

            entity.HasIndex(e => e.IdCategoria, "FK_IDCATEGORIA");

            entity.Property(e => e.IdProducto).HasColumnType("int(11)");
            entity.Property(e => e.CodigoBarra).HasMaxLength(20);
            entity.Property(e => e.Descripcion).HasMaxLength(50);
            entity.Property(e => e.IdCategoria).HasColumnType("int(11)");
            entity.Property(e => e.Marca).HasMaxLength(50);
            entity.Property(e => e.Precio).HasPrecision(10, 2);

            entity.HasOne(d => d.oCategoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK_IDCATEGORIA");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
