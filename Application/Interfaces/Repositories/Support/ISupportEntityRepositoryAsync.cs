using Domain.Entities.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Support
{
    public interface ISupportEntityRepositoryAsync : IGenericRepositoryAsync<CustomReport>
    {
        Task<IReadOnlyList<CustomReport>> GetCustomReportsByIdAsync(long id);
        Task<int> CreateMergedDocumentAsync(MergedDocument mergedDocument);


    }
}
