using jostva.Commerce.Customer.Data;
using jostva.Commerce.Customer.Domain;
using jostva.Commerce.Customer.Service.EventHandlers.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace jostva.Commerce.Service.Customer.EventHandlers
{
    public class ClientEventHandler : INotificationHandler<ClientCreateCommand>
    {
        private readonly ApplicationDbContext context;


        public ClientEventHandler(ApplicationDbContext context)
        {
            this.context = context;
        }


        public async Task Handle(ClientCreateCommand notification, CancellationToken cancellationToken)
        {
            await context.AddAsync(new Client
            {
                Name = notification.Name,
                Lastname = notification.Lastname
            });

            await context.SaveChangesAsync();
        }
    }
}