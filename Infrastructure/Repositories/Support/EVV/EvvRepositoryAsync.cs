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

namespace Infrastructure.Repositories.Support.EVV
{
    public class EvvRepositoryAsync : IEvvRepositoryAsync
    {
        private readonly IConfiguration configuration;
        public EvvRepositoryAsync(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Task<int> AddAsync(EvvStateTimeZone evvStateTimeZon)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<EvvStateTimeZone>> GetAllAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<EvvStateTimeZone> GetByFilterAsync(string filterCriteria)
        {
            throw new NotImplementedException();
        }

        public Task<EvvStateTimeZone> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(EvvStateTimeZone evvStateTimeZon)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<EvvStateTimeZone>> GetConfigurationsByStateAsync(string state)
        {
            var sql = "SELECT A.SEQ_NUM,A.STATE,A.TIMEZONE,A.QUALIFY,A.PLANFORMAT,A.PROVIDER_EMAIL_REQ,A.APPOINTMENT_REQ,A.PROVIDER_SSN_REQ,A.SSNFORMAT,A.CONTINGENCY_PLAN_REVIEW,A.CONTINGENCY_PLAN,A.AUTHORIZATION_NUMBER,A.EVV_TASKS,A.VISIT_TIME_DISPLAY,A.SERVICES_VERIFIED,A.VISITTIME_VERIFIED,A.EVV_ACK_WIN,A.SIGNATURE,A.VOICE_RECORDING,A.PLAN_VALIDATION,A.TIMEZONE_ID,A.DST,A.STATE1 FROM EVV_STATE_TIMEZONE A WHERE UPPER(A.STATE)=:STATE";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<EvvStateTimeZone>(sql, new { STATE = state.ToUpper() });
                return result.ToList();
            }
        }

        public async Task<IReadOnlyList<EvvTasks>> GetEvvTasksByStateAsync(string state)
        {
            var sql = "SELECT STATE, TASK_ID, TASKNAME FROM EVV_TASKS WHERE UPPER(STATE)= :STATE";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<EvvTasks>(sql, new { STATE = state.ToUpper() });
                return result.ToList();
            }
        }
    }
}
