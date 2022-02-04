namespace Authentication.Api.Controllers.V1
{
    using System;
    using System.Threading.Tasks;
    using Application.Features.Support;
    using Application.Features.Support.LoginLog.Commands;
    using Application.Features.Support.Users.Command;
    using Application.Features.Support.Users.Queries;
    using Application.Features.Users.Commands;
    using Application.Features.Users.Queries;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Support.Api.Controllers;

    /// <summary>
    /// Support User Controls.
    /// </summary>
    [ApiVersion("1.0")]
    [Authorize]
    public class SupportUserController : BaseApiController
    {

        /// <summary>
        /// Get Users.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>ApiUserViewModel.</returns>
        [HttpGet]
        [Route("/v1/Users/{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            return this.Ok(await this.Mediator.Send(new GetUserByIdQuery(userId)));
        }

        /// <summary>
        /// Authenticate User.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password.</param>
        /// <returns>Returns User Record if Authenticated, else Error.</returns>
        [HttpGet]
        [Route("/v1/Users/{email}/{password}/Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return this.Ok("Value is Null");
            }

            var response = await this.Mediator.Send(new GetUserCredentialsQuery(email, password));
            if (response.Data != null)
            {
                return this.Ok();
            }

            return this.NotFound();
        }

        /// <summary>
        /// Update User Password.
        /// </summary>
        /// <param name="UserId">User Seq Num.</param>
        /// <param name="Password">Password.</param>
        /// <returns>Returns Password Updated Else Error.</returns>
        [HttpPut]
        [Route("/v1/Users/{UserId}/{Password}/Password")]
        public async Task<IActionResult> UpdateUserPassword(long UserId, string Password)
        {
            return this.Ok(await this.Mediator.Send(new UpdateUserPasswordByIdCommand(UserId, Password)));
        }

        /// <summary>
        /// Get User Preferences.
        /// </summary>
        /// <param name="userId">userSeqNum.</param>
        /// <returns>Returns User Notification Settings.</returns>
        [HttpGet]
        [Route("/v1/Users/{userId}/Preferences")]
        public async Task<IActionResult> GetUserPrefrences(string userId)
        {

            return this.Ok(await this.Mediator.Send(new GetUserPreferencesByIdQuery(userId)));
        }

        /// <summary>
        /// Get Implementation Manager.
        /// </summary>
        /// <param name="entityId">Entity SeqNum.</param>
        /// <returns>Implementation Manager.</returns>
        [HttpGet]
        [Route("/v1/Users/{entityId}/ImplementationManager")]
        public async Task<IActionResult> GetImplementationManagers(long entityId)
        {
            return this.Ok(await this.Mediator.Send(new GetImplementationManagerByIDQuery(entityId)));
        }

        /// <summary>
        /// Returns Users List.
        /// </summary>
        /// <param name="firstName">FirstName.</param>
        /// <param name="lastName">LastName.</param>
        /// <param name="email">Email.</param>
        /// <param name="type">UserType.</param>
        /// <param name="active">Active.</param>
        /// <param name="accountactivated">AccountActivated.</param>
        /// <returns>SupportUserInfoViewModel.</returns>
        [HttpGet]
        [Route("/v1/Users")]
        public async Task<IActionResult> GetSupportUsersList(string firstName, string lastName, string email, string type, string active, string accountactivated)
        {
            return this.Ok(await this.Mediator.Send(new GetFilterUsersQuery(firstName, lastName, email, type, active, accountactivated)));
        }

        /// <summary>
        /// Update User By Id.
        /// </summary>
        /// <param name="command">command.</param>
        /// <returns>Update User Values By Id.</returns>
        [HttpPut]
        [Route("/v1/Users/{userId}")]
        public async Task<IActionResult> UpdateUserById(UpdateUserCommand command)
        {
            return this.Ok(await this.Mediator.Send(command));
        }

        /// <summary>
        /// Create New User.
        /// </summary>
        /// <param name="command">command.</param>
        /// <returns>Create Support User.</returns>
        [HttpPost]
        [Route("/v1/Users/SupportUser")]
        public async Task<IActionResult> CreateSupportUser([FromQuery] CreateSupportUserCommand command)
        {
            return this.Ok(await this.Mediator.Send(command));
        }

        /// <summary>
        /// Create login log.
        /// </summary>
        /// <param name="createLoginLogCommand">createLoginLogCommand.</param>
        /// <returns>True if User is Created/False if Error Occurs.</returns>
        [HttpPost]
        [Route("/v1/Users/LoginLog")]
        public async Task<IActionResult> CreateLoginLog(CreateLoginLogCommand createLoginLogCommand)
        {
            return this.Ok(await this.Mediator.Send(createLoginLogCommand));
        }

        [HttpPut]
        [Route("/v1/Users/LoginLog")]
        public async Task<IActionResult> UpdateLoginLog(UpdateLoginLogCommand updateLoginLogCommand)
        {
            return this.Ok(await this.Mediator.Send(updateLoginLogCommand));
        }

    }
}
