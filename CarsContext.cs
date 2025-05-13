using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace chizhprimebd;

public partial class CarsContext : DbContext
{
    public CarsContext()
    {
    }

    public CarsContext(DbContextOptions<CarsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("DataSource=C:\\Users\\Студент.44-6\\Desktop\\bdbd\\Cars.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.IdCars);

            entity.HasIndex(e => e.IdCars, "IX_Cars_idCars").IsUnique();

            entity.Property(e => e.IdCars)
                .ValueGeneratedNever()
                .HasColumnName("idCars");

            entity.HasOne(d => d.ManufacturersNavigation).WithMany(p => p.Cars).HasForeignKey(d => d.Manufacturers);

            entity.HasOne(d => d.ModelNavigation).WithMany(p => p.Cars).HasForeignKey(d => d.Model);
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.IdManufacturers);

            entity.HasIndex(e => e.IdManufacturers, "IX_Manufacturers_idManufacturers").IsUnique();

            entity.Property(e => e.IdManufacturers).HasColumnName("idManufacturers");
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasKey(e => e.IdModels);

            entity.HasIndex(e => e.IdModels, "IX_Models_idModels").IsUnique();

            entity.Property(e => e.IdModels)
                .ValueGeneratedNever()
                .HasColumnName("idModels");
            entity.Property(e => e.Model1).HasColumnName("Model");

            entity.HasOne(d => d.ManufacturersNavigation).WithMany(p => p.Models).HasForeignKey(d => d.Manufacturers);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
