using Application.DTOs;
using Application.DTOs.Entity;
using Application.DTOs.Ticket;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries
{
    public class GetTasksProgressByIdQuery : IRequest<Response<ProgressDataModel>>
    {
        public long EntityId { get; set; }
        public PayLoad Payload;

        public GetTasksProgressByIdQuery(PayLoad payload, long entityId)
        {
           
            EntityId = entityId;
            Payload = payload;
        }
    }
    public class GetTaskProgressHandler : IRequestHandler<GetTasksProgressByIdQuery, Response<ProgressDataModel>>
    {
        private readonly IMapper mapper;
        private readonly ITaskRepositoryAsync _taskRepository;
        public GetTaskProgressHandler(IMapper _mapper, ITaskRepositoryAsync userRepositoryAsync)
        {
            mapper = _mapper;
            _taskRepository = userRepositoryAsync;
        }
        public async Task<Response<ProgressDataModel>> Handle(GetTasksProgressByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _taskRepository.TicketsProgressData(request.Payload, request.EntityId);
            return new Response<ProgressDataModel>(mapper.Map<ProgressDataModel>(response));
        }
    }

}
