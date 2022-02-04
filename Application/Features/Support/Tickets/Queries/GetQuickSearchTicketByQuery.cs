using Application.DTOs.Ticket;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Tickets
{
    public class GetQuickSearchTicketByQuery : IRequest<Response<List<TicketViewModel>>>
    {
        public SearchFilter FilterSearch;
        public GetQuickSearchTicketByQuery(SearchFilter _obj)
        {
            FilterSearch = _obj;
        }

    }
    public class GetQuickSearchTicketHandler : IRequestHandler<GetQuickSearchTicketByQuery, Response<List<TicketViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepositoryAsync _ticketRepository;
        public GetQuickSearchTicketHandler(IMapper mapper, ITicketRepositoryAsync repo)
        {
            _mapper = mapper;
            _ticketRepository = repo;
        }
        public async Task<Response<List<TicketViewModel>>> Handle(GetQuickSearchTicketByQuery request, CancellationToken cancellationToken)
        {
            var response = await _ticketRepository.QuickSearchTicketsAsync(request.FilterSearch);
            return new Response<List<TicketViewModel>>(_mapper.Map<List<TicketViewModel>>(response));

        }
    }
}
