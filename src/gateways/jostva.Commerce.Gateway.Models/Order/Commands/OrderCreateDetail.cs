namespace jostva.Commerce.Gateway.Models.Order.Commands
{
    public class OrderCreateDetail
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}