namespace Authentication.API.Controllers.V1
{
    using System.Threading.Tasks;
    using Application.DTOs;
    using Application.DTOs.Auth.Client;
    using Application.Features.Clients.Queries;
    using Application.Features.Support.Clients.Commands;
    using Application.Features.Support.Clients.Queries;
    using Application.Features.Users.Queries;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Support.Api.Controllers;

    /// <summary>
    /// API Client Features.
    /// </summary>
    [ApiVersion("1.0")]
    [Authorize]
    public class ClientController : BaseApiController
    {
        /// <summary>
        /// Get Users Assigned Implmentation,Managers and Customer List.
        /// </summary>
        /// <param name="payload">PayLoad.</param>
        /// <returns>ListAuthUserModel.</returns>
        [HttpGet]
        [Route("/v1/Clients/Managers")]
        public async Task<IActionResult> GetManagersList(PayLoad payload)
        {
            return this.Ok(await this.Mediator.Send(new GetManagersQuery(payload)));
        }

        /// <summary>
        /// Get PEHR Client List.
        /// </summary>
        /// <param name="payload">PayLoad.</param>
        /// <param name="sales">sales.</param>
        /// <param name="implement">implement.</param>
        /// <param name="cust">cust.</param>
        /// <returns>PehrClientsListModel.</returns>
        [HttpGet]
        [Route("/v1/Clients/ImplemenationManagers")]
        public async Task<IActionResult> GetImplementationManagersList(PayLoad payload, string sales, string implement, string cust)
        {
            return this.Ok(await this.Mediator.Send(new GetClientsListQuery(payload, sales, implement, cust)));
        }

        /// <summary>
        /// Returns All Client List.
        /// </summary>
        /// <param name="obj">Client Filter.</param>
        /// <returns>PehrClientsListModel.</returns>
        [HttpGet]
        [Route("/v1/Clients")]
        public async Task<IActionResult> GetAllClients(ClientFilter obj)
        {
            return this.Ok(await this.Mediator.Send(new GetAllClientsByQuery(obj)));
        }

        /// <summary>
        /// Return Client Detail by Id.
        /// </summary>
        /// <param name="clientId">Cliend Id.</param>
        /// <returns>ClientDetailModel.</returns>
        [HttpGet]
        [Route("/v1/Clients/{clientId}")]
        public async Task<IActionResult> GetClientDetail( string clientId)
        {
            return this.Ok(await this.Mediator.Send(new GetClientDetailByQuery(clientId)));
        }

        /// <summary>
        /// Create New Client.
        /// </summary>
        /// <param name="clientData">ClientProfile.</param>
        /// <returns>True.</returns>
        [HttpPost]
        [Route("/v1/Clients")]
        public async Task<IActionResult> CreateClient(ClientProfileViewModel clientData)
        {
            return this.Ok(await this.Mediator.Send(new CreateClientCommand(clientData)));
        }

        /// <summary>
        /// Get client subscription by entity id.
        /// </summary>
        /// <param name="entityId">entityId.</param>
        /// <returns>ClientSubscriptionViewModel.</returns>
        [HttpGet]
        [Route("/v1/Clients/{entityId}/Subscription")]
        public async Task<IActionResult> GetClientSubscriptionById(long entityId)
        {
            return this.Ok(await this.Mediator.Send(new GetClientSubscriptionByIdQuery(entityId)));
        }
    }
}
