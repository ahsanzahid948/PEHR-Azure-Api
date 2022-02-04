using Application.Interfaces.Repositories.Support;
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

namespace Application.Features.Support.SSOINFO.Command
{
    public class UpdateCredentialCommand : IRequest<Response<int>>
    {
        public virtual string User { get; set; }
        public virtual string Password { get; set; }
        public virtual string Type { get; set; }
        [Required]
        public virtual long CredentialId { get; set; }

    }
    public class UpdateCredentialCommandHandler : IRequestHandler<UpdateCredentialCommand, Response<int>>
    {
        private readonly ITreatWriteRepositoryAsync _treatWriteRepositoryAsync;
        private readonly IMapper _mapper;

        public UpdateCredentialCommandHandler(ITreatWriteRepositoryAsync treatWriteRepositoryAsync, IMapper mapper)
        {
            _treatWriteRepositoryAsync = treatWriteRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateCredentialCommand request, CancellationToken cancellationToken)
        {
            var status = await _treatWriteRepositoryAsync.UpdateCredentialsAsync(request.CredentialId, request.User, request.Password, request.Type).ConfigureAwait(false);
            return new Response<int>(_mapper.Map<int>(status));
        }
    }
}
