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
            base.Database.Migrate();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>()
                .HasKey(pc => pc.ProductCategoryId);

            modelBuilder.Entity<Product>()
                .HasOne<ProductCategory>(p => p.Category)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Game>()
                .HasOne<Product>(g => g.Product)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey<Game>(g => g.ProductFK);

            modelBuilder.Entity<Game>()
                .HasOne<GameGenre>(g => g.Genre)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Stock>().HasKey(s => new { s.StockId, s.ProductId });
            modelBuilder.Entity<Stock>().Property(s => s.StockId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Stock>()
                .HasOne<Product>(s => s.Product)
                .WithMany()
                .HasForeignKey(si => si.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sold>().HasKey(s => new { s.SoldId, s.ProductId });
            modelBuilder.Entity<Sold>().Property(s => s.SoldId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Sold>()
                .HasOne<Product>(s => s.Product)
                .WithMany()
                .HasForeignKey(si => si.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SoldInvoiceRelation>().HasKey(si => new { si.SoldId, si.ProductId, si.InvoiceId });

            modelBuilder.Entity<SoldInvoiceRelation>()
                .HasOne<Sold>(s => s.Sold)
                .WithMany()
                .HasForeignKey(si => new {si.SoldId, si.ProductId})
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SoldInvoiceRelation>()
                .HasOne<Invoice>(i => i.Invoice)
                .WithMany(s => s.Products)
                .HasForeignKey(si => si.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
