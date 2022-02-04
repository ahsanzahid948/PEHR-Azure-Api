namespace Infrastructure.Repositories.Auth.MenuRole
{
    using Application.Interfaces.Repositories.Auth;
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Configuration;
    using Oracle.ManagedDataAccess.Client;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dapper;
    using Application.Interfaces.Repositories;
    using Domain.Entities.Auth;
    using System.Data;
    using Application.DTOs.Common.CreateRequestResponse;
    using Application.DTOs.Common.PagingResponse;
    using Infrastructure.Utilities;
    using Application.Parameters;

    public class MenuRoleRepositoryAsync : IMenuRoleRepositoryAsync
    {
        private readonly IConfiguration configuration;
        public MenuRoleRepositoryAsync(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<CreateRequestResponse> AddAsync(Domain.Entities.Auth.MenuRole menuRole)
        {
            var sql = "INSERT INTO WEB_MENU_ROLE (SEQ_NUM, SHORT_NAME, COMMENTS, ENTITY_SEQ_NUM, IS_DEFAULT) VALUES (PRIMARY_SEQ.NEXTVAL, :SHORT_NAME, :COMMENTS, :ENTITY_SEQ_NUM, :IS_DEFAULT) returning SEQ_NUM into: SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                DynamicParameters param = new DynamicParameters(menuRole);
                param.Add(name: "SEQ_NUM", dbType: DbType.Double, direction: ParameterDirection.Output);
                connection.Open();
                var result = await connection.ExecuteAsync(sql, param);
                var Id = param.Get<Double>("SEQ_NUM");
                return new CreateRequestResponse { Status = result, Id = Id };
            }
        }

        public async Task<int> DeleteAsync(long id)
        {
            var sql = "DELETE FROM WEB_AUTH.WEB_MENU_ROLE WHERE SEQ_NUM=:SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { SEQ_NUM = id });
                return result;
            }
        }

        public Task<IReadOnlyList<Domain.Entities.Auth.MenuRole>> GetAllAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Auth.MenuRole> GetByFilterAsync(string filterCriteria)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Auth.MenuRole> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<PageResponse<Domain.Entities.Auth.MenuRole>> GetByIdAsync(long id, string defaultEnable, string sortName, string sortOrder, RequestPageParameter requestPageParameter)
        {
            var sql = "SELECT SEQ_NUM, SHORT_NAME, COMMENTS, ENTITY_SEQ_NUM, IS_DEFAULT FROM WEB_MENU_ROLE WHERE ENTITY_SEQ_NUM = :ENTITY_SEQ_NUM OR IS_DEFAULT ='Y'";
            var totalRowsSql = "SELECT COUNT (SEQ_NUM) AS TOTAL_ROWS FROM WEB_MENU_ROLE WHERE ENTITY_SEQ_NUM = :ENTITY_SEQ_NUM OR IS_DEFAULT ='Y'";

            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var total = int.Parse((await connection.ExecuteScalarAsync(totalRowsSql, new { ENTITY_SEQ_NUM = id }))?.ToString());

                DynamicParameters parameters = new DynamicParameters();
                sql = Utility.FilterQuery(sql, null, sortName, sortOrder, out parameters);
                sql = requestPageParameter != null ? Utility.PaginatedQuery(sql, requestPageParameter) : sql;
                var result = await connection.QueryAsync<Domain.Entities.Auth.MenuRole>(sql, new { ENTITY_SEQ_NUM = id });
                return new PageResponse<Domain.Entities.Auth.MenuRole>(result.ToList(), total);
            }
        }

        public async Task<IReadOnlyList<MenuButton>> GetMenuButtonsByIdsAsync(string id)
        {
            var sql = "SELECT SEQ_NUM, NAME, TITLE , ICON, SORT_ORDER, TAB_MENU_SEQ_NUM, MENU_SEQ_NUM, HEADING, CATEGORY_TYPE FROM WEB_MENU_BUTTONS";

            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                if (string.IsNullOrEmpty(id))
                {
                    sql += " WHERE VISIBLE = :VISIBLE ORDER BY NAME";
                    var result = await connection.QueryAsync<MenuButton>(sql, new { VISIBLE = "Y" });
                    return result.ToList();
                }
                else
                {
                    sql += " WHERE NVL(TAB_MENU_SEQ_NUM,0) NOT IN :ID AND VISIBLE = :VISIBLE ORDER BY NAME";
                    List<int> seqNums = id.Split(',').Select(Int32.Parse).ToList();
                    var result = await connection.QueryAsync<MenuButton>(sql, new { ID = seqNums, VISIBLE = "Y" });
                    return result.ToList();
                }
            }
        }

        public async Task<IReadOnlyList<Menu>> GetMenusByIdsAsync(string id)
        {
            var sql = "SELECT SEQ_NUM, NAME, TITLE , ICON, SORT_ORDER, TAB_MENU_SEQ_NUM, CATEGORY_TYPE FROM WEB_MENUES_PROF";

            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                if (string.IsNullOrEmpty(id))
                {
                    sql += " WHERE VISIBLE = :VISIBLE ORDER BY NAME";
                    var result = await connection.QueryAsync<Menu>(sql, new { VISIBLE = "Y" });
                    return result.ToList();
                }
                else
                {
                    List<int> seqNums = id.Split(',').Select(Int32.Parse).ToList();
                    sql += " WHERE TAB_MENU_SEQ_NUM NOT IN :ID  AND VISIBLE = :VISIBLE ORDER BY NAME";
                    var result = await connection.QueryAsync<Menu>(sql, new { ID = seqNums, VISIBLE = "Y" });
                    return result.ToList();
                }
            }
        }

        public async Task<IReadOnlyList<TabMenu>> GetMenuTabsByIdsAsync(string id)
        {
            var sql = "SELECT SEQ_NUM, NAME, TITLE, ICON, SORT_ORDER, FOR_ERX, FOR_ADMIN FROM WEB_TAB_MENU";

            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                if (string.IsNullOrEmpty(id))
                {
                    var result = await connection.QueryAsync<TabMenu>(sql);
                    return result.ToList();
                }
                else
                {
                    List<int> seqNums = id.Split(',').Select(Int32.Parse).ToList();
                    sql += " WHERE SEQ_NUM NOT IN :ID";
                    var result = await connection.QueryAsync<TabMenu>(sql, new { ID = seqNums });
                    return result.ToList();
                }
            }
        }

        public async Task<IReadOnlyList<RoleAssignment>> GetPrivilegesByIdAsync(long id)
        {
            var sql = "SELECT SEQ_NUM, ROLE_SEQ_NUM, MENU_SEQ_NUM, TAB_MENU_SEQ_NUM, MENU_BUTTON_SEQ_NUM, ASSIGNMENT_FLAG, ENTERED_BY, ENTRY_DATE FROM WEB_MENU_ROLE_ASSIGNMENT WHERE ROLE_SEQ_NUM = :ROLE_SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<RoleAssignment>(sql, new { ROLE_SEQ_NUM = id });
                return result.ToList();
            }
        }

        public async Task<Domain.Entities.Auth.MenuRole> MenuRoleByRoleIdAsync(long id)
        {
            var sql = "SELECT SEQ_NUM, SHORT_NAME, COMMENTS, ENTITY_SEQ_NUM, IS_DEFAULT FROM WEB_MENU_ROLE WHERE SEQ_NUM = :SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<Domain.Entities.Auth.MenuRole>(sql, new { SEQ_NUM = id });
                return result;
            }
        }

        public Task<int> UpdateAsync(Domain.Entities.Auth.MenuRole entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdatePrivilegesAsync(List<RoleAssignment> roleAssignment)
        {

            var sql = "UPDATE WEB_MENU_ROLE_ASSIGNMENT SET ROLE_SEQ_NUM =:ROLE_SEQ_NUM, MENU_SEQ_NUM =:MENU_SEQ_NUM, TAB_MENU_SEQ_NUM =:TAB_MENU_SEQ_NUM, MENU_BUTTON_SEQ_NUM =:MENU_BUTTON_SEQ_NUM, ASSIGNMENT_FLAG =:ASSIGNMENT_FLAG WHERE SEQ_NUM = :SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, roleAssignment);
                return result;
            }
        }

        Task<int> IGenericRepositoryAsync<MenuRole>.AddAsync(MenuRole entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<UserTab>> GetUserTabsByIdsAsync(string id, List<long> seqNums)
        {
            var sql = "SELECT ROLE, ROLE_SEQ_NUM, SEQ_NUM, ID, NAME, ASSIGNMENT_FLAG FROM WEB_V_TAB";

            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                if (seqNums == null)
                {
                    sql += " WHERE ROLE_SEQ_NUM = :ROLE_SEQ_NUM";
                    var result = await connection.QueryAsync<UserTab>(sql, new { ROLE_SEQ_NUM = id});
                    return result.ToList();
                }
                else
                {
                    sql += " WHERE ROLE_SEQ_NUM = :ROLE_SEQ_NUM  AND SEQ_NUM NOT IN :ID";
                    var result = await connection.QueryAsync<UserTab>(sql, new { ROLE_SEQ_NUM = id, ID = seqNums });
                    return result.ToList();
                }
            }
        }

        public async Task<IReadOnlyList<UserMenu>> GetUserMenusByIdsAsync(string id, List<long> seqNums)
        {
            var sql = "SELECT  ROLE, ROLE_SEQ_NUM, SEQ_NUM, TAB_SEQ_NUM, ID, NAME, ASSIGNMENT_FLAG, TAB, CATEGORY_TYPE FROM WEB_V_MENU";

            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                if (seqNums == null)
                {
                    sql += " WHERE ROLE_SEQ_NUM = :ROLE_SEQ_NUM";
                    var result = await connection.QueryAsync<UserMenu>(sql, new { ROLE_SEQ_NUM = id });
                    return result.ToList();
                }
                else
                {
                    sql += " WHERE ROLE_SEQ_NUM = :ROLE_SEQ_NUM  AND TAB_SEQ_NUM NOT IN :ID";
                    var result = await connection.QueryAsync<UserMenu>(sql, new { ROLE_SEQ_NUM = id, ID = seqNums });
                    return result.ToList();
                }
            }
        }

        public async Task<IReadOnlyList<UserButton>> GetUserButtonsByIdsAsync(string id, List<long> seqNums)
        {
            var sql = "SELECT  ROLE, ROLE_SEQ_NUM, SEQ_NUM, MENU_SEQ_NUM, TAB_SEQ_NUM, ID, NAME, ASSIGNMENT_FLAG, TAB, MENU, CATEGORY_TYPE FROM WEB_V_BUTTON";

            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                if (seqNums == null)
                {
                    sql += " WHERE ROLE_SEQ_NUM = :ROLE_SEQ_NUM";
                    var result = await connection.QueryAsync<UserButton>(sql, new { ROLE_SEQ_NUM = id });
                    return result.ToList();
                }
                else
                {
                    sql += " WHERE ROLE_SEQ_NUM = :ROLE_SEQ_NUM  AND NVL(TAB_MENU_SEQ_NUM,0) NOT IN :ID";
                    var result = await connection.QueryAsync<UserButton>(sql, new { ROLE_SEQ_NUM = id, ID = seqNums });
                    return result.ToList();
                }
            }
        }

        public async Task<MenuRole> MenuRoleByNameAsync(string name)
        {
            var sql = "SELECT SEQ_NUM, SHORT_NAME, COMMENTS, ENTITY_SEQ_NUM, IS_DEFAULT FROM WEB_MENU_ROLE WHERE UPPER(SHORT_NAME) = :SHORT_NAME";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<Domain.Entities.Auth.MenuRole>(sql, new { SHORT_NAME = name.ToUpper() });
                return result;
            }
        }

        public async Task<int> DeleteRolePrivilegesAsync(long roleId)
        {
            var sql = "DELETE FROM WEB_AUTH.WEB_MENU_ROLE_ASSIGNMENT WHERE ROLE_SEQ_NUM=:ROLE_SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { ROLE_SEQ_NUM = roleId });
                return result;
            }
        }

        public async Task<int> CreatePrivilegesAsync(List<RoleAssignment> menuRole)
        {
            var sql = "INSERT INTO WEB_MENU_ROLE_ASSIGNMENT(SEQ_NUM, ROLE_SEQ_NUM, MENU_SEQ_NUM, TAB_MENU_SEQ_NUM, MENU_BUTTON_SEQ_NUM, ASSIGNMENT_FLAG) " +
                       "VALUES (PRIMARY_SEQ.NEXTVAL, :ROLE_SEQ_NUM, :MENU_SEQ_NUM, :TAB_MENU_SEQ_NUM, :MENU_BUTTON_SEQ_NUM, :ASSIGNMENT_FLAG)";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, menuRole);
                return result;
            }
        }
    }
}

