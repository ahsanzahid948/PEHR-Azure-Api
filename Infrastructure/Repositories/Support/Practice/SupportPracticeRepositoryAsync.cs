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

namespace Infrastructure.Repositories.Support.Practice
{
    public class SupportPracticeRepositoryAsync : ISupportPracticeRepositoryAsync
    {
        private readonly IConfiguration configuration;
        public SupportPracticeRepositoryAsync(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Task<int> AddAsync(PracticeSetup entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<PracticeSetup>> GetAllAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<PracticeSetup> GetByFilterAsync(string filterCriteria)
        {
            throw new NotImplementedException();
        }

        public Task<PracticeSetup> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(PracticeSetup entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<PracticeSetup>> GetPracticeSetupById(long entityId)
        {
            string query = "SELECT SEQ_NUM,TITLE,DESCRIPTION,AUTO_STATUS,ENTITY_SEQ_NUM,STATUS,SUPPORT_MESSAGE FROM V_AUTH_PRACTICE_DETAIL WHERE ENTITY_SEQ_NUM = :ENTITY_SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {

                var response = await connection.QueryAsync<Domain.Entities.Support.PracticeSetup>(query, new { ENTITY_SEQ_NUM = entityId });

                return response.ToList();
            }
        }

        public async Task<int> UpdatePracticeSetupDetail(string status, long practiceId, long entityId)
        {
            string query = "UPDATE PRACTICE_SETUP_DETAIL SET STATUS = :STATUS WHERE PRACTICE_LIST_SEQ_NUM = :PRACTICE_LIST_SEQ_NUM AND ENTITY_SEQ_NUM = :ENTITY_SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(query, new { STATUS = status, PRACTICE_LIST_SEQ_NUM = practiceId, ENTITY_SEQ_NUM = entityId });
                return result;
            }
        }
    }
}
