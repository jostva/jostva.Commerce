using jostva.Commerce.Order.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace jostva.Commerce.Order.Data.Configuration
{
    public class OrderDetailConfiguration
    {
        public OrderDetailConfiguration(EntityTypeBuilder<OrderDetail> entityBuilder)
        {
            entityBuilder.HasKey(x => x.OrderDetailId);
        }
    }
}