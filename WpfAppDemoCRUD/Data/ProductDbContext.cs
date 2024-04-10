using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WpfAppDemoCRUD.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Cart { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(GetProducts());
            modelBuilder.Entity<Cart>();
            base.OnModelCreating(modelBuilder);
        }

        private Product[] GetProducts()
        {
            return new Product[]
            {
                new Product { Id = 1, Name = "Tshirt", Description = "Red", Price = 19.99, Unit =5},
                new Product { Id = 2, Name = "Tshirt", Description = "Blue Color", Price = 10.99, Unit =50},
                new Product { Id = 3, Name = "Shirt", Description = "Formal Shirt", Price = 10.99, Unit =25},
                new Product { Id = 4, Name = "Socks", Description = "Yellow ", Price = 2, Unit =500},
                new Product { Id = 5, Name = "Tsdfghirt", Description = "Red", Price = 19.99, Unit =5},
                new Product { Id = 6, Name = "dfdf", Description = "Blue Color", Price = 10.99, Unit =50},
                new Product { Id = 7, Name = "hjgh", Description = "Formal Shirt", Price = 10.99, Unit =25},
                new Product { Id = 8, Name = "dsdfdfg", Description = "Yellow ", Price = 2, Unit =500},
                   new Product { Id = 9, Name = "jku", Description = "Red", Price = 19.99, Unit =5},
                new Product { Id = 10, Name = "rreree", Description = "Blue Color", Price = 10.99, Unit =50},
                new Product { Id = 11, Name = "Shkujdirt", Description = "Formal Shirt", Price = 10.99, Unit =25},
                new Product { Id =12, Name = "sswretrt", Description = "Yellow ", Price = 2, Unit =500},
                   new Product { Id = 13, Name = "oiuyt", Description = "Red", Price = 19.99, Unit =5},
                new Product { Id = 14, Name = "dfvb", Description = "Blue Color", Price = 10.99, Unit =50},
                new Product { Id = 15, Name = "pokjhbb", Description = "Formal Shirt", Price = 10.99, Unit =25},
                new Product { Id = 16, Name = "dfg", Description = "Yellow ", Price = 2, Unit =500},
            };
        }
    }
}
