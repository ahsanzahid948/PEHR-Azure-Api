namespace Authentication.API.Controllers
{
    using Application.DTOs.Authentication;
    using Application.Interfaces.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/v1/Authentication/Authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return this.Ok(await this.authenticationService.AuthenticateAsync(request, this.GenerateIPAddress()));
        }

        /// <summary>
        /// Kiosk user auhtentication.
        /// </summary>
        /// <param name="request">user credentials.</param>
        /// <returns>Token and intake template seq_num.</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("/v1/Authentication/AuthenticateKiosk")]
        public async Task<IActionResult> AuthenticateKioskAsync(AuthenticationRequest request)
        {
            return this.Ok(await this.authenticationService.AuthenticateKioskAsync(request, this.GenerateIPAddress()));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/v1/Authentication/Refresh")]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest tokenRequest)
        {
            return this.Ok(await this.authenticationService.RefreshTokenAsync(tokenRequest, this.GenerateIPAddress()));
        }

        /// <summary>
        /// Refresh Kiosk token.
        /// </summary>
        /// <param name="tokenRequest">Token.</param>
        /// <returns>New Token.</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("/v1/Authentication/RefreshKioskToken")]
        public async Task<ActionResult> RefreshKioskToken([FromBody] RefreshTokenRequest tokenRequest)
        {
            return this.Ok(await this.authenticationService.RefreshKioskTokenAsync(tokenRequest, this.GenerateIPAddress()));
        }

        private string GenerateIPAddress()
        {
            if (this.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                return this.Request.Headers["X-Forwarded-For"];
            }
            else
            {
                return this.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
        }
    }
}