using GamersUnited.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace GamersUnited.Infrastructure.Data.Context
{
    public class GamersUnitedContext : DbContext
    {
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<GameGenre> GameGenre { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Sold> Sold { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Invoice> Invoice { get; set; }

        public GamersUnitedContext(DbContextOptions<GamersUnitedContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>()
                .HasKey(pc => pc.Id);

            modelBuilder.Entity<Product>()
                .HasOne<ProductCategory>(p => p.Category)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Game>()
                .HasOne<GameGenre>(g => g.Genre)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Stock>()
                .HasOne<Product>(s => s.Product)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey<Product>(p => p.Id);

            modelBuilder.Entity<Sold>()
                .HasOne<Product>(s => s.Product)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey<Product>(p => p.Id);

            modelBuilder.Entity<SoldInvoiceRelation>().HasKey(si => new { si.SoldId, si.InvoiceId });

            modelBuilder.Entity<SoldInvoiceRelation>()
                .HasOne<Sold>(s => s.Sold)
                .WithMany()
                .HasForeignKey(si => si.SoldId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<SoldInvoiceRelation>()
                .HasOne<Invoice>(i => i.Invoice)
                .WithMany(s => s.Products)
                .HasForeignKey(si => si.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
