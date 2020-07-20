using jostva.Commerce.Catalog.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace jostva.Commerce.Catalog.Data.Configuration
{
    public class ProductConfiguration
    {
        public ProductConfiguration(EntityTypeBuilder<Product> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(item => item.ProductId);
            entityTypeBuilder.Property(item => item.Name).IsRequired().HasMaxLength(100);
            entityTypeBuilder.Property(item => item.Description).IsRequired().HasMaxLength(500);

            //  Products by default
            List<Product> products = new List<Product>();
            Random random = new Random();

            for (int i = 1; i <= 100; i++)
            {
                products.Add(new Product
                {
                    ProductId = i,
                    Name = $"Product {i}",
                    Description = $"Description for Product {i}",
                    Price = random.Next(100, 1000)                   
                });
            }

            entityTypeBuilder.HasData(products);
        }
    }
}