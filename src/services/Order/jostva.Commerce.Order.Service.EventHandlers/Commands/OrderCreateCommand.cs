using jostva.Commerce.Order.Common;
using MediatR;
using System.Collections.Generic;

namespace jostva.Commerce.Order.Service.EventHandlers.Commands
{
    public class OrderCreateCommand : INotification
    {
        public OrderPayment PaymentType { get; set; }
        
        public int ClientId { get; set; }
        
        public IEnumerable<OrderCreateDetail> Items { get; set; } = new List<OrderCreateDetail>();
    }
}