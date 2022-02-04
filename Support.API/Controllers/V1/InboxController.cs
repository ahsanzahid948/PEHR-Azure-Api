namespace Authentication.API.Controllers.V1
{
    using Application.DTOs.Ticket;
    using Application.Features.Tickets;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Support.Api.Controllers;

    /// <summary>
    /// Inbox Controls.
    /// </summary>
    [ApiVersion("1.0")]
    [Authorize]
    public class InboxController : BaseApiController
    {
        /// <summary>
        /// Get All User Inbox Messages.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <param name="inboxFilter">Inbox Filter.</param>
        /// <returns>SupportInboxDetailModel.</returns>
        [HttpGet]
        [Route("/v1/Inbox/{userId}")]
        public IActionResult GetAllInbox(long userId, InboxFilter inboxFilter)
        {
            return this.Ok(this.Mediator.Send(new GetInboxDetailByIdQuery(userId,inboxFilter)));
        }
    }
}
