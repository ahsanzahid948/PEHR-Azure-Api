namespace Authentication.API.Controllers.V1
{
    using System.Threading.Tasks;
    using Application.Features.Auth.Client.Commands;
    using Authentication.Api.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    [Authorize]
    public class AuthClientController : BaseApiController
    {
        /// <summary>
        /// Update client by .
        /// </summary>
        /// <param name="updateClientCommand">updateClientCommand.</param>
        /// <returns>True.</returns>
        [HttpPut]
        [Route("/v1/Clients/{clientId}")]
        public async Task<IActionResult> UpdateClient(UpdateClientCommand updateClientCommand)
        {
            return this.Ok(await this.Mediator.Send(updateClientCommand));
        }
    }
}
