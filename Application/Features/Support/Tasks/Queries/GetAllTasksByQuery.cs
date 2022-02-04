namespace Application.Features.Tasks.Queries
{
    using Application.DTOs.Task;
    using Application.Interfaces.Repositories;
    using Application.Wrappers;
    using AutoMapper;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetAllTasksByQuery : IRequest<Response<IReadOnlyList<TaskViewModel>>>
    {
        public TaskFilter obj;
        public GetAllTasksByQuery(TaskFilter _obj)
        {
            obj = _obj;
        }
    }
    public class GetTaskHandler : IRequestHandler<GetAllTasksByQuery, Response<IReadOnlyList<TaskViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepositoryAsync _taskRepo;
        public GetTaskHandler(IMapper _map, ITaskRepositoryAsync _repo)
        {
            _mapper = _map;
            _taskRepo = _repo;
        }
        public async Task<Response<IReadOnlyList<TaskViewModel>>> Handle(GetAllTasksByQuery request, CancellationToken cancellationToken)
        {
            var response = await _taskRepo.GetTasksAsync(request.obj);
            return new Response<IReadOnlyList<TaskViewModel>>(_mapper.Map<IReadOnlyList<TaskViewModel>>(response));
        }
    }
}
