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

namespace Application.Features.Auth.Client.Commands
{
    public class UpdateClientCommand : IRequest<Response<int>>
    {
        public virtual long ClientId { get; set; }
        public virtual string ShortName { get; set; }
        public virtual string Name { get; set; }
        public virtual string AddressLineOne { get; set; }
        public virtual string AddressLineTwo { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Zipcode { get; set; }
        public virtual string ZipcodeExt { get; set; }
        public virtual long? TelephoneNum { get; set; }
        public virtual string Email { get; set; }
        public virtual string ContactPerson { get; set; }
        public virtual string PrimaryAdmin { get; set; }
        public virtual string Active { get; set; }
    }

    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Response<int>>
    {
        private readonly IAuthClientRepositoryAsync _clientRepositoryAsync;
        private readonly IMapper _mapper;
        public UpdateClientCommandHandler(IAuthClientRepositoryAsync entityRepository, IMapper mapper)
        {
            _clientRepositoryAsync = entityRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = _mapper.Map<Domain.Entities.ClientProfile>(request);
            var userStatus = await _clientRepositoryAsync.UpdateAsync(client).ConfigureAwait(false);
            return new Response<int>(userStatus);
        }
    }
}
