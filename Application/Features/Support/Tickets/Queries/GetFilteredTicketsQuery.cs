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
    public class GetFilteredTicketsQuery : IRequest<Response<IReadOnlyList<TicketViewModel>>>
    {
        public TicketFilter TicketFilter { get; set; }

        public GetFilteredTicketsQuery(TicketFilter obj)
        {
            {
                this.TicketFilter = obj;
            }
        }
        public class GetFilteredTicketsHandler : IRequestHandler<GetFilteredTicketsQuery, Response<IReadOnlyList<TicketViewModel>>>
        {
            private readonly IMapper _mapper;
            private readonly ITicketRepositoryAsync _repositoryAsync;
            public GetFilteredTicketsHandler(IMapper mapper, ITicketRepositoryAsync repo)
            {
                _mapper = mapper;
                _repositoryAsync = repo;
            }
            public async Task<Response<IReadOnlyList<TicketViewModel>>> Handle(GetFilteredTicketsQuery request, CancellationToken cancellationToken)
            {
                var response = await _repositoryAsync.SupportTicketsListAsync(request.TicketFilter);
                return new Response<IReadOnlyList<TicketViewModel>>(_mapper.Map<IReadOnlyList<TicketViewModel>>(response));
            }
        }
    }
}
