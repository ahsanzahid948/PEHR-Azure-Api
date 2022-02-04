namespace Application.Features.Auth
{
    using Application.DTOs.Auth.Client;
    using Application.DTOs.Entity;
    using Application.Interfaces.Repositories.Auth;
    using Application.Wrappers;
    using AutoMapper;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class UpdateClientDetailCommand : IRequest<Response<string>>
    {
        public ClientProfileViewModel clientProf;
        public EntityViewModel entityView;
        public UpdateClientDetailCommand(ClientProfileViewModel client, EntityViewModel eModel)
        {
            clientProf = client;
            entityView = eModel;
        }

    }
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientDetailCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly IPracticeRepositoryAsync _repoPractice;
        public UpdateClientCommandHandler(IMapper mapper, IPracticeRepositoryAsync practiceRepo)
        {
            _mapper = mapper;
            _repoPractice = practiceRepo;
        }
        public async Task<Response<string>> Handle(UpdateClientDetailCommand request, CancellationToken cancellationToken)
        {
            var response =await _repoPractice.UpdateClientEntityProfile(request.clientProf, request.entityView);
            return new Response<string>(data:response);
        }
    }
}
