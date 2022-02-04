using AutoMapper;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Wrappers;
using Domain.Entities.Auth;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Features.Users.Commands
{
    public partial class CreateUserCommand : IRequest<Response<int>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string EntityId { get; set; }
        public string Active { get; set; }
        public virtual string Admin { get; set; }
        public virtual long? MenuRoleId { get; set; }
        public virtual long? EmergencyRoleId { get; set; }
        public virtual string AllowDelete { get; set; }
        public virtual long? ProviderId { get; set; }
        public virtual string Epcs { get; set; }
        public virtual string ApproveEpcs { get; set; }
        public virtual string MiddleInitial { get; set; }
        public virtual string ShowSetupProfiles { get; set; }
        public virtual string Token { get; set; }

    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<int>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordService _PasswordService;
        
        public CreateUserCommandHandler(IUserRepositoryAsync userRepository, IMapper mapper, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _PasswordService = passwordService;
            
        }
        public async Task<Response<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            //string passwordHash, passwordSalt;
            //_PasswordService.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            //user.Password_Hash = passwordHash;
            //user.Password_Salt = passwordSalt;

            var userStatus = await _userRepository.AddAsync(user).ConfigureAwait(false);
            return new Response<int>(userStatus);
        }
    }

}