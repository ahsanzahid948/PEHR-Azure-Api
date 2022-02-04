using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Auth.UserLog.Commands
{
    public class UpdateUserLogCommand : IRequest<Response<int>>
    {
        [Required]
        public long AuditSessionId { get; set; }
    }

    public class UpdateUserLogCommandHandler : IRequestHandler<UpdateUserLogCommand, Response<int>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;
        public UpdateUserLogCommandHandler(IUserRepositoryAsync userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateUserLogCommand request, CancellationToken cancellationToken)
        {
            var userStatus = await _userRepository.UpdateUserLogAsync(request.AuditSessionId).ConfigureAwait(false);
            return new Response<int>(userStatus);
        }
    }
}