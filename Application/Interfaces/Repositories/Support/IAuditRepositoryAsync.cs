using Domain.Entities.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Support
{
    public interface IAuditRepositoryAsync : IGenericRepositoryAsync<MenuRoleAudit>
    {
        Task<IReadOnlyList<MenuRoleAudit>> GetMenuRoleAuditAsync(string roleName);
        Task<IReadOnlyList<AuditAppUser>> GetUserAuditAsync(string userName); 
    }
}
