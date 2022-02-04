using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Support.LoginLog.Commands
{
    public class UpdateLoginLogCommand : IRequest<Response<int>>
    {
        [Required]
        public long LoginLogId { get; set; }
    }

    public class UpdateLoginLogCommandHandler : IRequestHandler<UpdateLoginLogCommand, Response<int>>
    {
        private readonly ISupportUserRepositoryAsync _supportUserRepository;
        private readonly IMapper _mapper;
        public UpdateLoginLogCommandHandler(ISupportUserRepositoryAsync supportUserRepository, IMapper mapper)
        {
            _supportUserRepository = supportUserRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(UpdateLoginLogCommand request, CancellationToken cancellationToken)
        {
            var userStatus = await _supportUserRepository.UpdateLoginLogAsync(request.LoginLogId).ConfigureAwait(false);
            return new Response<int>(userStatus);
        }
    }
}
