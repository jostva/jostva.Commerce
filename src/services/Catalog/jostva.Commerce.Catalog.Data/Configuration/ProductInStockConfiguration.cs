using jostva.Commerce.Catalog.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace jostva.Commerce.Catalog.Data.Configuration
{
    public class ProductInStockConfiguration
    {
        public ProductInStockConfiguration(EntityTypeBuilder<ProductInStock> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(item => item.ProductInStockId);

            Random random = new Random();
            List<ProductInStock> products = new List<ProductInStock>();

            for (int i = 1; i <= 100; i++)
            {
                products.Add(new ProductInStock
                {
                    ProductInStockId = i,
                    ProductId = i,
                    Stock = random.Next(0, 100)
                });
            }

            entityTypeBuilder.HasData(products);
        }
    }
}