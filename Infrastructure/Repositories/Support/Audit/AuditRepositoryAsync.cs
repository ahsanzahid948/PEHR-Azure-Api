using Application.Interfaces.Repositories.Support;
using Dapper;
using Domain.Entities.Support;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Support.Audit
{
    public class AuditRepositoryAsync : IAuditRepositoryAsync
    {
        private readonly IConfiguration configuration;
        public AuditRepositoryAsync(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Task<int> AddAsync(MenuRoleAudit entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<MenuRoleAudit>> GetAllAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }


        public Task<MenuRoleAudit> GetByFilterAsync(string filterCriteria)
        {
            throw new NotImplementedException();
        }

        public Task<MenuRoleAudit> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(MenuRoleAudit entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<MenuRoleAudit>> GetMenuRoleAuditAsync(string roleName)
        {
            var sql = "SELECT SEQ_NUM, DATA_TABLE_NAME, AUDIT_TABLE_NAME, DATA_COL_NAME, DISPLAY_COL_NAME, PRIMARY_KEY, ROW_SEQ_NUM, COL_TYPE, ORG_VALUE, CURRENT_VALUE, AUDIT_USER, AUDIT_TIMESTAMP, AUDIT_ACTION, UNIQUE_NAME, ROLE_NAME, VISIBLE_FLAG, CASE WHEN MENU_ROLE IS NULL THEN MENU_BUTTON ELSE MENU_ROLE END MENU_ROLE FROM AUTH_AUDIT_TABLE WHERE UPPER(ROLE_NAME) = :ROLE_NAME";

            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<MenuRoleAudit>(sql, new { ROLE_NAME = roleName.ToUpper() });
                return result.ToList();

            }
        }

        public async Task<IReadOnlyList<AuditAppUser>> GetUserAuditAsync(string userName)
        {
            var sql = "SELECT SEQ_NUM, DATA_TABLE_NAME, AUDIT_TABLE_NAME, DATA_COL_NAME, DISPLAY_COL_NAME, PRIMARY_KEY, ROW_SEQ_NUM, COL_TYPE, ORG_VALUE, CURRENT_VALUE, AUDIT_USER, AUDIT_TIMESTAMP, AUDIT_ACTION, UNIQUE_NAME, USER_NAME, VISIBLE_FLAG FROM AUDIT_APP_USER WHERE UPPER(USER_NAME) = :USER_NAME";

            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<AuditAppUser>(sql, new { USER_NAME = userName.ToUpper() });
                return result.ToList();

            }
        }
    }
}
