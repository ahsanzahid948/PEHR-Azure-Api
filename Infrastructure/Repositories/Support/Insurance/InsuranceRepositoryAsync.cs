using Application.DTOs;
using Application.DTOs.Common.PagingResponse;
using Application.Interfaces.Repositories.Support;
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

namespace Infrastructure.Repositories.Support.Insurance
{
    public class InsuranceRepositoryAsync : IInsuranceRepositoryAsync
    {
        private readonly IConfiguration configuration;
        public InsuranceRepositoryAsync(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<int> AddAsync(Domain.Entities.Support.Insurance insurance)
        {
            var sql = "INSERT INTO WEB_AUTH.INSURANCE (SEQ_NUM,INSURANCE_NAME,PAYOR_ID,ENTITTY_SEQ_NUM,SHORT_NAME,PLANTYPE, COMMENTS,BC_BS_FLAG,MEDICARE_FLAG,MEDICAID_FLAG,COMMERCIAL_FLAG,OTHER) " +
               "VALUES (PRIMARY_SEQ.NEXTVAL,:INSURANCE_NAME,:PAYOR_ID,:ENTITTY_SEQ_NUM,:SHORT_NAME,:PLANTYPE,:COMMENTS,:BC_BS_FLAG,:MEDICARE_FLAG,:MEDICAID_FLAG,:COMMERCIAL_FLAG,:OTHER)";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, insurance);
                return result;
            }
        }

        public async Task<int> DeleteAsync(long id)
        {
            var sql = "DELETE FROM WEB_AUTH.INSURANCE WHERE SEQ_NUM=:SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, new { SEQ_NUM = id});
                return result;
            }
        }

        public Task<IReadOnlyList<Domain.Entities.Support.Insurance>> GetAllAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Support.Insurance> GetByFilterAsync(string filterCriteria)
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Entities.Support.Insurance> GetByIdAsync(long id)
        {
            var query = "SELECT SEQ_NUM,INSURANCE_NAME,PAYOR_ID,ENTITTY_SEQ_NUM,SHORT_NAME,DESCRIPTION,PLANTYPE,COMMENTS,BC_BS_FLAG,MEDICARE_FLAG,MEDICAID_FLAG,COMMERCIAL_FLAG,OTHER FROM INSURANCE WHERE SEQ_NUM=:SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var response = await connection.QueryFirstAsync<Domain.Entities.Support.Insurance>(query, new {  SEQ_NUM = id });
                return response;
            }
        }

        public async Task<int> UpdateAsync(Domain.Entities.Support.Insurance insurance)
        {
            var sql = "UPDATE WEB_AUTH.INSURANCE SET INSURANCE_NAME=:INSURANCE_NAME,PAYOR_ID=:PAYOR_ID,SHORT_NAME=:SHORT_NAME" +
                    ",PLANTYPE=:PLANTYPE, COMMENTS=:COMMENTS,BC_BS_FLAG=:BC_BS_FLAG,MEDICARE_FLAG=:MEDICARE_FLAG" +
                    ",MEDICAID_FLAG=:MEDICAID_FLAG,COMMERCIAL_FLAG=:COMMERCIAL_FLAG,OTHER=:OTHER WHERE SEQ_NUM=:SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, insurance);
                return result;
            }
        }

        public async Task<PageResponse<Domain.Entities.Support.Insurance>> GetInsuranceAsync(long entityId, string sortName, string sortOrder, RequestPageParameter requestPageParameter)
        {
            string query = "SELECT SEQ_NUM,INSURANCE_NAME,PAYOR_ID,ENTITTY_SEQ_NUM,SHORT_NAME,DESCRIPTION,PLANTYPE,COMMENTS,BC_BS_FLAG,MEDICARE_FLAG,MEDICAID_FLAG,COMMERCIAL_FLAG,OTHER FROM INSURANCE WHERE ENTITTY_SEQ_NUM=:ENTITTY_SEQ_NUM";
            var totalRowsSql = "SELECT COUNT (SEQ_NUM) AS TOTAL_ROWS FROM INSURANCE WHERE ENTITTY_SEQ_NUM=:ENTITTY_SEQ_NUM";

            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                var total = int.Parse((await connection.ExecuteScalarAsync(totalRowsSql, new { ENTITTY_SEQ_NUM = entityId }))?.ToString());
                DynamicParameters parameters = new DynamicParameters();
                query = Utility.FilterQuery(query, null, sortName, sortOrder, out parameters);
                query = requestPageParameter != null ? Utility.PaginatedQuery(query, requestPageParameter) : query;
                var result = await connection.QueryAsync<Domain.Entities.Support.Insurance>(query, new { ENTITTY_SEQ_NUM = entityId });
                return new PageResponse<Domain.Entities.Support.Insurance>(result.ToList(), total);
            }
        }

    }
}
