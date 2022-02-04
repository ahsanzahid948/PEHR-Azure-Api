using Application.DTOs;
using Application.DTOs.Common.CreateRequestResponse;
using Application.DTOs.Support.Ticket;
using Application.DTOs.Support.ViewModels.TicketViewModel;
using Application.DTOs.Ticket;
using Domain.Entities;
using Domain.Entities.Notes;
using Domain.Entities.Support;
using Domain.Entities.Support.Ticket;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ITicketRepositoryAsync : IGenericRepositoryAsync<SupportTicket>
    {
        Task<IReadOnlyList<SupportTicket>> SupportTicketsListAsync(TicketFilter ticketfilter);
        Task<IReadOnlyList<SupportInbox>> GetInboxListAsync(long userId, InboxFilter inboxFilter);
        Task<List<SupportTicket>> SearchTicketsAsync(TicketFilter ticketFilter);
        Task<List<SupportTicket>> QuickSearchTicketsAsync(SearchFilter searchTicketFilter);
        Task<TicketStatusViewModel> SupportDashBoardStatus(SearchFilter searchTicketFilter);
        Task<List<SupportTicketChangeHistory>> GetTicketHistory(long ticketNo);
        Task<PracticeSetup> GetTicketAssignee(long practiceSeqNum);
        Task<CreateRequestResponse> SaveTicketAsync(SupportTicket supportTicket);
        Task<int> UpdateSupportTicket(SupportTicket ticket);
        Task<int> UpdateTicketResolutionAsync(SupportTicketResolution ticketResolution);
        Task<int> CreateTicketResolutionAsync(SupportTicketResolution ticketResolution);
        Task<SupportTicketResolution> GetSupportTicketResolutionAsync(long resolutionId);
        Task<List<SupportTicket>> GetPEHRDashboardTickets(long practiceSeqNum, long entitySeqNum);
        Task<int> InsertTicketHistory(string parentTicket, string type, string oldValue, string newValue, string appUser);
    }
}
