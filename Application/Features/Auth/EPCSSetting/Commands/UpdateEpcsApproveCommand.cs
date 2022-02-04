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

namespace Application.Features.Auth.EPCSSetting.Commands
{
    public class UpdateEpcsApproveCommand : IRequest<Response<int>>
    {
        [Required]
        public virtual string UserId { get; set; }
        [Required]
        public virtual string Epcs2ndApproval { get; set; }
        public virtual string EpcsApproval { get; set; }

    }

    public class UpdateEpcsApproveCommandHandler : IRequestHandler<UpdateEpcsApproveCommand, Response<int>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;
        public UpdateEpcsApproveCommandHandler(IUserRepositoryAsync userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public async Task<Response<int>> Handle(UpdateEpcsApproveCommand request, CancellationToken cancellationToken)
        {
            List<long> userId = new List<long>();
            if(!string.IsNullOrEmpty(request.UserId))
                userId = request.UserId.Split(",").Select(long.Parse).ToList();
            var userStatus = await _userRepository.UpdateEPCSApproveAsync(userId, request.Epcs2ndApproval, request.EpcsApproval).ConfigureAwait(false);
            return new Response<int>(userStatus);
        }
    }
}
