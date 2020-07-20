namespace jostva.Commerce.Order.Service.EventHandlers.Commands
{
    public class OrderCreateDetail
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}