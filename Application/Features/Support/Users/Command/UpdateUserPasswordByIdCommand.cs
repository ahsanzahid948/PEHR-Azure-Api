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

namespace Application.Features.Support.Users.Command
{
    public class UpdateUserPasswordByIdCommand : IRequest<Response<int>>
    {
        public string Password { get; set; }
        public long UserId { get; set; }
        public UpdateUserPasswordByIdCommand(long userId, string pass)
        {
            UserId = userId;
            Password = pass;
        }
    }
    public class UpdateUserPasswordHandler : IRequestHandler<UpdateUserPasswordByIdCommand, Response<int>>
    {
        private readonly IMapper _mapper;
        private readonly ISupportUserRepositoryAsync _supportUserRepo;

        public UpdateUserPasswordHandler(IMapper map, ISupportUserRepositoryAsync _repo)
        {
            _mapper = map;
            _supportUserRepo = _repo;
        }

        public async Task<Response<int>> Handle(UpdateUserPasswordByIdCommand request, CancellationToken cancellationToken)
        {
            var getUser = await _supportUserRepo.GetUserInfo(request.UserId);
            if (getUser != null)
            {
                var response = await _supportUserRepo.UpdateUserPassword(request.UserId, request.Password);
                return new Response<int>(response);
            }

            throw new Exception("Error Updating Password");
        }
    }
}
