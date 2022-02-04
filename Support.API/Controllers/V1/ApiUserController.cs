namespace Support.API.Controllers.V1
{
    using System.Threading.Tasks;
    using Application.Features.ApiUser.Queries;
    using Application.Features.Support.ApiUser.Commands;
    using Application.Features.Users.Commands;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Support.Api.Controllers;

    /// <summary>
    /// Api users controls.
    /// </summary>
    [ApiVersion("1.0")]
    [Authorize]
    public class ApiUserController : BaseApiController
    {
        /// <summary>
        /// Get Api user.
        /// </summary>
        /// <param name="patientId">patientId.</param>
        /// <returns>ApiUserViewModel.</returns>
        [HttpGet]
        [Route("/v1/Patient/{patientId}/User")]
        public async Task<IActionResult> GetApiUserById(long patientId)
        {
            return this.Ok(await this.Mediator.Send(new GetApiUserByIdQuery(patientId)));
        }

        /// <summary>
        /// Get Api user access Logs.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <returns>Status.</returns>
        [HttpGet]
        [Route("/v1/Patient/{email}/Logs")]
        public async Task<IActionResult> GetAccessLogById(string email)
        {
            return this.Ok(await this.Mediator.Send(new GetApiAccessLogByIdQuery(email)));
        }

        /// <summary>
        /// Update Api user.
        /// </summary>
        /// <param name="command">Api user.</param>
        /// <returns>Status.</returns>
        [HttpPut]
        [Route("/v1/Patient/{patientId}/User")]
        public async Task<IActionResult> UpdateApiUserById(UpdateApiUserCommand command)
        {
            return this.Ok(await this.Mediator.Send(command));
        }

        /// <summary>
        /// Create Api user.
        /// </summary>
        /// <param name="command">Api user.</param>
        /// <returns>ApiUser Created.</returns>
        [HttpPost]
        [Route("/v1/Patient/User")]
        public async Task<IActionResult> CreateApiUser(CreateApiUserCommand command)
        {
            return this.Ok(await this.Mediator.Send(command));
        }
    }
}
