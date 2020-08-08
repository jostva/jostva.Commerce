using jostva.Commerce.Gateway.Models.Order.Commons;
using System.Collections.Generic;

namespace jostva.Commerce.Gateway.Models.Order.Commands
{
    public class OrderCreateCommand
    {
        public OrderPayment PaymentType { get; set; }

        public int ClientId { get; set; }

        public IEnumerable<OrderCreateDetail> Items { get; set; } = new List<OrderCreateDetail>();
    }
}