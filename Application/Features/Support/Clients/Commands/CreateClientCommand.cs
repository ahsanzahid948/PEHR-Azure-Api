using Application.DTOs.Auth.Client;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Support.Clients.Commands
{
    public class CreateClientCommand : IRequest<Response<string>>
    {
        public ClientProfileViewModel ClientProfile { get; set; }

        public CreateClientCommand(ClientProfileViewModel obj)
        {
            ClientProfile = obj;
        }
    }
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Response<string>>
    {
        private readonly IClientRepositoryAsync _clientRepo;
        public CreateClientCommandHandler(IClientRepositoryAsync repo)
        {
            _clientRepo = repo;
        }

        public async Task<Response<string>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var response = await _clientRepo.AddClientAsync(request.ClientProfile);
            throw new NotImplementedException();
        }
    }
}
