using Application.Wrappers;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Interfaces.Repositories;
using AutoMapper;
using Application.DTOs.Ticket;
using Application.DTOs;

namespace Application.Features.Notes.Queries
{
    public class GetAssignedTicketsByIdQuery : IRequest<Response<AssignTicketViewModel>>
    {
        public string entityseqnum;
        public PayLoad PayLoad;
        public string teamseqnum;
        public GetAssignedTicketsByIdQuery(PayLoad payload, string entity,  string teamseq)
        {
            entityseqnum = entity;
            PayLoad = payload;
            teamseqnum = teamseq;
        }
    }
    public class GetLoadAssigneDDLbyHandler : IRequestHandler<GetAssignedTicketsByIdQuery, Response<AssignTicketViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ISupportUserRepositoryAsync _userRepositoryAsync;
        public GetLoadAssigneDDLbyHandler(IMapper mapper, ISupportUserRepositoryAsync repo)
        {
            _mapper = mapper;
            _userRepositoryAsync = repo;
        }
        public async Task<Response<AssignTicketViewModel>> Handle(GetAssignedTicketsByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _userRepositoryAsync.SupportAssignedTicketsAsync(request.PayLoad,request.entityseqnum, request.teamseqnum);
            return new Response<AssignTicketViewModel>(_mapper.Map<AssignTicketViewModel>(response));
        }
    }
}
