using Application.Interfaces.Repositories.Auth;
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

namespace Application.Features.Auth.Provider.Commands
{
    public class DeleteProviderCommand : IRequest<Response<int>>
    {
        [Required]
        public virtual long ProviderId { get; set; }

        public DeleteProviderCommand(long providerId)
        {
            this.ProviderId = providerId;
        }
    }

    public class DeleteProviderCommandHandler : IRequestHandler<DeleteProviderCommand, Response<int>>
    {
        private readonly IProviderRepositoryAsync _providerRepositry;
        private readonly IMapper _mapper;
        public DeleteProviderCommandHandler(IProviderRepositoryAsync providerRepositry, IMapper mapper)
        {
            _providerRepositry = providerRepositry;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(DeleteProviderCommand request, CancellationToken cancellationToken)
        {
            var userStatus = await _providerRepositry.DeleteAsync(request.ProviderId).ConfigureAwait(false);
            return new Response<int>(userStatus);
        }
    }
}