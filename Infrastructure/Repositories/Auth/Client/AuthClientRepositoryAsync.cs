using Application.Interfaces.Repositories.Auth;
using Dapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Auth.Client
{
    public class AuthClientRepositoryAsync : IAuthClientRepositoryAsync
    {
        private readonly IConfiguration configuration;
        public AuthClientRepositoryAsync(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Task<int> AddAsync(ClientProfile client)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<ClientProfile>> GetAllAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<ClientProfile> GetByFilterAsync(string filterCriteria)
        {
            throw new NotImplementedException();
        }

        public Task<ClientProfile> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateAsync(ClientProfile client)
        {
            var sql = "UPDATE CLIENT_PROF SET SHORT_NAME=:SHORT_NAME, NAME=:NAME, ADDRESS1=:ADDRESS1, ADDRESS2=:ADDRESS2, CITY=:CITY, STATE=:STATE, ZIPCODE=:ZIPCODE, ZIPCODE_EXT=:ZIPCODE_EXT, TEL_NUM=:TEL_NUM, EMAIL=:EMAIL, CONTACT_PERSON=:CONTACT_PERSON, PRIMARY_ADMIN=:PRIMARY_ADMIN, ACTIVE_FLAG=:ACTIVE_FLAG WHERE SEQ_NUM=:SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, client);
                return result;
            }
        }
    }
}
