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

namespace Application.Features.Support.Tickets.Queries
{
    public class GetEntityTicketsByIdQuery : IRequest<Response<List<TicketViewModel>>>
    {
        public long PracticeId;
        public long EntityId;
        public GetEntityTicketsByIdQuery(long practiceId, long entityId)
        {
            PracticeId = practiceId;
            EntityId = entityId;
        }
    }
    public class GetEntityTicketsHandler : IRequestHandler<GetEntityTicketsByIdQuery, Response<List<TicketViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepositoryAsync _ticketRepo;
        public GetEntityTicketsHandler(IMapper map, ITicketRepositoryAsync repo)
        {
            _mapper = map;
            _ticketRepo = repo;
        }
        public async Task<Response<List<TicketViewModel>>> Handle(GetEntityTicketsByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _ticketRepo.GetPEHRDashboardTickets(request.PracticeId, request.EntityId);
            return new Response<List<TicketViewModel>>(_mapper.Map<List<TicketViewModel>>(response));
        }
    }
}
