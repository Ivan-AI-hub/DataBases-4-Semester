using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WholesaleEntities.Models;

namespace WholesaleEntities.DataBaseControllers
{
    public partial class WholesaleContext : DbContext
    {
        public WholesaleContext()
        {
        }

        public WholesaleContext(DbContextOptions<WholesaleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Employer> Employers { get; set; } = null!;
        public virtual DbSet<Manufacturer> Manufacturers { get; set; } = null!;
        public virtual DbSet<ManufacturersInformation> ManufacturersInformations { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductBalance> ProductBalances { get; set; } = null!;
        public virtual DbSet<ProductType> ProductTypes { get; set; } = null!;
        public virtual DbSet<Provaider> Provaiders { get; set; } = null!;
        public virtual DbSet<ProvidersInformation> ProvidersInformations { get; set; } = null!;
        public virtual DbSet<ReceiptReport> ReceiptReports { get; set; } = null!;
        public virtual DbSet<ReleaseReport> ReleaseReports { get; set; } = null!;
        public virtual DbSet<Storage> Storages { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DataBaseConnection.Instance.GetConnection());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(20);

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.TelephoneNumber).HasMaxLength(20);
            });

            modelBuilder.Entity<Employer>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(20);
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<ManufacturersInformation>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ManufacturersInformation");

                entity.Property(e => e.ManufacturersName).HasMaxLength(50);

                entity.Property(e => e.ProductName).HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Package).HasMaxLength(20);

                entity.Property(e => e.StorageConditions).HasMaxLength(30);

                entity.Property(e => e.StorageLife).HasColumnType("date");

                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ManufacturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_To_Manufacturers");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_To_ProductTypes");
            });

            modelBuilder.Entity<ProductBalance>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ProductBalance");

                entity.Property(e => e.ProductName).HasMaxLength(50);

                entity.Property(e => e.Storage).HasMaxLength(20);
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Feature).HasMaxLength(20);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Provaider>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(20);

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.TelephoneNumber).HasMaxLength(20);
            });

            modelBuilder.Entity<ProvidersInformation>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ProvidersInformation");

                entity.Property(e => e.ProductName).HasMaxLength(50);

                entity.Property(e => e.ProvidersName).HasMaxLength(20);

                entity.Property(e => e.ReceipDate).HasColumnType("date");
            });

            modelBuilder.Entity<ReceiptReport>(entity =>
            {
                entity.Property(e => e.DepartureDate).HasColumnType("date");

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.ReciveDate).HasColumnType("date");

                entity.HasOne(d => d.Employer)
                    .WithMany(p => p.ReceiptReports)
                    .HasForeignKey(d => d.EmployerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReceiptReports_To_Employers");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ReceiptReports)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReceiptReports_To_Products");

                entity.HasOne(d => d.Provaider)
                    .WithMany(p => p.ReceiptReports)
                    .HasForeignKey(d => d.ProvaiderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReceiptReports_To_Provaiders");

                entity.HasOne(d => d.Storage)
                    .WithMany(p => p.ReceiptReports)
                    .HasForeignKey(d => d.StorageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReceiptReports_To_Storages");
            });

            modelBuilder.Entity<ReleaseReport>(entity =>
            {
                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.ReciveDate).HasColumnType("date");

                entity.Property(e => e.ReleaseDate).HasColumnType("date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ReleaseReports)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReleaseReports_To_Customers");

                entity.HasOne(d => d.Employer)
                    .WithMany(p => p.ReleaseReports)
                    .HasForeignKey(d => d.EmployerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReleaseReports_To_Employers");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ReleaseReports)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReleaseReports_To_Products");

                entity.HasOne(d => d.Storage)
                    .WithMany(p => p.ReleaseReports)
                    .HasForeignKey(d => d.StorageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReleaseReports_To_Storages");
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
