using Application.DTOs.Support.Ticket;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Support.Tickets.Queries
{
    public class GetTicketHistoryByIdQuery : IRequest<Response<List<TicketHistoryViewModel>>>
    {
        public long TicketId { get; set; }
        public GetTicketHistoryByIdQuery(long ticketNo)
        {
            TicketId = ticketNo;
        }
    }
    public class GetTicketHistoryHandler : IRequestHandler<GetTicketHistoryByIdQuery, Response<List<TicketHistoryViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepositoryAsync _ticketRepo;
        public GetTicketHistoryHandler(IMapper map,ITicketRepositoryAsync ticketRepo)
        {
            _mapper = map;
            _ticketRepo = ticketRepo;
        }
        public async Task<Response<List<TicketHistoryViewModel>>> Handle(GetTicketHistoryByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _ticketRepo.GetTicketHistory(request.TicketId);
            return new Response<List<TicketHistoryViewModel>>(_mapper.Map<List<TicketHistoryViewModel>>(response));

        }
    }
}
