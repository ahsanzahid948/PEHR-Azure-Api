using Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IEntityRepositoryAsync : IGenericRepositoryAsync<Entity>
    {
        Task<Entity> GetByIdAsync(long id, string dbEntity);
        Task<EntityConfiguration> GetConfigurationsByIdAsync(long id);
        Task<IReadOnlyList<Credentials>> GetCredentialsAsync(long id, string email, string authEntityId);
        Task<IReadOnlyList<ProviderLocation>> GetProviderLocationAsync(long id, string email);
        Task<IReadOnlyList<MultiProvider>> GetMultiProviderAsync(long id, long userId);
        Task<IReadOnlyList<ProviderAgent>> GetProviderAgentsAsync(long id, long providerId);
        Task<IReadOnlyList<ProviderAgent>> GetUserAgentsAsync(long id, long userId);
        Task<int> CreateProviderLocationAsync(ProviderLocation providerLocation);
        Task<int> UpdateProviderLocationAsync(ProviderLocation providerLocation);
        Task<int> CreateMultiProviderAsync(List<MultiProvider> multiProvider);
        Task<int> DeleteMultiProviderAsync(MultiProvider multiProvider);
        Task<int> CreateProviderAgentAsync(ProviderAgent providerAgent);
        Task<IReadOnlyList<AdvanceOption>> GetAdvanceOptionsByIdAsync(long id);
        Task<IReadOnlyList<Credentials>> GetEntityUsersByQueryAsync(string authEntityId, string active);
        Task<int> UpdateAsync(long entityId, string paymentGateway, string accountType, string practiceSetup);
        Task<int> DeleteProviderAgentAsync(long authEntityId, long providerId);
    }
}
