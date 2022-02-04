using Application.DTOs.Common.CreateRequestResponse;
using Application.DTOs.Support.Ticket;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Support;
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
    public class CreateCommentCommand : IRequest<Response<CreateRequestResponse>>
    {
        public TicketCommentViewModel supportTicketComments;
        public CreateCommentCommand(TicketCommentViewModel supportComment)
        {
            supportTicketComments = supportComment;
        }
    }
    public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, Response<CreateRequestResponse>>
    {
        private readonly ICommentRepositoryAsync _commentRepo;
        private readonly IMapper _mapper;
        public CreateCommentHandler(IMapper map, ICommentRepositoryAsync repo)
        {
            _mapper = map;
            _commentRepo = repo;
        }
        public async Task<Response<CreateRequestResponse>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var response = await _commentRepo.CreateComment(_mapper.Map<SupportTicketComments>(request.supportTicketComments));
            return new Response<CreateRequestResponse>(response);
        }
    }
}
