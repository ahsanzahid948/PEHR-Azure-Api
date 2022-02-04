namespace Authentication.API.Controllers.V1
{
    using System.Threading.Tasks;
    using Application.DTOs;
    using Application.DTOs.Ticket;
    using Application.Features.Notes.Queries;
    using Application.Features.Support.Tickets.Commands;
    using Application.Features.Support.Tickets.Queries;
    using Application.Features.Tasks.Commands;
    using Application.Features.Tickets;
    using Application.Features.Users.Queries;
    using Domain.Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Support.Api.Controllers;

    /// <summary>
    /// Ticket Detail Summary.
    /// </summary>
    [ApiVersion("1.0")]
    [Authorize]
    public class TicketController : BaseApiController
    {
        /// <summary>
        /// Get All Tickets.
        /// </summary>
        /// <param name="obj">Ticket Filter.</param>
        /// <returns>SupportTicketViewModel.</returns>
        [HttpGet]
        [Route("/v1/Tickets")]
        public async Task<IActionResult> GetAllTickets([FromQuery] TicketFilter obj)
        {
            return this.Ok(await this.Mediator.Send(new GetFilteredTicketsQuery(obj)));
        }

        /// <summary>
        /// Get Ticket Assignee.
        /// </summary>
        /// <param name="practiceSetupId">PracticeId.</param>
        /// <returns>PracticeSetup.</returns>
        [HttpGet]
        [Route("/v1/Tickets/{practiceSetupId}/Assigned")]
        public async Task<IActionResult> GetTicketAssign(long practiceSetupId)
        {
            return this.Ok(await this.Mediator.Send(new GetTicketAssigneeByQuery(practiceSetupId)));
        }

        /// <summary>
        /// Get User Assigned Tickets.
        /// </summary>
        /// <param name="payload">Ticket Filter.</param>
        /// <param name="entityId">Entity Seq Num.</param>
        /// <param name="teamId">Team Seq Num.</param>
        /// <returns>SupportAssignTicketViewModel.</returns>
        [HttpGet]
        [Route("/v1/Tickets/{entityId}/{teamId}/Assigned")]
        public async Task<IActionResult> GetAssignedTicket([FromQuery] PayLoad payload, string entityId, string teamId)
        {
            return this.Ok(await this.Mediator.Send(new GetAssignedTicketsByIdQuery(payload, entityId, teamId)));
        }

        /// <summary>
        /// Get Ticket History.
        /// </summary>
        /// <param name="ticketId">Ticket SeqNum .</param>
        /// <returns>SupportTicketHistoryViewModelList.</returns>
        [HttpGet]
        [Route("/v1/Tickets/{ticketId}/History")]
        public async Task<IActionResult> GetTicketHistory(long ticketId)
        {
            return this.Ok(await this.Mediator.Send(new GetTicketHistoryByIdQuery(ticketId)));
        }

        /// <summary>
        /// Get Searched Tickets.
        /// </summary>
        /// <param name="filter">Ticket Filter.</param>
        /// <returns>SupportAssignTicketViewModel.</returns>
        [HttpGet]
        [Route("/v1/Tickets/Search")]
        public async Task<IActionResult> SearchTickets([FromQuery] TicketFilter filter)
        {
            return this.Ok(await this.Mediator.Send(new GetSearchTicketFilterByQuery(filter)));
        }

        /// <summary>
        /// Get QuickSearch Tickets.
        /// </summary>
        /// <param name="filter">Search Filter.</param>
        /// <returns>SupportTicketViewModel.</returns>
        [HttpGet]
        [Route("/v1/Tickets/QuickSearch")]
        public async Task<IActionResult> QuickSearchTicket([FromQuery] SearchFilter filter)
        {
            return this.Ok(await this.Mediator.Send(new GetQuickSearchTicketByQuery(filter)));
        }

        /// <summary>
        /// Get Ticket.
        /// </summary>
        /// <param name="ticketId">Ticket Number.</param>
        /// <returns>SupportTicketViewModel.</returns>
        [HttpGet]
        [Route("/v1/Tickets/{ticketId}")]
        public async Task<IActionResult> GetTicket(long ticketId)
        {
            return this.Ok(await this.Mediator.Send(new GetTicketByIdQuery(ticketId)));
        }

        /// <summary>
        /// Get Dashboard Ticket.
        /// </summary>
        /// <param name="practiceId">PracticeSetupSeqNumber.</param>
        /// <param name="entityId">EntitySeqNum.</param>
        /// <returns>SupportTicketViewModel.</returns>
        [HttpGet]
        [Route("/v1/Tickets/{practiceId}/{entityId}")]
        public async Task<IActionResult> GetPEHRTickets(long practiceId, long entityId)
        {
            return this.Ok(await this.Mediator.Send(new GetEntityTicketsByIdQuery(practiceId, entityId)));
        }

        /// <summary>
        /// Save Ticket.
        /// </summary>
        /// <param name="supportTicket">SupportTicket Filter.</param>
        /// <returns>String.</returns>
        [HttpPost]
        [Route("/v1/Tickets")]
        public async Task<IActionResult> SaveTicket(SupportTicket supportTicket)
        {
            return this.Ok(await this.Mediator.Send(new CreateTicketCommand(supportTicket)));
        }

        /// <summary>
        /// Update Ticket.
        /// </summary>
        /// <param name="command">Update Ticket Command.</param>
        /// <returns>Ticket Updated.</returns>
        [HttpPut]
        [Route("/v1/Ticket/{ticketId}")]
        public async Task<IActionResult> UpdateTicket(UpdateSupportTicketCommand command)
        {
            return this.Ok(await this.Mediator.Send(command));
        }

        /// <summary>
        /// Create Ticket Logs.
        /// </summary>
        /// <param name="command">CreateTicketHistoryByCommand.</param>
        /// <returns>Inserted.</returns>
        [HttpPost]
        [Route("/v1/Tickets/History")]
        public async Task<IActionResult> CreateTicketChangeLog(CreateTicketHistoryCommand command)
        {
            return this.Ok(await this.Mediator.Send(command));
        }

        /// <summary>
        /// Get Ticket Resolution.
        /// </summary>
        /// <param name="ticketId">TicketId.</param>
        /// <returns>Ticket Resolution.</returns>
        [HttpGet]
        [Route("/v1/Tickets/Resolution")]
        public async Task<IActionResult> GetTicketResolution(long ticketId)
        {
            return this.Ok(await this.Mediator.Send(new GetTicketResolutionByIdQuery(ticketId)));
        }

        /// <summary>
        /// Update Ticket Resolution.
        /// </summary>
        /// <param name="command">UpdateTicketResolutionCommand.</param>
        /// <returns>Updated Ticket Resolution.</returns>
        [HttpPut]
        [Route("/v1/Tickets/Resolution")]
        public async Task<IActionResult> UpdateTicketResolution(UpdateTicketResolutionCommand command)
        {
            return this.Ok(await this.Mediator.Send(command));
        }

        /// <summary>
        /// Create Ticket Resolution.
        /// </summary>
        /// <param name="command">CreateTicketResolutionCommand.</param>
        /// <returns>Created.</returns>
        [HttpPost]
        [Route("/v1/Tickets/Resolution")]
        public async Task<IActionResult> CreateTicketResolution(CreateTicketResolutionCommand command)
        {
            return this.Ok(await this.Mediator.Send(command));
        }
    }
}
