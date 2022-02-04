using Application.DTOs.ApiUser;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Application.Features.Support.ApiUser.Commands
{
    public class CreateApiUserCommand : IRequest<Response<string>>
    {
        public ApiUserViewModel Apiuser { get; set; }
    }

    public class CreateApiUserHandler : IRequestHandler<CreateApiUserCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly IApiUserRepositoryAsync _apiUser;
        public CreateApiUserHandler(IMapper map, IApiUserRepositoryAsync repo)
        {
            _mapper = map;
            _apiUser = repo;
        }
        public async Task<Response<string>> Handle(CreateApiUserCommand request, CancellationToken cancellationToken)
        {
            var response = await _apiUser.CreateApiUserAsync(_mapper.Map<Domain.Entities.Support.ApiUser>(request.Apiuser));

            if (response > 0)
            {
                return new Response<string>(data: "Created Successfully");
            }
            throw new Exception("Error Creating Api User");
        }
    }
}
