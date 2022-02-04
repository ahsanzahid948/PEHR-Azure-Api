namespace Authentication.API.Controllers.V1
{
    using System.Threading.Tasks;
    using Application.DTOs;
    using Application.DTOs.Auth.Client;
    using Application.DTOs.Entity;
    using Application.Features.Auth;
    using Application.Features.Auth.Practice.Commands;
    using Application.Features.Auth.Practice.Queries;
    using Application.Features.Auth.PracticeComments.Commands;
    using Application.Features.Auth.PracticeComments.Queries;
    using Authentication.Api.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Practice Client Controls.
    /// </summary>
    [ApiVersion("1.0")]
    [Authorize]
    public class PracticeController : BaseApiController
    {
        /// <summary>Gets Practice Setup Data(Progress/Implementation/Setup/Questions</summary>
        /// <param name="payload">PayLoad.</param>
        /// <param name="entityId">EntitySeqNum.</param>
        /// <returns>PracticeData.</returns>
        [HttpGet]
        [Route("/v1/Practice/{entityId}/PracticeData")]
        public async Task<IActionResult> PracticeInfo([FromQuery] PayLoad payload, long entityId)
        {
            return this.Ok(await this.Mediator.Send(new GetPracticeDataByQuery(payload, entityId)));
        }

        /// <summary>
        /// Get Client Providers Data.
        /// </summary>
        /// <param name="payload">PayLoad.</param>
        /// <param name="enttityId">EntitySeqNum.</param>
        /// <returns>ProviderViewModel.</returns>
        [HttpGet]
        [Route("/v1/Practice/{enttityId}/ProvidersData")]
        public async Task<IActionResult> GetProviderData([FromQuery] PayLoad payload, long enttityId)
        {
            return this.Ok(await this.Mediator.Send(new GetProviderDataByQuery(payload, enttityId)));
        }
        /// <summary>Gets Client Audit Data.</summary>
        /// <param name="payload">PayLoad.</param>
        /// <param name="entityId">EntitySeqNum.</param>
        /// <returns>AuditViewModel.</returns>
        [HttpGet]
        [Route("/v1/Practice/{entityId}/AuditData")]
        public async Task<IActionResult> GetPracticeAuditData(PayLoad payload, long entityId)
        {
            return this.Ok(await this.Mediator.Send(new GetAuditDataByQuery(payload, entityId)));
        }

        /// <summary>Gets Provider Detail.</summary>
        /// <param name="payload">PayLoad.</param>
        /// <param name="entityId">EntitySeqNum.</param>
        /// <param name="providerId">ProviderSeqNum.</param>
        /// <returns>ProviderViewModel.</returns>
        [HttpGet]
        [Route("/v1/Practice/{entityId}/{providerId}/ProviderDetail")]
        public async Task<IActionResult> GetPracticeProviderDetail([FromQuery] PayLoad payload, long entityId, long providerId)
        {
            return this.Ok(await this.Mediator.Send(new GetProviderDetailByQuery(payload, entityId, providerId)));
        }

        /// <summary>
        /// Update Practice Client Profile.
        /// </summary>
        /// <param name="clientData">ClientProfileModel.</param>
        /// <param name="entity">EntityViewModel.</param>
        /// <returns>Updated.</returns>
        [HttpPut]
        [Route("/v1/Practice/UpdateClient")]
        public async Task<IActionResult> UpdatePracticeClientProfile([FromQuery] ClientProfileViewModel clientData, [FromQuery] EntityViewModel entity)
        {
            return this.Ok(await this.Mediator.Send(new UpdateClientDetailCommand(clientData, entity)));
        }

        /// <summary>
        /// Update Practice Configuration.
        /// </summary>
        /// <param name="updateClientConfig">UpdateClientConfiguration Command.</param>
        /// <returns>Configuration Updated Successfully.</returns>
        [HttpPut]
        [Route("/v1/Practice/UpdateConfiguration/")]
        public async Task<IActionResult> UpdatePracticeClientConfiguration(UpdateClientConfigurationCommand updateClientConfig)
        {
            return this.Ok(await this.Mediator.Send(updateClientConfig));
        }

        [HttpGet]
        [Route("/v1/Practice/{entityId}/Comments")]
        public async Task<IActionResult> GetCommentsById(long entityId)
        {
            return this.Ok(await this.Mediator.Send(new GetCommentsByIdQuery(entityId)));
        }

        [HttpPost]
        [Route("/v1/Practice/{entityId}/Comments")]
        public async Task<IActionResult> GetCommentsById(CreateCommentCommand createCommentCommand)
        {
            return this.Ok(await this.Mediator.Send(createCommentCommand));
        }

        [HttpGet]
        [Route("/v1/Practice/{entityId}")]
        public async Task<IActionResult> GetPracticeById(long entityId)
        {
            return this.Ok(await this.Mediator.Send(new GetPracticeByIdQuery(entityId)));
        }

        [HttpPost]
        [Route("/v1/Practice/{entityId}")]
        public async Task<IActionResult> CreatePractice(CreatePracticeCommand createPracticeCommand)
        {
            return this.Ok(await this.Mediator.Send(createPracticeCommand));
        }

        [HttpPut]
        [Route("/v1/Practice/{entityId}")]
        public async Task<IActionResult> UpdatePractice(UpdatePracticeCommand updatePracticeCommand)
        {
            return this.Ok(await this.Mediator.Send(updatePracticeCommand));
        }

        [HttpGet]
        [Route("/v1/Practice/MultiPracticeIds")]
        public async Task<IActionResult> GetMultiPracticeIds([FromQuery] GetMultiPracticeIdsByQuery multiPractice)
        {
            return this.Ok(await this.Mediator.Send(multiPractice));
        }

        [HttpGet]
        [Route("/v1/Practice/PracticeHelp")]
        public async Task<IActionResult> GetPracticeHelp()
        {
            return this.Ok(await this.Mediator.Send(new GetPracticeHelpByQuery()));
        }

        [HttpPatch]
        [Route("/v1/Practice/{entityId}")]
        public async Task<IActionResult> UpdatePatchPractice(long entityId, string companyLogo = "", string noInsuranceNeeded= "", bool removeLogo = false)
        {
            return this.Ok(await this.Mediator.Send(new UpdatePracticePatchCommand(entityId, companyLogo, noInsuranceNeeded, removeLogo)));
        }
    }
}
