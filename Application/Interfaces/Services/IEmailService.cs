using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IEmailService
    {
        public void SendTicketEmail(string ticketNumber, string emailType, string oldAssignee, string oldinternalassignee, string newinternalassignee,
            string oldStatus, string newStatus, string oldteamassignee, string currentteamassignee, string emailSubject, SupportTicket supportTicket, ref bool isSentToReporter);
        public void SentUserCreationEmail(string email, string token);
        public void SendServeyEmail(string ticketReporterEmail, string ticketReporter, string customerId, string TicketNo);

    }
}
