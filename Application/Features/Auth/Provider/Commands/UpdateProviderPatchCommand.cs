using Application.Interfaces.Repositories.Auth;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Auth.Provider.Commands
{
    public class UpdateProviderPatchCommand : IRequest<Response<int>>
    {
        public virtual long ProviderId { get; set; }
        public virtual string ProviderSignature { get; set; }

        public UpdateProviderPatchCommand(long providerId, string providerSignature)
        {
            this.ProviderId = providerId;
            this.ProviderSignature = providerSignature;
        }

    }

    public class UpdateProviderPatchCommandHandler : IRequestHandler<UpdateProviderPatchCommand, Response<int>>
    {
        private readonly IProviderRepositoryAsync _providerRepositry;
        private readonly IMapper _mapper;
        public UpdateProviderPatchCommandHandler(IProviderRepositoryAsync providerRepositry, IMapper mapper)
        {
            _providerRepositry = providerRepositry;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateProviderPatchCommand request, CancellationToken cancellationToken)
        {
            var provider = await _providerRepositry.GetProviderDetailByIdAsync(request.ProviderId).ConfigureAwait(false);
            if (provider != null)
            {
                provider.Provider_Signature = request.ProviderSignature;
                var userStatus = await _providerRepositry.UpdatePatchAsync(provider).ConfigureAwait(false);
                return new Response<int>(userStatus);
            }
            else
                throw new Exception("Provider doesnot exist");
        }
    }
}