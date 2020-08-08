using jostva.Commerce.Gateway.Models.Catalog.DTOs;

namespace jostva.Commerce.Gateway.Models.Order.DTOs
{
    public class OrderDetailDto
    {
        public ProductDto Product { get; set; }
        
        public int OrderDetailId { get; set; }
        
        public int ProductId { get; set; }
        
        public int Quantity { get; set; }
        
        public decimal UnitPrice { get; set; }
        
        public decimal Total { get; set; }
    }
}