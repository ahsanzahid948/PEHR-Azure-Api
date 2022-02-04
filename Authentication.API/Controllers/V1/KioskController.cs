
namespace Authentication.API.Controllers.V1
{
    using System.Threading.Tasks;
    using Application.Features.Kiosk.Queries;
    using Authentication.Api.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    [Authorize]
    public class KioskController : BaseApiController
    {
        /// <summary>
        /// Get Kiosk admin details by using entityseqnum.
        /// </summary>
        /// <param name="entityId">entityId.</param>
        /// <returns>KioskViewModel.</returns>
        [HttpGet]
        [Route("/v1/Kiosk/{entityId}")]
        public async Task<IActionResult> GetKioskById(long entityId)
        {
            return this.Ok(await this.Mediator.Send(new GetKioskByIdQuery(entityId)));
        }

        /// <summary>
        /// Get Kiosk user details by using entityseqnum and email.
        /// </summary>
        /// <param name="entityId">entityId.</param>
        /// <param name="email">email.</param>
        /// <returns>KioskViewModel.</returns>
        [HttpGet]
        [Route("/v1/KioskUser/{entityId}/{email}")]
        public async Task<IActionResult> GetKioskUser(long entityId, string email)
        {
            return this.Ok(await this.Mediator.Send(new GetKioskUserQuery(entityId, email)));
        }
    }
}
