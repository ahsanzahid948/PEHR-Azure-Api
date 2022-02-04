namespace Authentication.Api.Controllers.V1
{
    using System.Threading.Tasks;
    using Application.DTOs.Auth.UserSetting;
    using Application.Features.Auth.DataExportSetting.Commands;
    using Application.Features.Auth.DataExportSetting.Queries;
    using Application.Features.Auth.EPCSSetting.Commands;
    using Application.Features.Auth.EPCSSetting.Queries;
    using Application.Features.Auth.InternalUser.Queries;
    using Application.Features.Auth.UserEntities.Queries;
    using Application.Features.Auth.UserLog.Commands;
    using Application.Features.Auth.Users.Commands;
    using Application.Features.Auth.Users.Queries;
    using Application.Features.Auth.UserSetting.Commands;
    using Application.Features.Auth.UserSetting.Queries;
    using Application.Features.Users.Commands;
    using Application.Features.Users.Queries;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    [Authorize]
    public class UserController : BaseApiController
    {

        [HttpGet]
        [Route("/v1/Users/{userId}")]
        public async Task<IActionResult> GetUserById(long userId)
        {
            return this.Ok(await this.Mediator.Send(new GetUserByIdQuery(userId)));
        }

        /// <summary>
        ///  Create New User.
        /// </summary>
        /// <param name="command">command.</param>
        /// <returns>True if User is Created/False if Error Occurs.</returns>
        [HttpPost]
        [Route("/v1/Users")]
        public async Task<IActionResult> Post(CreateUserCommand command)
        {
            return this.Ok(await this.Mediator.Send(command));
        }

        [HttpPut]
        [Route("/v1/Users/{userId}")]
        public async Task<IActionResult> UpdateUserById(UpdateUserCommand command)
        {
            return this.Ok(await this.Mediator.Send(command));
        }

        [HttpGet]
        [Route("/v1/Users/{email}/EPCSSettings")]
        public async Task<IActionResult> GetEPCSSettingsById(string email)
        {
            return this.Ok(await this.Mediator.Send(new GetEPCSSettingsByIdQuery(email)));
        }

        [HttpPut]
        [Route("/v1/Users/{email}/EPCSSettings")]
        public async Task<IActionResult> UpdateEPCSSetting(UpdateEPCSSettingCommand updateEPCSSettingCommand)
        {
            return this.Ok(await this.Mediator.Send(updateEPCSSettingCommand));
        }

        [HttpGet]
        [Route("/v1/Users/{userId}/DataExportSettings")]
        public async Task<IActionResult> GetDataExportSettingsById(long userId)
        {
            return this.Ok(await this.Mediator.Send(new GetDataExportSettingsByIdQuery(userId)));
        }

        [HttpPost]
        [Route("/v1/Users/{userId}/DataExportSettings")]
        public async Task<IActionResult> CreateDataExportSetting(CreateDataExportSettingCommand createDataExportSettingCommand)
        {
            return this.Ok(await this.Mediator.Send(createDataExportSettingCommand));
        }

        [HttpPut]
        [Route("/v1/Users/{settingId}/DataExportSettings")]
        public async Task<IActionResult> UpdateDataExportSetting(UpdateDataExportSettingCommand createDataExportSettingCommand)
        {
            return this.Ok(await this.Mediator.Send(createDataExportSettingCommand));
        }

        [HttpGet]
        [Route("/v1/Users/{email}/UserSettings")]
        public async Task<IActionResult> GetUserSettingsById(string email)
        {
            return this.Ok(await this.Mediator.Send(new GetUserSettingsByIdQuery(email)));
        }

        [HttpPut]
        [Route("/v1/Users/{email}/UserSettings")]
        public async Task<IActionResult> UpdateUserSetting([FromBody] UserSettingVewModel updateUserSettingCommand, string email)
        {
            return this.Ok(await this.Mediator.Send(new UpdateUserSettingCommand(updateUserSettingCommand, email)));
        }

        /// <summary>
        /// Get users.
        /// </summary>
        /// <param name="query">query.</param>
        /// <returns>UserViewModel <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [Route("/v1/Users/Search")]
        public async Task<IActionResult> GetUsersByFilter([FromBody] GetUsersByFilterQuery query)
        {
            return this.Ok(await this.Mediator.Send(query));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="createUserLogCommand">createUserLogCommand</param>
        /// <returns>true <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [Route("/v1/Users/UserLog")]
        public async Task<IActionResult> CreateUserLong(CreateUserLogCommand createUserLogCommand)
        {
            return this.Ok(await this.Mediator.Send(createUserLogCommand));
        }

        [HttpPut]
        [Route("/v1/Users/UserLog")]
        public async Task<IActionResult> UpdateUserLong(UpdateUserLogCommand updateUserLogCommand)
        {
            return this.Ok(await this.Mediator.Send(updateUserLogCommand));
        }

        [HttpGet]
        [Route("/v1/Users/{email}/Entities")]
        public async Task<IActionResult> GetUsersEntities(string email, string epcs = "", string approveEpcs = "", string authEntityId = "")
        {
            return this.Ok(await this.Mediator.Send(new GetUserEntitiesByQuery(email, epcs, approveEpcs, authEntityId)));
        }

        /// <summary>
        /// User profile against the email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>UserViewModel <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("/v1/Users/ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email, string token)
        {
            return this.Ok(await this.Mediator.Send(new GetUserByQuery(email, token)));
        }

        /// <summary>
        /// Update user password by token 
        /// </summary>
        /// <param name="updateUserPasswordCommand"></param>
        /// <returns>true <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [AllowAnonymous]
        [HttpPut]
        [Route("/v1/Users/ForgetPassword")]
        public async Task<IActionResult> UpdateUserPassword(UpdateUserPasswordByTokenCommand updateUserPasswordCommand)
        {
            return this.Ok(await this.Mediator.Send(updateUserPasswordCommand));
        }

        /// <summary>
        /// Update user token
        /// </summary>
        /// <returns>True <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [AllowAnonymous]
        [HttpPut]
        [Route("/v1/Users/Activate")]
        public async Task<IActionResult> UpdateUserToken(UpdateUserTokenCommand updateUserTokenCommand)
        {
            return this.Ok(await this.Mediator.Send(updateUserTokenCommand));
        }

        /// <summary>
        /// change password for login user
        /// </summary>
        /// <param name="updateUserPasswordCommand"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPut]
        [Route("/v1/Users/Password")]
        public async Task<IActionResult> ChangeUserPassword(UpdateUserPasswordCommand updateUserPasswordCommand)
        {
            return this.Ok(await this.Mediator.Send(updateUserPasswordCommand));
        }

        [HttpPut]
        [Route("/v1/Users/EPCSApprove")]
        public async Task<IActionResult> UppdateEPCSApprove(UpdateEpcsApproveCommand updateEpcsApproveCommand)
        {
            return this.Ok(await this.Mediator.Send(updateEpcsApproveCommand));
        }

        /// <summary>
        /// Delete data export settings
        /// </summary>
        /// <param name="settingId"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpDelete]
        [Route("/v1/Users/{settingId}/DataExportSettings")]
        public async Task<IActionResult> DeleteDataExportSetting(long settingId)
        {
            return this.Ok(await this.Mediator.Send(new DeleteDataExportSettingCommand(settingId)));
        }

        /// <summary>
        /// Get the count of user provider
        /// </summary>
        /// <param name="email"></param>
        /// <param name="providerId"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet]
        [Route("/v1/Users/{email}/{providerId}/UserProvider")]
        public async Task<IActionResult> GetUserProviderCount(string email, long providerId)
        {
            return this.Ok(await this.Mediator.Send(new GetUserProviderCountByQuery(email, providerId)));
        }

        /// <summary>
        /// Get internal users.
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="practiceRcm"></param>
        /// <param name="active"></param>
        /// <param name="rcmType"></param>
        /// <param name="rcmTypeTwo"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet]
        [Route("/v1/Users/{entityId}/InternalUsers")]
        public async Task<IActionResult> GetInternalUsers(long entityId, bool practiceRcm, string active, string rcmType, string rcmTypeTwo)
        {
            return this.Ok(await this.Mediator.Send(new GetInternalUsersByIdQuery(entityId, practiceRcm, active, rcmType, rcmTypeTwo)));
        }
    }
}
