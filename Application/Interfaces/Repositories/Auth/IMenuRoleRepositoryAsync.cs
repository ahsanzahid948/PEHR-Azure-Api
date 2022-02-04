using Application.DTOs.Common.CreateRequestResponse;
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
    public interface IMenuRoleRepositoryAsync : IGenericRepositoryAsync<MenuRole>
    {
        Task<PageResponse<MenuRole>> GetByIdAsync(long id,string defaultEnable, string sortName, string sortOrder, RequestPageParameter requestPageParameter);
        Task<MenuRole> MenuRoleByRoleIdAsync(long id);
        Task<IReadOnlyList<TabMenu>> GetMenuTabsByIdsAsync(string id);
        Task<IReadOnlyList<MenuButton>> GetMenuButtonsByIdsAsync(string id);
        Task<IReadOnlyList<Menu>> GetMenusByIdsAsync(string id);
        Task<IReadOnlyList<RoleAssignment>> GetPrivilegesByIdAsync(long id);
        Task<int> UpdatePrivilegesAsync(List<RoleAssignment> roleAssignment);
        new Task<CreateRequestResponse> AddAsync(MenuRole menuRole);
        Task<IReadOnlyList<UserTab>> GetUserTabsByIdsAsync(string id, List<long> seqNums);
        Task<IReadOnlyList<UserMenu>> GetUserMenusByIdsAsync(string id, List<long> seqNums);
        Task<IReadOnlyList<UserButton>> GetUserButtonsByIdsAsync(string id, List<long> seqNums);
        Task<MenuRole> MenuRoleByNameAsync(string name);
        Task<int> CreatePrivilegesAsync(List<RoleAssignment> menuRole);
        Task<int> DeleteRolePrivilegesAsync(long roleId);
    }
}
