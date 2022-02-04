using Application.Interfaces.Repositories.Support;
using Dapper;
using Domain.Entities.Support;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories.Support.Entity
{
    class SupportEntityRepositoryAsync : ISupportEntityRepositoryAsync
    {
        private readonly IConfiguration configuration;
        public SupportEntityRepositoryAsync(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Task<CustomReport> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomReport> GetByFilterAsync(string filterCriteria)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<CustomReport>> GetAllAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(CustomReport entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }


        public Task<int> UpdateAsync(Domain.Entities.Support.CustomReport customReport)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<CustomReport>> GetCustomReportsByIdAsync(long id)
        {
            var sql = "SELECT SEQ_NUM, ENTITY_SEQ_NUM, REPORT_DESCRIPTION, REPORT_NAME, ENTERED_BY, ENTRY_DATE, TELERIK_REPORT_ID, SORT_ORDER, CUST_VIEW FROM WEB_AUTH.CUSTOM_REPORTS_ENTITY WHERE ENTITY_SEQ_NUM = :ENTITY_SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<CustomReport>(sql, new { ENTITY_SEQ_NUM = id });
                return result.ToList();
            }
        }

        public async Task<int> CreateMergedDocumentAsync(MergedDocument mergedDocument)
        {
            var sql = "INSERT INTO WEB_AUTH.PDF_MERGED_DOCUMENT (SEQ_NUM,AUTH_SEQ_NUM,PATIENT_SEQ_NUM,DOCUMENT_SEQ_NUM,ASSIGNEE_USER) VALUES(PRIMARY_SEQ.NEXTVAL,:AUTH_SEQ_NUM,:PATIENT_SEQ_NUM,:DOCUMENT_SEQ_NUM,:ASSIGNEE_USER)";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, mergedDocument);
                return result;
            }
        }
    }
}
