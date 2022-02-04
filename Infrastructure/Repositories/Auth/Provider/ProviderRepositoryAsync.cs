using Application.DTOs.Common.PagingResponse;
using Application.Interfaces.Repositories.Auth;
using Application.Parameters;
using Dapper;
using Infrastructure.Utilities;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Auth.Provider
{
    public class ProviderRepositoryAsync : IProviderRepositoryAsync
    {
        private readonly IConfiguration configuration;
        public ProviderRepositoryAsync(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<int> AddAsync(Domain.Entities.Auth.Provider provider)
        {
            var sql = "INSERT INTO WEB_AUTH.PROVIDER (SEQ_NUM,FIRST_NAME,LAST_NAME, MIDDLE_INTIAL, NPI, SPECIALITY, TAXONOMY_CODE, " +
                "SSN,  DEA_NO,  QUALIFICATION,  PHONE_NO,  FAX_NO,  ADDRESS, PROVIDER_SIGNATURE ,ENTITTY_SEQ_NUM ) VALUES(PRIMARY_SEQ.NEXTVAL," +
                ":FIRST_NAME, :LAST_NAME, :MIDDLE_INTIAL, :NPI, :SPECIALITY, :TAXONOMY_CODE,  :SSN,  :DEA_NO,  :QUALIFICATION,  :PHONE_NO,  :FAX_NO," +
                ":ADDRESS, :PROVIDER_SIGNATURE,:ENTITTY_SEQ_NUM )";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, provider);
                return result;
            }
        }

        public async Task<int> DeleteAsync(long id)
        {
            var sql = "DELETE FROM WEB_AUTH.PROVIDER WHERE SEQ_NUM=:SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, new { SEQ_NUM = id });
                return result;
            }
        }

        public Task<IReadOnlyList<Domain.Entities.Auth.Provider>> GetAllAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Auth.Provider> GetByFilterAsync(string filterCriteria)
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Entities.Auth.Provider> GetByIdAsync(long id)
        {
            var sql = "SELECT SEQ_NUM ,FIRST_NAME,LAST_NAME,MIDDLE_INTIAL,NPI,SPECIALITY,TAXONOMY_CODE,SSN,DEA_NO,QUALIFICATION,PHONE_NO,FAX_NO,ADDRESS,DME_PTAN,MCR_RR_PTAN,MCR_PTAN,PROVIDER_SIGNATURE,ENTITTY_SEQ_NUM FROM PROVIDER WHERE SEQ_NUM =:SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<Domain.Entities.Auth.Provider>(sql, new { SEQ_NUM = id });
                return result;
            }
        }

        public async Task<int> UpdateAsync(Domain.Entities.Auth.Provider provider)
        {
            var sql = "UPDATE WEB_AUTH.PROVIDER SET FIRST_NAME=:FIRST_NAME,LAST_NAME=:LAST_NAME, MIDDLE_INTIAL=:MIDDLE_INTIAL, " +
                "NPI=:NPI, SPECIALITY=:SPECIALITY, TAXONOMY_CODE=:TAXONOMY_CODE,  SSN=:SSN,  DEA_NO=:DEA_NO,  QUALIFICATION=:QUALIFICATION,  " +
                "PHONE_NO=:PHONE_NO, FAX_NO=:FAX_NO,  ADDRESS=:ADDRESS, PROVIDER_SIGNATURE=:PROVIDER_SIGNATURE WHERE SEQ_NUM=:SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, provider);
                return result;
            }
        }

        public async Task<PageResponse<Domain.Entities.Auth.Provider>> GetProvidersByIdAsync(long id, string sortName, string sortOrder, RequestPageParameter requestPageParameter)
        {
            var sql = "SELECT SEQ_NUM, FIRST_NAME, LAST_NAME, SPECIALITY, NPI FROM PROVIDER WHERE ENTITTY_SEQ_NUM =:ENTITTY_SEQ_NUM";
            var totalRowsSql = "SELECT COUNT (SEQ_NUM) AS TOTAL_ROWS FROM PROVIDER WHERE ENTITTY_SEQ_NUM =:ENTITTY_SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var total = int.Parse((await connection.ExecuteScalarAsync(totalRowsSql, new { ENTITTY_SEQ_NUM = id }))?.ToString());

                DynamicParameters parameters = new DynamicParameters();
                sql = Utility.FilterQuery(sql, null, sortName, sortOrder, out parameters);
                sql = requestPageParameter != null ? Utility.PaginatedQuery(sql, requestPageParameter) : sql;
                var result = await connection.QueryAsync<Domain.Entities.Auth.Provider>(sql, new { ENTITTY_SEQ_NUM = id });
                return new PageResponse<Domain.Entities.Auth.Provider>(result.ToList(), total);
            }
        }

        public async Task<Domain.Entities.Auth.Provider> GetProviderDetailByIdAsync(long id)
        {
            var sql = "SELECT SEQ_NUM,FIRST_NAME,LAST_NAME,MIDDLE_INTIAL,NPI,SPECIALITY,TAXONOMY_CODE,SSN,DEA_NO,QUALIFICATION,PHONE_NO,FAX_NO,ADDRESS,DME_PTAN,MCR_RR_PTAN,MCR_PTAN,PROVIDER_SIGNATURE,ENTITTY_SEQ_NUM FROM PROVIDER WHERE SEQ_NUM =:SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<Domain.Entities.Auth.Provider>(sql, new { SEQ_NUM = id });
                return result;
            }
        }

        public async Task<int> UpdatePatchAsync(Domain.Entities.Auth.Provider provider)
        {
            var sql = "UPDATE PROVIDER SET PROVIDER_SIGNATURE=:PROVIDER_SIGNATURE WHERE SEQ_NUM=:SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, provider);
                return result;
            }
        }
    }
}
