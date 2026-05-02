using Microsoft.EntityFrameworkCore;
using ProductService.domain.Product.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.infrastructure.Common.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
       


        public DbSet<Product> Products { get; set; }    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
