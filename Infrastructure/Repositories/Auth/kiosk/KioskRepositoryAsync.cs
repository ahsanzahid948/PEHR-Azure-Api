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
    public class KioskRepositoryAsync : IKioskRepositoryAsync 
    {

        private readonly IConfiguration configuration;
        public KioskRepositoryAsync(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<int> AddAsync(Kiosk kiosk)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> DeleteAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyList<Kiosk>> GetAllAsync(int pageNumber, int pageSize)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Kiosk> GetByFilterAsync(string filterCriteria)
        {
            var sql = "SELECT SEQ_NUM,EMAIL,PASSWORD,PORTAL_URL,ACTIVE,KIOSK_OPTIONS,ENTITY_SEQ_NUM,TEMPLATE_SEQ_NUM,STRIPE_PUBLIC_KEY,STRIPE_PRIVATE_KEY,KIOSK_APPT_SEARCH,KIOSK_URL FROM WEB_AUTH.KIOSK_ADMIN WHERE lower(email) = :email";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Kiosk>(sql, new { email = filterCriteria });
                return result;
            }
        }
        public async Task<Kiosk> GetByIdAsync(long id)
        {
            var sql = "SELECT * FROM WEB_AUTH.KIOSK_ADMIN WHERE ENTITY_SEQ_NUM=:Id";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Kiosk>(sql, new { Id = id });
                return result;
            }
        }
        public async Task<Kiosk> GetUserAsync(long id,string email)
        {
            var sql = "SELECT SEQ_NUM,EMAIL,PASSWORD,PORTAL_URL,ACTIVE,KIOSK_OPTIONS,ENTITY_SEQ_NUM,TEMPLATE_SEQ_NUM,STRIPE_PUBLIC_KEY,STRIPE_PRIVATE_KEY,KIOSK_APPT_SEARCH,KIOSK_URL FROM WEB_AUTH.KIOSK_ADMIN WHERE ENTITY_SEQ_NUM=:Id and lower(email) = :email";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Kiosk>(sql, new { Id = id, email = email });
                return result;
            }
        }

        public async Task<int> UpdateAsync(Kiosk kiosk)
        {
            throw new System.NotImplementedException();
        }
    }
}



