using InventoryTrackingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace InventoryTrackingSystem.EntityFrameworkCore
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } // Table for Products
        public DbSet<Store> Stores { get; set; } // Table for Stores
        public DbSet<StockMovement> StockMovements { get; set; } // Table for StockMovements
        public DbSet<StoreProductStock> StoreProductStocks { get; set; } // Table for StoreProductStocks
        public DbSet<User> Users { get; set; } // Table for Users


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User Entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id); // Primary Key

                entity.Property(u => u.Username)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.PasswordHash)
                      .IsRequired();

                entity.Property(u => u.Email)
                      .HasMaxLength(150);

                entity.Property(u => u.Role)
                      .IsRequired()
                      .HasMaxLength(50);

                // Optional: Foreign key to Store
                entity.HasOne<Store>()
                      .WithMany()
                      .HasForeignKey(u => u.StoreId)
                      .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
            });

            // Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Price).IsRequired().HasPrecision(18, 2);
                entity.Property(p => p.Description).HasMaxLength(500);
            });

            // Store
            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.Property(s => s.Location).IsRequired().HasMaxLength(200);
            });

            // StoreProductStock
            modelBuilder.Entity<StoreProductStock>(entity =>
            {
                entity.HasKey(sps => sps.Id);
                entity.Property(sps => sps.Quantity).IsRequired();

                entity.HasOne<Store>()
                      .WithMany()
                      .HasForeignKey(sps => sps.StoreId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne<Product>()
                      .WithMany()
                      .HasForeignKey(sps => sps.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // StockMovement
            modelBuilder.Entity<StockMovement>(entity =>
            {
                entity.HasKey(sm => sm.Id);
                entity.Property(sm => sm.Quantity).IsRequired();
                entity.Property(sm => sm.MovementType).IsRequired().HasMaxLength(50);
                entity.Property(sm => sm.Timestamp).IsRequired();
                entity.Property(sm => sm.CreatedById).IsRequired();
                entity.Property(sm => sm.CreatedByName).IsRequired().HasMaxLength(100);

                entity.HasOne<Product>()
                      .WithMany()
                      .HasForeignKey(sm => sm.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Store>()
                      .WithMany()
                      .HasForeignKey(sm => sm.StoreId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

        }
    }
}
