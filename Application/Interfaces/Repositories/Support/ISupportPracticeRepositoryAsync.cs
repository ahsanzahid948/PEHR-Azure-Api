using Domain.Entities.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Support
{
    public interface ISupportPracticeRepositoryAsync : IGenericRepositoryAsync<PracticeSetup>
    {
        Task<IReadOnlyList<PracticeSetup>> GetPracticeSetupById(long entityId);
        Task<int> UpdatePracticeSetupDetail(string status, long practiceId, long entityId);
    }
}
