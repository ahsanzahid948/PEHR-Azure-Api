using Application.DTOs;
using Application.DTOs.Auth.ViewModel;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Clients.Queries
{
    public class GetClientDetailByQuery : IRequest<Response<ClientDetailModel>>
    {
        public string ClientId;
        public GetClientDetailByQuery(string seqnum)
        {
            ClientId = seqnum;
        }
    }
    public class GetClientDetailHandler : IRequestHandler<GetClientDetailByQuery, Response<ClientDetailModel>>
    {
        private readonly IMapper _mapper;
        private readonly IClientRepositoryAsync _clientRepo;
        public GetClientDetailHandler(IMapper map, IClientRepositoryAsync repo)
        {
            _mapper = map;
            _clientRepo = repo;
        }
        public async Task<Response<ClientDetailModel>> Handle(GetClientDetailByQuery request, CancellationToken cancellationToken)
        {
            var response = await _clientRepo.ClientsListAsync(request.ClientId);
            return new Response<ClientDetailModel>(_mapper.Map<ClientDetailModel>(response));

        }
    }
}
