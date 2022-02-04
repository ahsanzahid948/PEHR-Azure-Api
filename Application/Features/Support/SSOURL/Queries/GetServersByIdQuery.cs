using Application.DTOs.Support.SSOURL;
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

namespace Application.Features.Support.SSOURL.Queries
{
    public class GetServersByIdQuery : IRequest<Response<IReadOnlyList<SSOURLViewModel>>>
    {
        public long EntityId { get; set; }
        public string Type { get; set; }
        public GetServersByIdQuery(long entityId, string type)
        {
            this.EntityId = entityId;
            this.Type = type;
        }
    }
    public class GetServersByIdHandler : IRequestHandler<GetServersByIdQuery, Response<IReadOnlyList<SSOURLViewModel>>>
    {
        private readonly ITreatWriteRepositoryAsync _treatWriteRepositoryAsync;
        private readonly IMapper _mapper;

        public GetServersByIdHandler(ITreatWriteRepositoryAsync treatWriteRepositoryAsync, IMapper mapper)
        {
            _treatWriteRepositoryAsync = treatWriteRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<IReadOnlyList<SSOURLViewModel>>> Handle(GetServersByIdQuery request, CancellationToken cancellationToken)
        {
            var serversList = await _treatWriteRepositoryAsync.GetServersByIdAsync(request.EntityId, request.Type).ConfigureAwait(false);
            return new Response<IReadOnlyList<SSOURLViewModel>>(_mapper.Map< IReadOnlyList<SSOURLViewModel>>(serversList));
        }
    }
}

