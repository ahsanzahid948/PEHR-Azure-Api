using Domain.Entities.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IKioskRepositoryAsync : IGenericRepositoryAsync<Kiosk>
    {
        Task<Kiosk> GetUserAsync(long id, string email);
    }
}
