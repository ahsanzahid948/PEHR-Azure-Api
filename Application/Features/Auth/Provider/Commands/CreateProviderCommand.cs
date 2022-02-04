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
    public class CreateProviderCommand : IRequest<Response<int>>
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string MiddleIntial { get; set; }
        public virtual string ProviderNPI { get; set; }
        public virtual string Speciality { get; set; }
        public virtual string TaxonomyCode { get; set; }
        public virtual string SSN { get; set; }
        public virtual string DEANo { get; set; }
        public virtual string Qualification { get; set; }
        public virtual string PhoneNo { get; set; }
        public virtual string FaxNo { get; set; }
        public virtual string Address { get; set; }
        public virtual string ProviderSignature { get; set; }
        public virtual long? EntittyId { get; set; }

    }

    public class CreateProviderCommandHandler : IRequestHandler<CreateProviderCommand, Response<int>>
    {
        private readonly IProviderRepositoryAsync _providerRepositry;
        private readonly IMapper _mapper;
        public CreateProviderCommandHandler(IProviderRepositoryAsync providerRepositry, IMapper mapper)
        {
            _providerRepositry = providerRepositry;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateProviderCommand request, CancellationToken cancellationToken)
        {
            var provider = _mapper.Map<Domain.Entities.Auth.Provider>(request);
            var userStatus = await _providerRepositry.AddAsync(provider).ConfigureAwait(false);
            return new Response<int>(userStatus);
        }
    }
}