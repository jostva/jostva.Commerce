using jostva.Commerce.Identity.Services.Queries.DTOs;
using jostva.Infrastructure.Collection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace jostva.Commerce.Identity.Services.Queries.Interfaces
{
    public interface IUserQueryService
    {
        Task<DataCollection<UserDto>> GetAllAsync(int page, int take, IEnumerable<string> users = null);
        
        Task<UserDto> GetAsync(string id);
    }
}