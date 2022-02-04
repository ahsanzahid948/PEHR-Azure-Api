namespace Support.API.Controllers.V1
{
    using Application.DTOs.Support.Ticket;
    using Application.Features.Support.Tickets.Commands;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Support.Api.Controllers;
    using Application.Features.Support.Tickets;
    using Application.Features.Support;
    using Microsoft.AspNetCore.Authorization;

    /// <summary>
    /// Ticket Comments Controller.
    /// </summary>
    [Authorize]
    public class CommentsController : BaseApiController
    {
        /// <summary>
        /// Create Comment.
        /// </summary>
        /// <param name="supportTicket">Update Ticket Command.</param>
        /// <returns>Ticket Updated.</returns>
        [HttpPost]
        [Route("/v1/Comments")]
        public async Task<IActionResult> CreateTicketComment(TicketCommentViewModel supportTicket)
        {
            return this.Ok(await this.Mediator.Send(new CreateCommentCommand(supportTicket)));
        }

        /// <summary>
        /// Create Comment Attachment.
        /// </summary>
        /// <param name="command">CreateTicketCommentAttachmentModel.</param>
        /// <returns>Create Ticket Comment Attachment.</returns>
        [HttpPost]
        [Route("/v1/Comments/Attachments")]
        public async Task<IActionResult> CreateCommentAttachment(CreateCommentAttachmentCommand command)
        {
            return this.Ok(await this.Mediator.Send(command));
        }

        /// <summary>
        /// Update Comment.
        /// </summary>
        /// <param name="command">SupportTicket ViewModel.</param>
        /// <returns>Ticket Comment Updated.</returns>
        [HttpPut]
        [Route("/v1/Comments/")]
        public async Task<IActionResult> UpdateComment(UpdateCommentByIdCommand command)
        {
            return this.Ok(await this.Mediator.Send(command));
        }

        /// <summary>
        /// Get Comments.
        /// </summary>
        /// <param name="ticketId">Ticket SeqNum.</param>
        /// <returns>SupportTicketCommentsViewModelList.</returns>
        [HttpGet]
        [Route("/v1/Comments/{ticketId}")]
        public async Task<IActionResult> GetComments(long ticketId)
        {
            return this.Ok(await this.Mediator.Send(new GetCommentsByIdQuery(ticketId)));
        }

        /// <summary>
        /// Get Comments Attachments.
        /// </summary>
        /// <param name="ticketId">Ticket SeqNum.</param>
        /// <param name="commentId">Comment SeqNum.</param>
        /// <returns>SupportTicketCommentsAttachment List.</returns>
        [HttpGet]
        [Route("/v1/Comments/{ticketId}/{commentId}/Attachments")]
        public async Task<IActionResult> GetTicketCommentsAttachment(long ticketId, long commentId)
        {
            return this.Ok(await this.Mediator.Send(new GetCommentAttachmentByIdQuery(ticketId, commentId)));
        }

    }
}
