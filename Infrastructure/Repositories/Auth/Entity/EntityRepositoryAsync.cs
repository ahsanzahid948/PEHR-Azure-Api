using Application.Interfaces.Repositories;
using Dapper;
using Domain.Entities.Auth;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EntityRepositoryAsync : IEntityRepositoryAsync
    {

        private readonly IConfiguration configuration;
        public EntityRepositoryAsync(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<int> AddAsync(Entity entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> DeleteAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyList<Entity>> GetAllAsync(int pageNumber, int pageSize)
        {
            throw new System.NotImplementedException();
        }

        public Task<Entity> GetByFilterAsync(string filterCriteria)
        {
            throw new System.NotImplementedException();
        }
        public async Task<Entity> GetByIdAsync(long id, string dbEntity)
        {
            if (dbEntity.ToUpper() == "AUTH")
            {
                return await GetByIdAsync(id);
            }
            var sql = "Select CEIL(a.TRIAL_END_DATE - sysdate) + 1 AS TRIALDAYS,  a.* from entity a Where entity_seq_num = :Id";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Entity>(sql, new { Id = id });
                return result.FirstOrDefault();
            }
        }
        public async Task<EntityConfiguration> GetConfigurationsByIdAsync(long id)
        {
            var sql = "Select * From entity Where seq_num = :Id";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<EntityConfiguration>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<Entity> GetByIdAsync(long id)
        {
            var sql = "Select CEIL(a.TRIAL_END_DATE - sysdate) + 1 AS TRIALDAYS,  a.* from entity a Where seq_num = :Id";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Entity>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Credentials>> GetCredentialsAsync(long id, string email, string authEntityId)
        {
            //AND SEQ_NUM = :Id

            var sql = "SELECT * FROM WEB_AUTH.V_APP_USER_ACCESS WHERE UPPER(TRIM(EMAIL)) = UPPER(TRIM(:EMAIL)) AND DEFAULT_ENTITY_SEQ_NUM=:DEFAULT_ENTITY_SEQ_NUM";
            if (string.IsNullOrEmpty(authEntityId))
            {
                using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Credentials>(sql, new { EMAIL = email, DEFAULT_ENTITY_SEQ_NUM = id });
                    return result.ToList();
                }
            }
            else
            {
                sql = "SELECT * FROM V_APP_SUPPORT_USER_ACCESS WHERE UPPER(TRIM(EMAIL)) = UPPER(TRIM(:EMAIL)) AND SEQ_NUM = :SEQ_NUM";
                using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Credentials>(sql, new { EMAIL = email, SEQ_NUM = authEntityId });
                    return result.ToList();
                }
            }

        }
        public async Task<int> UpdateAsync(Entity entity)
        {
            throw new System.NotImplementedException();
        }
        public async Task<IReadOnlyList<ProviderLocation>> GetProviderLocationAsync(long id, string email)
        {
            var sql = "SELECT SEQ_NUM, AUTH_ENTITY_SEQ_NUM, LOGIN_USER, PROVIDER_SEQ_NUM, LOCATION_SEQ_NUM FROM NEW_PATIENT_PROVIDER_LOCATION WHERE UPPER(LOGIN_USER) =: LOGIN_USER AND AUTH_ENTITY_SEQ_NUM = :AUTH_ENTITY_SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<ProviderLocation>(sql, new { LOGIN_USER = email.ToUpper(), AUTH_ENTITY_SEQ_NUM = id });

                return result.ToList();
            }
        }

        public async Task<IReadOnlyList<MultiProvider>> GetMultiProviderAsync(long id, long userId)
        {
            var sql = "SELECT SEQ_NUM, PROVIDER_SEQ_NUM, AUTH_USER_SEQ_NUM, AUTH_ENTITY_SEQ_NUM FROM MULTI_VIEW_PROVIDER_LIST WHERE AUTH_USER_SEQ_NUM =: AUTH_USER_SEQ_NUM AND AUTH_ENTITY_SEQ_NUM =:AUTH_ENTITY_SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<MultiProvider>(sql, new { AUTH_USER_SEQ_NUM = userId, AUTH_ENTITY_SEQ_NUM = id });

                return result.ToList();
            }
        }

        public async Task<IReadOnlyList<ProviderAgent>> GetProviderAgentsAsync(long id, long providerId)
        {
            var sql = "SELECT SEQ_NUM,PROVIDER_SEQ_NUM, USER_AGENT_SEQ_NUM, AUTH_ENTITY_SEQ_NUM FROM USER_PROVIDER_AGENT WHERE AUTH_ENTITY_SEQ_NUM =:AUTH_ENTITY_SEQ_NUM AND PROVIDER_SEQ_NUM= :PROVIDER_SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<ProviderAgent>(sql, new { AUTH_ENTITY_SEQ_NUM = id, PROVIDER_SEQ_NUM = providerId });

                return result.ToList();
            }
        }

        public async Task<IReadOnlyList<ProviderAgent>> GetUserAgentsAsync(long id, long userId)
        {
            var sql = "SELECT PROVIDER_SEQ_NUM,SEQ_NUM  FROM USER_PROVIDER_AGENT WHERE AUTH_ENTITY_SEQ_NUM =:AUTH_ENTITY_SEQ_NUM AND USER_AGENT_SEQ_NUM= :USER_AGENT_SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<ProviderAgent>(sql, new { AUTH_ENTITY_SEQ_NUM = id, USER_AGENT_SEQ_NUM = userId });

                return result.ToList();
            }
        }

        public async Task<int> CreateProviderLocationAsync(ProviderLocation providerLocation)
        {
            var sql = "INSERT INTO NEW_PATIENT_PROVIDER_LOCATION (SEQ_NUM,AUTH_ENTITY_SEQ_NUM,LOGIN_USER,PROVIDER_SEQ_NUM,LOCATION_SEQ_NUM) VALUES(PRIMARY_SEQ.NEXTVAL,:AUTH_ENTITY_SEQ_NUM,:LOGIN_USER,:PROVIDER_SEQ_NUM,:LOCATION_SEQ_NUM)";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, providerLocation);
                return result;
            }
        }

        public async Task<int> UpdateProviderLocationAsync(ProviderLocation providerLocation)
        {
            var sql = "UPDATE NEW_PATIENT_PROVIDER_LOCATION SET PROVIDER_SEQ_NUM=:PROVIDER_SEQ_NUM, LOCATION_SEQ_NUM=:LOCATION_SEQ_NUM WHERE AUTH_ENTITY_SEQ_NUM = :AUTH_ENTITY_SEQ_NUM AND UPPER(LOGIN_USER)= :LOGIN_USER";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, providerLocation);
                return result;
            }
        }

        public async Task<int> CreateMultiProviderAsync(List<MultiProvider> multiProvider)
        {
            var sql = "INSERT INTO WEB_AUTH.MULTI_VIEW_PROVIDER_LIST (SEQ_NUM, PROVIDER_SEQ_NUM, AUTH_USER_SEQ_NUM, AUTH_ENTITY_SEQ_NUM) VALUES(PRIMARY_SEQ.NEXTVAL, :PROVIDER_SEQ_NUM, :AUTH_USER_SEQ_NUM,:AUTH_ENTITY_SEQ_NUM)";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, multiProvider);
                return result;
            }
        }

        public async Task<int> DeleteMultiProviderAsync(MultiProvider multiProvider)
        {
            var sql = "DELETE FROM WEB_AUTH.MULTI_VIEW_PROVIDER_LIST WHERE PROVIDER_SEQ_NUM = :PROVIDER_SEQ_NUM AND AUTH_USER_SEQ_NUM= :AUTH_USER_SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, multiProvider);
                return result;
            }
        }

        public async Task<int> CreateProviderAgentAsync(ProviderAgent providerAgent)
        {
            var sql = "INSERT INTO USER_PROVIDER_AGENT (SEQ_NUM,PROVIDER_SEQ_NUM, USER_AGENT_SEQ_NUM, AUTH_ENTITY_SEQ_NUM) VALUES(PRIMARY_SEQ.NEXTVAL, :PROVIDER_SEQ_NUM, :USER_AGENT_SEQ_NUM, :AUTH_ENTITY_SEQ_NUM)";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, providerAgent);
                return result;
            }
        }
        public async Task<IReadOnlyList<AdvanceOption>> GetAdvanceOptionsByIdAsync(long id)
        {
            var sql = "SELECT B.DESCRIPTION, B.SEQ_NUM FROM SYSTEM_ADV_OPTIONS_ASSIGNMENT A, SYSTEM_ADV_OPTIONS B WHERE A.ADVANCE_OPTIONS_SEQ_NUM = B.SEQ_NUM AND A.VISIBLE = 'Y' AND A.ENTITY_SEQ_NUM = :ENTITY_SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<AdvanceOption>(sql, new { ENTITY_SEQ_NUM = id });
                return result.ToList();
            }
        }

        public async Task<IReadOnlyList<Credentials>> GetEntityUsersByQueryAsync(string authEntityId, string active)
        {
            var sql = "SELECT USER_NAME, ENTITY_SEQ_NUM, USER_ACCESS_SEQ_NUM, EMAIL FROM USER_ACCESS WHERE  UPPER(ACTIVE_FLAG) = :ACTIVE_FLAG AND SEQ_NUM = :SEQ_NUM";

            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Credentials>(sql, new { ACTIVE_FLAG = active.ToUpper(), SEQ_NUM = authEntityId });
                return result.ToList();
            }

        }

        public async Task<int> UpdateAsync(long entityId, string paymentGateway, string accountType, string practiceSetup)
        {
            var sql = "UPDATE ENTITY SET PAYMENT_GATEWAY =:PAYMENT_GATEWAY WHERE SEQ_NUM=:SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                if (!string.IsNullOrEmpty(accountType))
                {
                    sql = "UPDATE ENTITY SET ACCOUNT_TYPE = :ACCOUNT_TYPE, TRIAL_START_DATE = NULL,TRIAL_END_DATE = NULL, NEXT_BILLING_DATE = (SELECT TRUNC (SYSDATE + 30) FROM DUAL) WHERE SEQ_NUM = :SEQ_NUM";
                    return await connection.ExecuteAsync(@sql, new { ACCOUNT_TYPE = accountType, SEQ_NUM = entityId });
                }
                else
                {
                    if (!string.IsNullOrEmpty(paymentGateway))
                    {
                        return await connection.ExecuteAsync(@sql, new { PAYMENT_GATEWAY = paymentGateway, SEQ_NUM = entityId });
                    }
                    else
                    {
                        sql = "UPDATE ENTITY SET PRACTICE_SETUP =:PRACTICE_SETUP, PRACTICE_SETUP_CHANGEDATE = SYSDATE WHERE SEQ_NUM =:SEQ_NUM";
                        return await connection.ExecuteAsync(@sql, new { PRACTICE_SETUP = practiceSetup, SEQ_NUM = entityId });
                    }
                }

            }
        }

        public async Task<int> DeleteProviderAgentAsync(long authEntityId, long providerId)
        {
            var sql = "DELETE FROM WEB_AUTH.USER_PROVIDER_AGENT WHERE PROVIDER_SEQ_NUM =:PROVIDER_SEQ_NUM AND AUTH_ENTITY_SEQ_NUM= :AUTH_ENTITY_SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, new { PROVIDER_SEQ_NUM = providerId, AUTH_ENTITY_SEQ_NUM = authEntityId });
                return result;
            }
        }
    }
}



