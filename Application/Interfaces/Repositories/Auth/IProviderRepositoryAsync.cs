using Application.DTOs.Common.PagingResponse;
using Application.Parameters;
using Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Auth
{
    public interface IProviderRepositoryAsync : IGenericRepositoryAsync<Provider>
    {
        Task<PageResponse<Provider>> GetProvidersByIdAsync(long entityId, string sortName, string sortOrder, RequestPageParameter pageParameter);
        Task<Provider> GetProviderDetailByIdAsync(long id);
        Task<int> UpdatePatchAsync(Provider provider);
    }
}
