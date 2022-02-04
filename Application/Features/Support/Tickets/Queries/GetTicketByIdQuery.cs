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
    public class GetTicketByIdQuery : IRequest<Response<TicketViewModel>>
    {
        public long TicketId { get; set; }
        public GetTicketByIdQuery(long ticketId)
        {
            TicketId = ticketId;
        }
    }
    public class GetTicketHandler : IRequestHandler<GetTicketByIdQuery, Response<TicketViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepositoryAsync _ticketRepo;
        public GetTicketHandler(IMapper map, ITicketRepositoryAsync repo)
        {
            _mapper = map;
            _ticketRepo = repo;
        }
        public async Task<Response<TicketViewModel>> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _ticketRepo.GetByIdAsync(request.TicketId);
            return new Response<TicketViewModel>(_mapper.Map<TicketViewModel>(response));
        }
    }
}
