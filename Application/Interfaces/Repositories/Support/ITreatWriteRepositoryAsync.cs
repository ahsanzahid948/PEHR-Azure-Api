using Domain.Entities.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Support
{
    public interface ITreatWriteRepositoryAsync : IGenericRepositoryAsync<SSOURL>
    {
        Task<IReadOnlyList<SSOURL>> GetServersByIdAsync(long entityId, string type);
        Task<SSOInfo> GetCredentialsByQueryAsync(long entityId, string type, string email);
        Task<int> CreateCredentialsAsync(SSOInfo credential);
        Task<int> UpdateCredentialsAsync(long credentialId, string user, string password, string type);
    }
}
