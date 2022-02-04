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

namespace Application.Features.Auth.InternalUser.Queries
{
    public class GetInternalUsersByIdQuery : IRequest<Response<IReadOnlyList<string>>>
    {
        public long EntityId { get; set; }
        public bool PracticeRcm { get; set; }
        public string Active { get; set; }
        public string RcmType { get; set; }
        public string RcmTypeTwo { get; set; }
        public GetInternalUsersByIdQuery(long entityId, bool practiceRcm, string active, string rcmType, string rcmTypeTwo)
        {
            this.EntityId = entityId;
            this.PracticeRcm = practiceRcm;
            this.Active = active;
            this.RcmType = rcmType;
            this.RcmTypeTwo = rcmTypeTwo;
        }
    }

    public class GetInternalUsersByIdQueryHandler : IRequestHandler<GetInternalUsersByIdQuery, Response<IReadOnlyList<string>>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;
        public GetInternalUsersByIdQueryHandler(IUserRepositoryAsync userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<Response<IReadOnlyList<string>>> Handle(GetInternalUsersByIdQuery request, CancellationToken cancellationToken)
        {
            var userList = await _userRepository.GetInternalUsersAsync(request.EntityId, request.PracticeRcm, request.Active, request.RcmType, request.RcmTypeTwo).ConfigureAwait(false);
            return new Response<IReadOnlyList<string>>(userList.Select(p=>p.Email).ToList());
        }
    }
}
