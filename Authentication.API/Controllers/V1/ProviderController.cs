namespace Authentication.API.Controllers.V1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Features.Auth.Provider.Commands;
    using Application.Features.Auth.Provider.Queries;
    using Authentication.Api.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    [Authorize]
    public class ProviderController : BaseApiController
    {
        [HttpGet]
        [Route("/v1/Providers")]
        public async Task<IActionResult> GetProviderById([FromQuery]GetProvidersByIdQuery providerRequest)
        {
            return this.Ok(await this.Mediator.Send(providerRequest));
        }

        [HttpGet]
        [Route("/v1/Providers/{providerId}/ProviderDetail")]
        public async Task<IActionResult> GetProviderDetailById(long providerId)
        {
            return this.Ok(await this.Mediator.Send(new GetProviderDetailByIdQuery(providerId)));
        }

        [HttpPost]
        [Route("/v1/Providers/{providerId}")]
        public async Task<IActionResult> CreateProvider(CreateProviderCommand createProviderCommand)
        {
            return this.Ok(await this.Mediator.Send(createProviderCommand));
        }

        [HttpPut]
        [Route("/v1/Providers/{providerId}")]
        public async Task<IActionResult> UpdateProvider(UpdateProviderCommand updateProviderCommand)
        {
            return this.Ok(await this.Mediator.Send(updateProviderCommand));
        }

        [HttpDelete]
        [Route("/v1/Providers/{providerId}")]
        public async Task<IActionResult> DeleteProvider(long providerId)
        {
            return this.Ok(await this.Mediator.Send(new DeleteProviderCommand(providerId)));
        }

        [HttpPatch]
        [Route("/v1/Providers/{providerId}")]
        public async Task<IActionResult> UpdateProviderPatch(long providerId, string providerSignature)
        {
            return this.Ok(await this.Mediator.Send(new UpdateProviderPatchCommand(providerId, providerSignature)));
        }
    }
}
