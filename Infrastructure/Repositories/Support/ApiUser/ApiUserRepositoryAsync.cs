using Application.Interfaces.Repositories;
using Dapper;
using Domain.Entities.Support;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ApiUserRepositoryAsync : IApiUserRepositoryAsync
    {

        private readonly IConfiguration configuration;
        public ApiUserRepositoryAsync(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<int> AddAsync(ApiUser apiUser)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> DeleteAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyList<ApiUser>> GetAllAsync(int pageNumber, int pageSize)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ApiUser> GetByFilterAsync(string filterCriteria)
        {
            throw new System.NotImplementedException();
        }
        public async Task<IReadOnlyList<ApiAccessLog>> GetAccessLogByFilterAsync(string filterCriteria)
        {
            var sql = "SELECT SEQ_NUM,EMAIL,DATA_ACCESSED,ACCESSED_DATE,IP_ADDRESS,ACCESS_TOKEN FROM WEB_AUTH.API_ACCESS_LOG WHERE UPPER(EMAIL)= :email";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<ApiAccessLog>(sql, new { email = filterCriteria });
                return result.ToList();
            }
        }
        public async Task<ApiUser> GetByIdAsync(long id)
        {
            var sql = "SELECT * FROM WEB_AUTH.API_USERS WHERE PATIENT_SEQ_NUM =:Id";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<ApiUser>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> CreateApiUserAsync(ApiUser user)
        {
            var sql = "INSERT INTO  WEB_AUTH.API_USERS(SEQ_NUM,FIRST_NAME,LAST_NAME,DOB,GENDER,PATIENT_SEQ_NUM,CLIENT_ID,PROV_ALLOW_ACCESS,PAT_ALLOW_ACCESS,USER_NAME,ACCESS_TOKEN,ENTRY_DATE,LAST_MODIFIED_DATE,PASSWORD) VALUES(PRIMARY_SEQ.NEXTVAL,:FIRST_NAME,:LAST_NAME,:DOB,:GENDER,:PATIENT_SEQ_NUM,:CLIENT_ID,:PROV_ALLOW_ACCESS,:PAT_ALLOW_ACCESS,:USER_NAME,:ACCESS_TOKEN,SYSDATE,SYSDATE,:PASSWORD)";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, user);
                return result;
            }
        }
        public async Task<int> UpdateAsync(ApiUser apiUser)
        {
            var sql = "UPDATE WEB_AUTH.API_USERS SET PAT_ALLOW_ACCESS=:AllowAccess,PASSWORD=:Password WHERE PATIENT_SEQ_NUM =:Id";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new
                {
                    AllowAccess = apiUser.Pat_Allow_Access,
                    Password = apiUser.Password,
                    Id = apiUser.Patient_Seq_Num
                });
                return result;
            }
        }
    }
}



