using Application.DTOs;
using Application.DTOs.Common.CreateRequestResponse;
using Domain.Entities;
using Domain.Entities.Auth;
using Domain.Entities.Notes;
using Domain.Entities.Support;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ISupportUserRepositoryAsync : IGenericRepositoryAsync<User>
    {
        Task<IReadOnlyList<User>> GetByEntityIdAsync(int pageNumber, int pageSize, int id);
        Task<int> UpdateAsync(Kiosk entity);
        Task<IReadOnlyList<User>> GetUserInfo(int pageNumber, int pageSize, int id);
        Task<User> UserLoginAsync(string email, string password);
        Task<SupportUserInfo> GetUserInfo(long seqNum);
        Task<int> UpdateUserPassword(long seqNum,string password);
        Task<IReadOnlyList<UserPreferences>> AuthUserNotificationSettingsAsync(string seqnum);
        Task<ClientDetail_VM> EntityManagersAsync(PayLoad payload);
        Task<string> CreateSupportUser(SupportUserInfo supportUser, PayLoad payload);
        Task<IReadOnlyList<ClientsList>> GetClientListAsync(PayLoad payload, string ddlSalesMgr, string ddlImplement, string ddlCust);
        Task<AssignedTicket_VM> SupportAssignedTicketsAsync(PayLoad payload, string entity, string teamseqnum);
        Task<CreateRequestResponse> CreateLoginLogAsync(LoginLog loginLog);
        Task<int> UpdateLoginLogAsync(long lodinLodId);
        Task<ClientsList> GetImplementationManager(long seqNum);

        Task<List<SupportUserInfo>> GetFilteredUsersAsync(string FName, string LName, string Email, string Type, string Active, string AccountActivated);
    }
}