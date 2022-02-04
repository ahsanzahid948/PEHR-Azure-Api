namespace Application.Features.Tickets
{
    using Application.DTOs.Ticket;
    using Application.Interfaces.Repositories;
    using Application.Wrappers;
    using AutoMapper;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    public class GetInboxDetailByIdQuery : IRequest<Response<List<SupportInboxDetailModel>>>
    {
        public InboxFilter Filter { get; set; }
        public long UserId { get; set; }
        public GetInboxDetailByIdQuery(long userid,InboxFilter _obj)
        {
            UserId = userid;
            Filter = _obj;
        }

    }
    public class GetInboxDetailHandler : IRequestHandler<GetInboxDetailByIdQuery, Response<List<SupportInboxDetailModel>>>
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepositoryAsync _ticketRepository;
        public GetInboxDetailHandler(IMapper map, ITicketRepositoryAsync repo)
        {
            _mapper = map;
            _ticketRepository = repo;
        }
        public async Task<Response<List<SupportInboxDetailModel>>> Handle(GetInboxDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _ticketRepository.GetInboxListAsync(request.UserId,request.Filter);
            return new Response<List<SupportInboxDetailModel>>(_mapper.Map<List<SupportInboxDetailModel>>(response));
        }
    }
}
