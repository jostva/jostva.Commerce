using jostva.Commerce.Customer.Data.Configuration;
using jostva.Commerce.Customer.Domain;
using Microsoft.EntityFrameworkCore;

namespace jostva.Commerce.Customer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
               
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //  Database Schema
            builder.HasDefaultSchema("Customer");

            //  Model Contraints
            ModelConfig(builder);
        }


        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new ClientConfiguration(modelBuilder.Entity<Client>());
        }
    }
}