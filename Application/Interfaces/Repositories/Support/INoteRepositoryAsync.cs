using Application.DTOs;
using Application.DTOs.Common.CreateRequestResponse;
using Application.DTOs.Notes;
using Domain.Entities.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface INoteRepositoryAsync : IGenericRepositoryAsync<NotesViewModel>
    {
        Task<CreateRequestResponse> SaveNote(NotesViewModel _clientNotesViewModel);

        Task<NotesView_VM> TicketClientNotes(PayLoad payload, long ticketno, long commentSeq);

        Task<List<Notes>> GetAllNotes(NotesFilter notesFilter, long entitySeqNum, string ddlNoteType_ClientDetail);

        Task<List<SupportTicketAttachments>> GetNoteAttachments(long ticketno, long commentSeqnum);

        Task<int> UpdateNote(NotesViewModel _clientNote);

        Task<Notes> GetNotesByIdAsync(long id);
 
    }
}
