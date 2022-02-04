using Application.DTOs.Common.CreateRequestResponse;
using Application.DTOs.Common.PagingResponse;
using Application.Parameters;
using Domain.Entities.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepositoryAsync : IGenericRepositoryAsync<User>
    {
        Task<IReadOnlyList<User>> GetByEntityIdAsync(string id, string filter);
        Task<int> UpdateAsync(Kiosk entity);
        Task<User> GetEPCSSettingsByEmailAsync(string email);
        Task<IReadOnlyList<DataExportSetting>> GetDataExportSettingsByIdAsync(long id);
        Task<int> UpdateEPCSSettingAsync(User user);
        Task<int> CreateDataExportSettingAsync(DataExportSetting dataExportSetting);
        Task<int> UpdateDataExportSettingAsync(DataExportSetting dataExportSetting);
        Task<IReadOnlyList<User>> GetUserSettingsByIdAsync(string email);
        Task<int> UpdateUserSettingAsync(User user);
        Task<PageResponse<User>> GetUsersByFilterAsync(string sortname, string sortorder, RequestPageParameter requestPageParameter, string userId, string active, string firstName, string lastName, string email);
        Task<IReadOnlyList<UserEntities>> GetUsersEntitesAsync(string email, string epcs, string approveEpcs, string authEntityId);
        Task<CreateRequestResponse> AddUserLogAsync(AuditSession userLog, bool authSequence);
        Task<int> UpdateUserLogAsync(long userLogSeqNum);
        Task<int> UpdateUserTokenAsync(string email, string token);
        Task<int> UpdateUserPasswordAsync(string token, string password);
        Task<int> UpdateUserPasswordByEmailAsync(string email, string password);
        Task<User> GetByFilterAsync(string email, string token);
        Task<int> UpdateEPCSApproveAsync(List<long> userId, string epcs2ndApproval, string epcsApproval);
        Task<int> DeleteDataExportSettingAsync(long settingId);
        Task<int> GetUserProviderCountAsync(string email, long providerId);
        Task<IReadOnlyList<User>> GetUserbyTokeAsync(string token);
        Task<IReadOnlyList<User>> GetUserbyMenuRoleAsync(string menuRoleId, string emergencyRoleId = "");
        Task<IReadOnlyList<User>> GetInternalUsersAsync(long entityId, bool practiceRcm, string active, string rcmType, string rcmTypeTwo);
    }
}