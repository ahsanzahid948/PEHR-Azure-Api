using Application.DTOs.Auth.Client;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Support.Users.Queries
{
    public class GetImplementationManagerByIDQuery : IRequest<Response<ClientsListViewModel>>
    {
        public long EntitySeqNum;
        public GetImplementationManagerByIDQuery(long entitySeqNum)
        {
            EntitySeqNum = entitySeqNum;
        }
    }
    public class GetImplementationManagerHandler : IRequestHandler<GetImplementationManagerByIDQuery, Response<ClientsListViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ISupportUserRepositoryAsync _userRepo;
        public GetImplementationManagerHandler(IMapper map, ISupportUserRepositoryAsync repo)
        {
            _mapper = map;
            _userRepo = repo;
        }
        public async Task<Response<ClientsListViewModel>> Handle(GetImplementationManagerByIDQuery request, CancellationToken cancellationToken)
        {
            var response = await _userRepo.GetImplementationManager(request.EntitySeqNum);
            return new Response<ClientsListViewModel>(_mapper.Map<ClientsListViewModel>(response));
        }
    }
}
