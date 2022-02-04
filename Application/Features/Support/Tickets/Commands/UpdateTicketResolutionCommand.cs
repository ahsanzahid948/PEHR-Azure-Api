using Application.DTOs.Support.Ticket;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities.Support.Ticket;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Support.Tickets.Commands
{
    public class UpdateTicketResolutionCommand : IRequest<Response<string>>
    {
        public TicketResolutionViewModel TicketResolution { get; set; }
    }
    public class UpdateTicketResolutionHandler : IRequestHandler<UpdateTicketResolutionCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepositoryAsync _ticketRepo;
        public UpdateTicketResolutionHandler(IMapper map, ITicketRepositoryAsync repo)
        {
            _mapper = map;
            _ticketRepo = repo;
        }
        public async Task<Response<string>> Handle(UpdateTicketResolutionCommand request, CancellationToken cancellationToken)
        {
            var TicketResolution = await _ticketRepo.GetSupportTicketResolutionAsync(request.TicketResolution.TicketNo);
            if (!string.IsNullOrEmpty(request.TicketResolution.Description) && request.TicketResolution.Description != TicketResolution.Description)
            {
                request.TicketResolution.Description = TicketResolution.Description;
            }
            if (!string.IsNullOrEmpty(request.TicketResolution.Comments) && request.TicketResolution.Comments != TicketResolution.Comments)
            {
                request.TicketResolution.Comments = TicketResolution.Comments;
            }
            var ticketResolution = await _ticketRepo.UpdateTicketResolutionAsync(_mapper.Map<SupportTicketResolution>(request.TicketResolution));
            if (ticketResolution > 0)
            {
                return new Response<string>("Updated Ticket Resolution");
            }

            throw new Exception("Error Updating Resolution");

        }
    }
}
