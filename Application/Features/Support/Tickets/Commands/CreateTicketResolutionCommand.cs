using Application.DTOs.Support.Ticket;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities.Support.Ticket;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Support.Tickets.Commands
{
    public class CreateTicketResolutionCommand : IRequest<Response<string>>
    {
        public TicketResolutionViewModel SupportResolution { get; set; }
    }
    public class CreateTicketResolutionHandler : IRequestHandler<CreateTicketResolutionCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepositoryAsync _ticketRepo;
        public CreateTicketResolutionHandler(IMapper map, ITicketRepositoryAsync repo)
        {
            _mapper = map;
            _ticketRepo = repo;
        }
        public async Task<Response<string>> Handle(CreateTicketResolutionCommand request, CancellationToken cancellationToken)
        {
            var response = await _ticketRepo.CreateTicketResolutionAsync(_mapper.Map<SupportTicketResolution>(request.SupportResolution));
            if (response > 0)
            {
                return new Response<string>(message: "Resolution Created Successfully");

            }
            return new Response<string>(message: "Error Creating Resolution");

        }
    }
}
