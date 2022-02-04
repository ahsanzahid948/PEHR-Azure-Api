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

namespace Application.Features.Support.Tickets.Queries
{
    public class GetTicketResolutionByIdQuery : IRequest<Response<SupportTicketResolution>>
    {
        public long ResolutionId;
        public GetTicketResolutionByIdQuery(long resId)
        {
            ResolutionId = resId;
        }
    }
    public class GetTicketResolutionHandler : IRequestHandler<GetTicketResolutionByIdQuery, Response<SupportTicketResolution>>
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepositoryAsync _ticketRepo;

        public GetTicketResolutionHandler(IMapper map, ITicketRepositoryAsync repo)
        {
            _mapper = map;
            _ticketRepo = repo;
        }
        public async Task<Response<SupportTicketResolution>> Handle(GetTicketResolutionByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _ticketRepo.GetSupportTicketResolutionAsync(request.ResolutionId);
            return new Response<SupportTicketResolution>(_mapper.Map<SupportTicketResolution>(response));
        }
    }
}
