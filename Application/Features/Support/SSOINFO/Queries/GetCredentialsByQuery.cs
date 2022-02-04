using Application.DTOs.Support.SSOInfo;
using Application.Interfaces.Repositories.Support;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Support.SSOINFO.Queries
{
    public class GetCredentialsByQuery : IRequest<Response<SSOInfoViewModel>>
    {
        public long EntityId { get; set; }
        public string Type { get; set; }
        public string Email { get; set; }
        public GetCredentialsByQuery(long entityId, string type, string email)
        {
            this.EntityId = entityId;
            this.Type = type;
            this.Email = email;
        }
    }
    public class GetCredentialsByQueryHandler : IRequestHandler<GetCredentialsByQuery, Response<SSOInfoViewModel>>
    {
        private readonly ITreatWriteRepositoryAsync _treatWriteRepositoryAsync;
        private readonly IMapper _mapper;

        public GetCredentialsByQueryHandler(ITreatWriteRepositoryAsync treatWriteRepositoryAsync, IMapper mapper)
        {
            _treatWriteRepositoryAsync = treatWriteRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<SSOInfoViewModel>> Handle(GetCredentialsByQuery request, CancellationToken cancellationToken)
        {
            var credential = await _treatWriteRepositoryAsync.GetCredentialsByQueryAsync(request.EntityId, request.Type, request.Email).ConfigureAwait(false);
            return new Response<SSOInfoViewModel>(_mapper.Map<SSOInfoViewModel>(credential));
        }
    }
}
