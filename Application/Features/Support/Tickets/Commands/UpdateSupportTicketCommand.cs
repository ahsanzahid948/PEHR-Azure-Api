using Application.DTOs.Support.Ticket;
using Application.DTOs.Ticket;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Support.Ticket;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Support.Tickets.Commands
{
    public class UpdateSupportTicketCommand : IRequest<Response<int>>
    {
        public long ticketId { get; set; }
        public string SuspendedFlag { get; set; }
        public DateTime TickleDate { get; set; }
        public string Priority { get; set; }
        public string LastUpdatedBy { get; set; }
        public string Status { get; set; }

    }
    public class UpdateSupportTicketHandler : IRequestHandler<UpdateSupportTicketCommand, Response<int>>
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepositoryAsync _ticketRepositoryAsync;

        public UpdateSupportTicketHandler(IMapper map, ITicketRepositoryAsync _repo)
        {
            _mapper = map;
            _ticketRepositoryAsync = _repo;
        }
        public async Task<Response<int>> Handle(UpdateSupportTicketCommand request, CancellationToken cancellationToken)
        {
            var supportTicket = await _ticketRepositoryAsync.GetByIdAsync(request.ticketId);
            if (!string.IsNullOrEmpty(request.SuspendedFlag))
            {
                supportTicket.Suspended_Flag = request.SuspendedFlag;
            }
            if (supportTicket.Tickle_Date != request.TickleDate)
            {
                supportTicket.Tickle_Date = request.TickleDate;
            }
            if (!string.IsNullOrEmpty(request.Priority))
            {
                supportTicket.Priority = request.Priority;
            }
            if (!string.IsNullOrEmpty(request.LastUpdatedBy))
            {
                supportTicket.Last_Update_By = request.LastUpdatedBy;
            }
            if (!string.IsNullOrEmpty(request.Status))
            {
                supportTicket.Status = request.Status;
            }
            var response = await _ticketRepositoryAsync.UpdateSupportTicket(supportTicket);
            return new Response<int>(message: response.ToString());
        }
    }
}
