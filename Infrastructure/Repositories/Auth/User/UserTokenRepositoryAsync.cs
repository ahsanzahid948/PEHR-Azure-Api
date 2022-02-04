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
    public class UserTokenRepositoryAsync : IUserTokenRepositoryAsync
    {
        private readonly IConfiguration configuration;
        public UserTokenRepositoryAsync(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<int> AddAsync(UserToken token)
        {
            var sql = "Insert into app_user_tokens (seq_num,user_seq_num,token,created_date,expiry_date) VALUES (PRIMARY_SEQ.NEXTVAL, :user_seq_num, :token, :created_date, :expiry_date)";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, token);
                return result;
            }
        }

        public async Task<int> DeleteAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyList<UserToken>> GetAllAsync(int pageNumber, int pageSize)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserToken> GetByFilterAsync(string filterCriteria)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UserToken> GetByIdAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> UpdateAsync(UserToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}