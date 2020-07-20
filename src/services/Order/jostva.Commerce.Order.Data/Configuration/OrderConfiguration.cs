using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace jostva.Commerce.Order.Data.Configuration
{
    public class OrderConfiguration
    {
        public OrderConfiguration(EntityTypeBuilder<Domain.Order> entityBuilder)
        {
            entityBuilder.HasKey(x => x.OrderId);
        }
    }
}