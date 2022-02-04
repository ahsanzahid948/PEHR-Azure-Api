using Application.DTOs.Support.Ticket;
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

namespace Application.Features.Support.Tickets.Commands
{
    public class CreateTicketHistoryCommand : IRequest<Response<int>>
    {
        public string TicketNo { get; set; }
        public string Type { get; set; }
        public string OldValue { get; set; }
        public string EnteredBy { get; set; }
        public string newValue { get; set; }
    }
    public class CreateTicketHistoryHandler : IRequestHandler<CreateTicketHistoryCommand, Response<int>>
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepositoryAsync _ticketRepo;
        public CreateTicketHistoryHandler(IMapper map, ITicketRepositoryAsync repo)
        {
            _mapper = map;
            _ticketRepo = repo;
        }
        public async Task<Response<int>> Handle(CreateTicketHistoryCommand request, CancellationToken cancellationToken)
        {
            var response = await _ticketRepo.InsertTicketHistory(request.TicketNo, request.Type, request.OldValue, request.newValue, request.EnteredBy);
            return new Response<int>(response);

        }
    }
}
