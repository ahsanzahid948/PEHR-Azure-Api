using Domain.Entities.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.Auth.Client;
using Application.DTOs.Entity;
using Domain.Entities.Support;
using Application.DTOs.Common.CreateRequestResponse;

namespace Application.Interfaces.Repositories.Auth
{
    public interface IPracticeRepositoryAsync : IGenericRepositoryAsync<Practice>
    {
        Task<List<Practice>> GetPracticeProfileData(PayLoad payload, long entitySeqNum);

        Task<ClientComments_VM> GetPracticeClientComments(PayLoad payload, long entitySeqNum);
        Task<List<Audit>> GetPracticeAuditData(PayLoad payload, long entitySeqNum);

        Task<int> SaveClientComments(PayLoad payload, long entitySeqNum, string comment);


        Task<List<Provider>> GetProviderData(PayLoad payload, long Seqnum);

        Task<Provider> GetProviderDetail(PayLoad payload, long authEntitySeqNum, long providerSeqNum);

        Task<string> UpdateClientEntityProfile(ClientProfileViewModel clientProf, EntityViewModel entity);

        Task<string> UpdateClientConfiguration(long seqNum, long eSeqNum, string flag, string visible);

        Task<IReadOnlyList<PracticeComments>> GetPracticeCommentsByIdAsync(long id);
        Task<int> AddCommentAsync(PracticeComments comment);

        Task<string> GetMultiPracticeIdsAsync(bool allPracticeUser, long entityId, long authEntityId, string dbServer);

        Task<IReadOnlyList<PracticeHelp>> GetPracticeHelpAsync();
        Task<CreateRequestResponse> AddPracticeAsync(Practice practice);

        Task<int> UpdatePracticePatchAsync(Practice practice);
    }
}
