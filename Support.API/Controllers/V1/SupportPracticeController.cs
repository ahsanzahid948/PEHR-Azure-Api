namespace Support.API.Controllers.V1
{
    using System.Threading.Tasks;
    using Application.Features.Support.PracticeSetup.Commands;
    using Application.Features.Support.PracticeSetup.Queries;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Support.Api.Controllers;

    [ApiVersion("1.0")]
    [Authorize]
    public class SupportPracticeController : BaseApiController
    {
        [HttpGet]
        [Route("/v1/Practice/{entityId}/Setup")]
        public async Task<IActionResult> GetPracticeSetupById(long entityId)
        {
            return this.Ok(await this.Mediator.Send(new GetPracticeSetupByIdQuery(entityId)));
        }

        /// <summary>
        /// Update practice setup
        /// </summary>
        /// <param name="updatePracticeSetupCommand"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPut]
        [Route("/v1/Practice/Setup")]
        public async Task<IActionResult> UpdatePracticeSetup(UpdatePracticeSetupCommand updatePracticeSetupCommand)
        {
            return this.Ok(await this.Mediator.Send(updatePracticeSetupCommand));
        }
    }
}
