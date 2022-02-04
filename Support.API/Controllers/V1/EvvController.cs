namespace Support.API.Controllers.V1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Features.Support.EvvStateTimeZone.Queries;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Support.Api.Controllers;

    [ApiVersion("1.0")]
    [Authorize]
    public class EvvController : BaseApiController
    {
        [HttpGet]
        [Route("/v1/EVV/{state}/Configurations")]
        public async Task<IActionResult> GetConfigurationsByState(string state)
        {
            return this.Ok(await this.Mediator.Send(new GetConfigurationsByStateQuery(state)));
        }

        [HttpGet]
        [Route("/v1/EVV/{state}/Tasks")]
        public async Task<IActionResult> GetTasksByState(string state)
        {
            return this.Ok(await this.Mediator.Send(new GetTasksByStateQuery(state)));
        }

    }
}
