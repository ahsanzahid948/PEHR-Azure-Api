using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Support.Users.Command
{
    public class CreateSupportUserCommand : IRequest<Response<string>>
    {
      public UserInfoViewModel supportUser { get; set; }
      public PayLoad payLoad { get; set; }
    }

    public class CreateSupportUserHandler : IRequestHandler<CreateSupportUserCommand, Response<string>>
    {
        private readonly ISupportUserRepositoryAsync _supportUserRepo;
        private readonly IMapper _mapper;
        public CreateSupportUserHandler(ISupportUserRepositoryAsync _supportUser,IMapper mapper)
        {
            _supportUserRepo = _supportUser;
            _mapper = mapper;

        }
        public async Task<Response<string>> Handle(CreateSupportUserCommand request, CancellationToken cancellationToken)
        {
            var response = await _supportUserRepo.CreateSupportUser(_mapper.Map<SupportUserInfo>(request.supportUser), request.payLoad);
            return new Response<string>(data:response);
        }
    }
}
