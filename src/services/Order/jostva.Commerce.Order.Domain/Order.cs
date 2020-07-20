using jostva.Commerce.Order.Common;
using System;
using System.Collections.Generic;

namespace jostva.Commerce.Order.Domain
{
    public class Order
    {
        public int OrderId { get; set; }
        
        public OrderStatus Status { get; set; }
        
        public OrderPayment PaymentType { get; set; }
        
        public int ClientId { get; set; }
        
        public ICollection<OrderDetail> Items { get; set; } = new List<OrderDetail>();
        
        public DateTime CreatedAt { get; set; }
        
        public decimal Total { get; set; }
    }
}