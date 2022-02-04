namespace Support.API.Controllers.V1
{
    using System.Threading.Tasks;
    using Application.Features.Support.MenuRoleAudit.Queries;
    using Application.Features.Support.UserAudit.Queries;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Support.Api.Controllers;

    [ApiVersion("1.0")]
    [Authorize]
    public class AuditController : BaseApiController
    {
        /// <summary>
        /// Menu role audit.
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet]
        [Route("/v1/Audit/{roleName}/MenuRoleAudit")]
        public async Task<IActionResult> GetMenuRoleAudit(string roleName)
        {
            return this.Ok(await this.Mediator.Send(new GetMenuRoleAuditByNameQuery(roleName)));
        }

        /// <summary>
        /// Get User Audit.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet]
        [Route("/v1/Audit/{userName}/UserAudit")]
        public async Task<IActionResult> GetUserAudit(string userName)
        {
            return this.Ok(await this.Mediator.Send(new GetUserAuditByNameQuery(userName)));
        }
    }
}
