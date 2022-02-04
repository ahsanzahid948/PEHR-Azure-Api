namespace Authentication.API.Controllers.V1
{
    using System.Threading.Tasks;
    using Application.DTOs;
    using Application.DTOs.Notes;
    using Application.Features.Clients.Queries;
    using Application.Features.Notes.Commands;
    using Application.Features.Support.Notes;
    using Application.Features.Support.Notes.Commands;
    using Application.Features.Support.Notes.Queries;
    using Application.Features.Support.Tickets.Commands;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Support.Api.Controllers;

    /// <summary>
    /// Notes Controls.
    /// </summary>
    [ApiVersion("1.0")]
    [Authorize]
    public class NotesController : BaseApiController
    {
        /// <summary>
        /// Get Ticket Note Details.
        /// </summary>
        /// <param name="payLoad">PayLoad.</param>
        /// <param name="id">Ticket Id.</param>
        /// <param name="commentId">Comment Id.</param>
        /// <returns>Returns Client Notes if Found.</returns>
        [HttpGet]
        [Route("/v1/Notes/{id}")]
        public async Task<IActionResult> GetClientNoteDetail([FromQuery] PayLoad payLoad, long id, long commentId)
        {
            return this.Ok(await this.Mediator.Send(new GetClientNoteDetailByIdQuery(payLoad, id, commentId)));
        }

        /// <summary>
        /// Save User Note.
        /// </summary>
        /// <param name="clientNotesViewModel">Client Notes.</param>
        /// <returns>Returns Successfull if Saved, else Error.</returns>
        [HttpPost]
        [Route("/v1/Notes")]
        public async Task<IActionResult> SaveNotes(NotesViewModel clientNotesViewModel)
        {
            return this.Ok(await this.Mediator.Send(new CreateNoteCommand(clientNotesViewModel)));
        }

        /// <summary>
        /// Save Note Attachment.
        /// </summary>
        /// <param name="noteAttachment">Note Attachment.</param>
        /// <returns>Returns Successfull if Saved, else Error.</returns>
        [HttpPost]
        [Route("/v1/Notes/Attachment")]
        public async Task<IActionResult> NoteAttachment(CreateCommentAttachmentCommand noteAttachment)
        {
            return this.Ok(await this.Mediator.Send(noteAttachment));
        }

        /// <summary>
        /// Update Note.
        /// </summary>
        /// <param name="noteId">Note Id.</param>
        /// <param name="noteViewModel">NoteViewModel.</param>
        /// <returns>Returns Successfull if Updated, else Error.</returns>
        [HttpPost]
        [Route("/v1/Notes/{noteId}")]
        public async Task<IActionResult> UpdateNote(long noteId, NotesViewModel noteViewModel)
        {
            return this.Ok(await this.Mediator.Send(new UpdateNoteByIdCommand(noteId, noteViewModel)));
        }


        /// <summary>
        /// Get Note Attachments.
        /// </summary>
        /// <param name="ticketId">Note Id.</param>
        /// <param name="commentId">Comment Id.</param>
        /// <returns>Note Attachments.</returns>
        [HttpGet]
        [Route("/v1/Notes/{ticketId}/Attachments")]
        public async Task<IActionResult> GetNoteAttachments(long ticketId, long commentId)
        {
            return this.Ok(await this.Mediator.Send(new GetNoteAttachmentsByIdQuery(ticketId, commentId)));
        }

        /// <summary>
        /// Get Client Notes List.
        /// </summary>
        /// <param name="payLoad">PayLoad.</param>
        /// <param name="entityId">Entity Id.</param>
        /// <param name="clientDetailNote">Note Search Detail.</param>
        /// <returns>Returns Client Notes List.</returns>
        [HttpGet]
        [Route("/v1/Notes/{entityId}")]
        public async Task<IActionResult> GetClientNoteList([FromQuery] NotesFilter payLoad, long entityId, string clientDetailNote)
        {
            return this.Ok(await this.Mediator.Send(new GetClientNotesDataByIdQuery(payLoad, entityId, clientDetailNote)));
        }
    }
}
