namespace Support.API.Controllers.V1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.DTOs;
    using Application.Features.Support.Insurance_Setup.Commands;
    using Application.Features.Support.Insurance_Setup.Queries;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Support.Api.Controllers;

    [ApiVersion("1.0")]
    [Authorize]
    public class InsuranceController : BaseApiController
    {
        /// <summary>Insurance Setup Data</summary>
        /// <param name="insuranceRequest">insuranceRequest.</param>
        /// <returns>InsuranceViewModel.</returns>
        [HttpGet]
        [Route("/v1/Insurances")]
        public async Task<IActionResult> InsuranceInfo([FromQuery]GetInsuranceDataByQuery insuranceRequest)
        {
            return this.Ok(await this.Mediator.Send(insuranceRequest));
        }

        /// <summary>Insurance Details</summary>
        /// <param name="insuranceId">insuranceId.</param>
        /// <returns>InsuranceViewModel.</returns>
        [HttpGet]
        [Route("/v1/Insurances/{insuranceId}/Detail")]
        public async Task<IActionResult> InsuranceDetail(long insuranceId)
        {
            return this.Ok(await this.Mediator.Send(new GetInsuranceDetailByQuery(insuranceId)));
        }

        /// <summary>Create Insurance</summary>
        /// <param name="createInsuranceCommand">createInsuranceCommand.</param>
        /// <returns>True.</returns>
        [HttpPost]
        [Route("/v1/Insurances/{insuranceId}")]
        public async Task<IActionResult> CreateInsurance(CreateInsuranceCommand createInsuranceCommand)
        {
            return this.Ok(await this.Mediator.Send(createInsuranceCommand));
        }

        /// <summary>Update Insurance</summary>
        /// <param name="updateInsuranceCommand">updateInsuranceCommand.</param>
        /// <returns>True.</returns>
        [HttpPut]
        [Route("/v1/Insurances/{insuranceId}")]
        public async Task<IActionResult> UpdateInsurance(UpdateInsuranceCommand updateInsuranceCommand)
        {
            return this.Ok(await this.Mediator.Send(updateInsuranceCommand));
        }

        /// <summary>Delete Insurance</summary>
        /// <param name="updateInsuranceCommand">updateInsuranceCommand.</param>
        /// <returns>True.</returns>
        [HttpDelete]
        [Route("/v1/Insurances/{insuranceId}")]
        public async Task<IActionResult> DeleteInsurance(long insuranceId)
        {
            return this.Ok(await this.Mediator.Send(new DeleteInsuranceCommand(insuranceId)));
        }
    }
}
