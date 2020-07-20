#region usings

using jostva.Commerce.Customer.Data;
using jostva.Commerce.Customer.Domain;
using jostva.Commerce.Customer.Service.Queries.DTOs;
using jostva.Commerce.Customer.Service.Queries.Interfaces;
using jostva.Infrastructure.Collection;
using jostva.Infrastructure.Mapping;
using jostva.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 

#endregion

namespace jostva.Commerce.Service.Customer.Queries
{
    public class ClientQueryService : IClientQueryService
    {
        private readonly ApplicationDbContext context;


        public ClientQueryService(ApplicationDbContext context)
        {
            this.context = context;
        }


        public async Task<DataCollection<ClientDto>> GetAllAsync(int page, int take, IEnumerable<int> clients = null)
        {
            DataCollection<Client> collection = await context.Clients
                                                             .Where(x => clients == null || clients.Contains(x.ClientId))
                                                             .OrderBy(x => x.Name)
                                                             .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<ClientDto>>();
        }


        public async Task<ClientDto> GetAsync(int id)
        {
            return (await context.Clients.SingleAsync(x => x.ClientId == id)).MapTo<ClientDto>();
        }
    }
}