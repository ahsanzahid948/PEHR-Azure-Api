using Application.DTOs.Auth.Client;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Clients.Queries
{
    public class GetAllClientsByQuery : IRequest<Response<List<ClientsListViewModel>>>
    {
        public ClientFilter clientFilter;
        public GetAllClientsByQuery(ClientFilter _clientFilter)
        {
            clientFilter = _clientFilter;
        }

    }
    public class GetAllClientsHandler : IRequestHandler<GetAllClientsByQuery, Response<List<ClientsListViewModel>>>
    {
        private readonly IClientRepositoryAsync _clientRepositoryAsync;
        private readonly IMapper _mapper;
        public GetAllClientsHandler(IClientRepositoryAsync repo, IMapper map)
        {
            _clientRepositoryAsync = repo;
            _mapper = map;
        }
        public async Task<Response<List<ClientsListViewModel>>> Handle(GetAllClientsByQuery request, CancellationToken cancellationToken)
        {
            var response = await _clientRepositoryAsync.GetClientsData(request.clientFilter);
            return new Response<List<ClientsListViewModel>>(_mapper.Map<List<ClientsListViewModel>>(response));

        }
    }
}
