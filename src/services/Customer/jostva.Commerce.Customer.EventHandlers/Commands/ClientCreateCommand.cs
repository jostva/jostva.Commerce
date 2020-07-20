using MediatR;

namespace jostva.Commerce.Customer.Service.EventHandlers.Commands
{
    public class ClientCreateCommand : INotification
    {
        public string Name { get; set; }

        public string Lastname { get; set; }
    }
}