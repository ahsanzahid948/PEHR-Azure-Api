using Application.DTOs;
using Application.DTOs.Auth.Client;
using Domain.Entities;
using Domain.Entities.Support;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IClientRepositoryAsync : IGenericRepositoryAsync<ClientProfileViewModel>
    {
        Task<List<ClientsList>> GetClientsData(ClientFilter _client);
        Task<ClientList_VM> ClientsListAsync(string seqnum);
        Task<int> AddClientAsync(ClientProfileViewModel clientProfile);
        Task<ClientSubscription> GetClientSubscriptionById(long entityId);

    }
}
