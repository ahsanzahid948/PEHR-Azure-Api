using Application.DTOs;
using Application.DTOs.Task;
using Application.Wrappers;
using Domain.Entities;
using Domain.Entities.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ITaskRepositoryAsync : IGenericRepositoryAsync<TaskViewModel>
    {
        Task<IReadOnlyList<SupportTask>> GetTasksAsync(TaskFilter obj);
        Task<List<SupportTask>> GetClientTasksAsync(long entityseqnum, string status);
        Task<TicketProgress_VM> TicketsProgressData(PayLoad payload, long entity);
    }
}
