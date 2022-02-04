using Application.DTOs;
using Application.DTOs.Auth.Client;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries
{
    public class GetClientsListQuery : IRequest<Response<IReadOnlyList<ClientsListViewModel>>>
    {
        public string DdlSalesMgr { get; set; }
        public string DdlImplementationMgr { get; set; }
        public string DdlCustAccNums { get; set; }

        public PayLoad payload;
        public GetClientsListQuery(PayLoad obj, string ddSales, string ddImplement, string ddCust)
        {
            DdlSalesMgr = ddSales;
            DdlImplementationMgr = ddImplement;
            DdlCustAccNums = ddCust;
            payload = obj;

        }

    }
    public class GetClientListByQueryHandler : IRequestHandler<GetClientsListQuery, Response<IReadOnlyList<ClientsListViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly ISupportUserRepositoryAsync _userRepository;

        public GetClientListByQueryHandler(IMapper mapper, ISupportUserRepositoryAsync userRepository)
        {
            this._mapper = mapper;
            this._userRepository = userRepository;
        }
        public async Task<Response<IReadOnlyList<ClientsListViewModel>>> Handle(GetClientsListQuery request, CancellationToken cancellationToken)
        {
            var response = await _userRepository.GetClientListAsync(request.payload, request.DdlSalesMgr, request.DdlImplementationMgr, request.DdlCustAccNums);
            return new Response<IReadOnlyList<ClientsListViewModel>>(_mapper.Map<IReadOnlyList<ClientsListViewModel>>(response));
        }
    }
}
