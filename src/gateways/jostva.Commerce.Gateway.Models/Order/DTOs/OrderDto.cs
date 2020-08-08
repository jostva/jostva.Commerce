using jostva.Commerce.Gateway.Models.Customer.DTOs;
using jostva.Commerce.Gateway.Models.Order.Commons;
using System;
using System.Collections.Generic;

namespace jostva.Commerce.Gateway.Models.Order.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        
        public ClientDto Client { get; set; }
        
        public string OrderNumber { get; set; }
        
        public OrderStatus Status { get; set; }
        
        public OrderPayment PaymentType { get; set; }
        
        public int ClientId { get; set; }
        
        public IEnumerable<OrderDetailDto> Items { get; set; } = new List<OrderDetailDto>();
        
        public DateTime CreatedAt { get; set; }
        
        public decimal Total { get; set; }
    }
}