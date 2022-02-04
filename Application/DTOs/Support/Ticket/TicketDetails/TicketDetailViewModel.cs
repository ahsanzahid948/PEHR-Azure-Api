using Application.DTOs.Support.Ticket;
using System.Collections.Generic;

namespace Application.DTOs.Support.ViewModels.TicketViewModel
{
    public class TicketDetailViewModel
    {
        public DTOs.Ticket.TicketViewModel ticketData { get; set; }
        public List<TicketHistoryViewModel> ticketHistory { get; set; }
    }
}
