using Application.DTOs.Support.Ticket;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Support;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Support.Tickets
{
    public class GetCommentsByIdQuery : IRequest<Response<List<TicketCommentViewModel>>>
    {
        public long TicketNo;
        public GetCommentsByIdQuery(long ticketNo)
        {
            TicketNo = ticketNo;
        }
    }
    public class GetCommentsHandler : IRequestHandler<GetCommentsByIdQuery, Response<List<TicketCommentViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepositoryAsync _commentRepo;
        public GetCommentsHandler(IMapper mapper, ICommentRepositoryAsync repo)
        {
            _mapper = mapper;
            _commentRepo = repo;
        }
        public async Task<Response<List<TicketCommentViewModel>>> Handle(GetCommentsByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _commentRepo.GetComments(request.TicketNo);
            return new Response<List<TicketCommentViewModel>>(_mapper.Map<List<TicketCommentViewModel>>(response));
        }
    }
}
