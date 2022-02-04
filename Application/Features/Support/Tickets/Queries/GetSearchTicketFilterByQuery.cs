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

namespace Application.Features.Users.Queries
{
    public class GetSearchTicketFilterByQuery : IRequest<Response<List<TicketViewModel>>>
    {
        public TicketFilter FilterTicket;

        public GetSearchTicketFilterByQuery(TicketFilter ticketFilter)
        {
            FilterTicket = ticketFilter;

        }
    }
    public class GetSearchTicketHandler : IRequestHandler<GetSearchTicketFilterByQuery, Response<List<TicketViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepositoryAsync _ticketRepo;
        public GetSearchTicketHandler(IMapper mapper, ITicketRepositoryAsync repository)
        {
            _mapper = mapper;
            _ticketRepo = repository;
        }
        public async Task<Response<List<TicketViewModel>>> Handle(GetSearchTicketFilterByQuery request, CancellationToken cancellationToken)
        {
            var response = await _ticketRepo.SearchTicketsAsync(request.FilterTicket);
            return new Response<List<TicketViewModel>>(_mapper.Map<List<TicketViewModel>>(response));

        }
    }
}
