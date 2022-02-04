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
    public class UpdateCommentByIdCommand : IRequest<Response<string>>
    {
        public TicketCommentViewModel supportTicketComment { get; set; }
       }
    public class UpdateCommentHandler : IRequestHandler<UpdateCommentByIdCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepositoryAsync _commentRepo;
        public UpdateCommentHandler(IMapper mapper, ICommentRepositoryAsync repo)
        {
            _mapper = mapper;
            _commentRepo = repo;
        }
        public async Task<Response<string>> Handle(UpdateCommentByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _commentRepo.UpdateComment(_mapper.Map<SupportTicketComments>(request.supportTicketComment));
            if (response > 0)
            {
                return new Response<string>(message: "Ticket Comment Updated");
            }
            return new Response<string>("Failed");
        }
    }
}
