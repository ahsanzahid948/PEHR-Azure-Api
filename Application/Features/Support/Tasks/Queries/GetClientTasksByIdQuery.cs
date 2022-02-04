using Application.DTOs.Task;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Support.Tasks.Queries
{
    public class GetClientTasksByIdQuery : IRequest<Response<List<TaskViewModel>>>
    {
        public long EntityId { get; set; }
        public string Status { get; set; }
        public GetClientTasksByIdQuery(long entityId, string status)
        {
            EntityId = entityId;
            Status = status;
        }
    }
    public class GetClientTasksHandler : IRequestHandler<GetClientTasksByIdQuery, Response<List<TaskViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepositoryAsync _taskRepositoryAsync;

        public GetClientTasksHandler(IMapper map, ITaskRepositoryAsync repo)
        {
            _mapper = map;
            _taskRepositoryAsync = repo;
        }
        public async Task<Response<List<TaskViewModel>>> Handle(GetClientTasksByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _taskRepositoryAsync.GetClientTasksAsync(request.EntityId, request.Status);
            return new Response<List<TaskViewModel>>(_mapper.Map<List<TaskViewModel>>(response));
        }
    }
}
