using Domain.Entities.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Support
{
    public interface IEvvRepositoryAsync : IGenericRepositoryAsync<EvvStateTimeZone>
    {
        Task<IReadOnlyList<EvvStateTimeZone>> GetConfigurationsByStateAsync(string state);
        Task<IReadOnlyList<EvvTasks>> GetEvvTasksByStateAsync(string state);
    }
}
