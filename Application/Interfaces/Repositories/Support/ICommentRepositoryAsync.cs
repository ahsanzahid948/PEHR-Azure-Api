using Application.DTOs.Common.CreateRequestResponse;
using Domain.Entities.Notes;
using Domain.Entities.Support.Ticket;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Support
{
    public interface ICommentRepositoryAsync : IGenericRepositoryAsync<SupportTicketComments>
    {
        Task<int> UpdateComment(SupportTicketComments supportTicketComment);
        Task<CreateRequestResponse> AddCommentsAttachment(string ticketNo, long commentSeqNo, string filePath, string fileName);
        Task<List<SupportTicketComments>> GetComments(long ticketNo);
        Task<CreateRequestResponse> CreateComment(SupportTicketComments supportTicketComment);
        Task<IReadOnlyList<SupportTicketAttachments>> GetCommentsAttachments(long ticketno, long commentSeqnum);
    }
}
