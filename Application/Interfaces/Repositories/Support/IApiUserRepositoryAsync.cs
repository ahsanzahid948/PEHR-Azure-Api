using Domain.Entities.Support;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IApiUserRepositoryAsync : IGenericRepositoryAsync<ApiUser>
    {
        Task<IReadOnlyList<ApiAccessLog>> GetAccessLogByFilterAsync(string filterCriteria);

        Task<int> CreateApiUserAsync(ApiUser user);
    }
}
