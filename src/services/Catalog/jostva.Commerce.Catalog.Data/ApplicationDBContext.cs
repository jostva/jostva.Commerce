using jostva.Commerce.Catalog.Data.Configuration;
using jostva.Commerce.Catalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace jostva.Commerce.Catalog.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }


        public DbSet<Product> Products { get; set; }

        public DbSet<ProductInStock> Stocks { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //  Database Schema
            builder.HasDefaultSchema("Catalog");

            ModelConfig(builder);
        }


        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new ProductConfiguration(modelBuilder.Entity<Product>());
            new ProductInStockConfiguration(modelBuilder.Entity<ProductInStock>());
        }
    }
}