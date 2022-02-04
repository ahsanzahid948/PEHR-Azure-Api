using Application.Interfaces.Repositories;
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

namespace Infrastructure.Repositories.Support.TreatWrite
{
    public class TreatWriteRepositoryAsync : ITreatWriteRepositoryAsync
    {
        private readonly IConfiguration configuration;
        public TreatWriteRepositoryAsync(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Task<int> AddAsync(SSOURL entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<SSOURL>> GetAllAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<SSOURL> GetByFilterAsync(string filterCriteria)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(SSOURL entity)
        {
            throw new NotImplementedException();
        }

        Task<SSOURL> IGenericRepositoryAsync<SSOURL>.GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<SSOURL>> GetServersByIdAsync(long entityId, string type)
        {
            string query = "SELECT SEQ_NUM,SSO_NAME,VALIDATION_URL,POST_URL,ENCRYPTION_KEY,ENTITY_SEQ_NUM FROM SSO_URLS WHERE UPPER(SSO_NAME) = :SSO_NAME  AND (ENTITY_SEQ_NUM= :ENTITY_SEQ_NUM OR ENTITY_SEQ_NUM = 0) ORDER BY ENTITY_SEQ_NUM DESC";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {

                var response = await connection.QueryAsync<SSOURL>(query, new { ENTITY_SEQ_NUM = entityId, SSO_NAME = type.ToUpper() });

                return response.ToList();
            }
        }

        public async Task<SSOInfo> GetCredentialsByQueryAsync(long entityId, string type, string email)
        {
            string query = "SELECT SEQ_NUM,SSO_USER,SSO_PASSWORD,PEHR_USER_EMAIL,ENTERED_BY,ENTRY_DATE,SSO_TYPE,ENTITY_SEQ_NUM FROM SSO_INFO WHERE UPPER(PEHR_USER_EMAIL) = :PEHR_USER_EMAIL AND UPPER(SSO_TYPE)=:SSO_TYPE AND ENTITY_SEQ_NUM=:ENTITY_SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                var response = await connection.QueryFirstOrDefaultAsync<SSOInfo>(query, new { ENTITY_SEQ_NUM = entityId, SSO_TYPE = type.ToUpper(), PEHR_USER_EMAIL = email.ToUpper() });
                return response;
            }
        }

        public async Task<int> CreateCredentialsAsync(SSOInfo credential)
        {
            var sql = "INSERT INTO SSO_INFO (SEQ_NUM,SSO_USER,SSO_PASSWORD,SSO_TYPE,PEHR_USER_EMAIL,ENTITY_SEQ_NUM) VALUES(PRIMARY_SEQ.NEXTVAL,:SSO_USER,:SSO_PASSWORD,:SSO_TYPE,:PEHR_USER_EMAIL,:ENTITY_SEQ_NUM)";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, credential);
                return result;
            }
        }

        public async Task<int> UpdateCredentialsAsync(long credentialId, string user, string password, string type)
        {
            var sql = "UPDATE SSO_INFO SET SSO_USER = :SSO_USER,SSO_PASSWORD= :SSO_PASSWORD,SSO_TYPE= : SSO_TYPE WHERE SEQ_NUM = :SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, new { SSO_USER = user, SSO_PASSWORD = password, SSO_TYPE = type, SEQ_NUM = credentialId });
                return result;
            }
        }
    }
}
