#region usings

using jostva.Commerce.Identity.Data;
using jostva.Commerce.Identity.Services.Queries.DTOs;
using jostva.Commerce.Identity.Services.Queries.Interfaces;
using jostva.Infrastructure.Collection;
using jostva.Infrastructure.Mapping;
using jostva.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace jostva.Commerce.Identity.Services.Queries
{
    public class UserQueryService : IUserQueryService
    {
        private readonly ApplicationDbContext context;


        public UserQueryService(ApplicationDbContext context)
        {
            this.context = context;
        }


        public async Task<DataCollection<UserDto>> GetAllAsync(int page, int take, IEnumerable<string> users = null)
        {
            DataCollection<Domain.ApplicationUser> collection = await context.Users
                                                                             .Where(x => users == null || users.Contains(x.Id))
                                                                             .OrderBy(x => x.FirstName)
                                                                             .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<UserDto>>();
        }


        public async Task<UserDto> GetAsync(string id)
        {
            return (await context.Users.SingleAsync(x => x.Id == id)).MapTo<UserDto>();
        }
    }
}