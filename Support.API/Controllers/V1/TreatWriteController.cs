namespace Support.API.Controllers.V1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Features.Support.SSOINFO.Command;
    using Application.Features.Support.SSOINFO.Queries;
    using Application.Features.Support.SSOURL.Queries;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Support.Api.Controllers;

    [ApiVersion("1.0")]
    [Authorize]
    public class TreatWriteController : BaseApiController
    {
        [HttpGet]
        [Route("/v1/TreatWrite/{entityId}/{type}/Servers")]
        public async Task<IActionResult> GetServers(long entityId, string type)
        {
            return this.Ok(await this.Mediator.Send(new GetServersByIdQuery(entityId, type)));
        }

        /// <summary>
        /// Get credentials
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="type"></param>
        /// <param name="email"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet]
        [Route("/v1/TreatWrite/{entityId}/{type}/{email}/Credentials")]
        public async Task<IActionResult> GetCredentials(long entityId, string type, string email)
        {
            return this.Ok(await this.Mediator.Send(new GetCredentialsByQuery(entityId, type, email)));
        }

        /// <summary>
        /// Create Credentials.
        /// </summary>
        /// <param name="createCredentialCommand"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [Route("/v1/TreatWrite/Credentials")]
        public async Task<IActionResult> SaveCredentials(CreateCredentialCommand createCredentialCommand)
        {
            return this.Ok(await this.Mediator.Send(createCredentialCommand));
        }

        /// <summary>
        /// Update Credentials.
        /// </summary>
        /// <param name="updateCredentialCommand"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPut]
        [Route("/v1/TreatWrite/Credentials")]
        public async Task<IActionResult> UpdateCredentials(UpdateCredentialCommand updateCredentialCommand)
        {
            return this.Ok(await this.Mediator.Send(updateCredentialCommand));
        }
    }
}
