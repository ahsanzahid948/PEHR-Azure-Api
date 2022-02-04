using Application.DTOs.Common.CreateRequestResponse;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Tasks.Commands
{
    public class CreateTicketCommand : IRequest<Response<CreateRequestResponse>>
    {
        public SupportTicket SupportTicketObj { get; set; }
        public CreateTicketCommand(SupportTicket _obj)
        {
            SupportTicketObj = _obj;
        }
    }
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, Response<CreateRequestResponse>>
    {
        private readonly ITicketRepositoryAsync _ticketRepo;
        public CreateTicketCommandHandler(ITicketRepositoryAsync repo)
        {
            _ticketRepo = repo;
        }
        public async Task<Response<CreateRequestResponse>> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var response = await _ticketRepo.SaveTicketAsync(request.SupportTicketObj);
            return new Response<CreateRequestResponse>(data: response);
        }
    }
}
