using Application.DTOs;
using Application.DTOs.Common.PagingResponse;
using Application.Parameters;
using Domain.Entities.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Support
{
    public interface IInsuranceRepositoryAsync : IGenericRepositoryAsync<Insurance>
    {
        Task<PageResponse<Insurance>> GetInsuranceAsync(long entitySeqNum, string sortName, string sortOrder, RequestPageParameter pageParameter);
    }
}
