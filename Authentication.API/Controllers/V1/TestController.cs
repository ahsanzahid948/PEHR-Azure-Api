using Application.DTOs.Authentication;
using Application.Interfaces.Services;
using Authentication.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Authentication.API.Controllers.V1
{
    /// <summary>
    /// Test api is working.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : BaseApiController
    {
        private readonly ITestService testService;

        public TestController(ITestService testService)
        {
            this.testService = testService;
        }

        /// <summary>
        /// Test api is awake.
        /// </summary>
        [HttpGet]
        [Route("/v1/Handshake")]
        public async Task<IActionResult> Handshake()
        {

            return this.Ok(await this.testService.HandShake("AuthApi"));
        }
    }
}
