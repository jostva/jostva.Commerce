#region usings

using jostva.Commerce.Catalog.Data;
using jostva.Commerce.Catalog.Domain;
using jostva.Commerce.Catalog.Services.EventHandlers.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace jostva.Commerce.Catalog.Services.EventHandlers
{
    public class ProductCreateEventHandler : INotificationHandler<ProductCreateCommand>
    {
        private readonly ApplicationDBContext context;


        public ProductCreateEventHandler(ApplicationDBContext context)
        {
            this.context = context;
        }


        public async Task Handle(ProductCreateCommand command, CancellationToken cancellationToken)
        {
            await context.AddAsync(new Product
            {
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
            });

            await context.SaveChangesAsync();
        }
    }
}