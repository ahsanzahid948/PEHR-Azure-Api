using Application.Interfaces.Repositories.Support;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities.Support;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Support.SSOINFO.Command
{
    public class CreateCredentialCommand : IRequest<Response<int>>
    {
        public virtual string SSOUser { get; set; }
        public virtual string SSOPassword { get; set; }
        public virtual string Email { get; set; }
        public virtual string SSOType { get; set; }
        public virtual long? EntityId { get; set; }

    }
    public class CreateCredentialCommandHandler : IRequestHandler<CreateCredentialCommand, Response<int>>
    {
        private readonly ITreatWriteRepositoryAsync _treatWriteRepositoryAsync;
        private readonly IMapper _mapper;

        public CreateCredentialCommandHandler(ITreatWriteRepositoryAsync treatWriteRepositoryAsync, IMapper mapper)
        {
            _treatWriteRepositoryAsync = treatWriteRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateCredentialCommand request, CancellationToken cancellationToken)
        {
            var credential = _mapper.Map<SSOInfo>(request);
            var paymentMethodsList = await _treatWriteRepositoryAsync.CreateCredentialsAsync(credential).ConfigureAwait(false);
            return new Response<int>(_mapper.Map<int>(paymentMethodsList));
        }
    }
}
