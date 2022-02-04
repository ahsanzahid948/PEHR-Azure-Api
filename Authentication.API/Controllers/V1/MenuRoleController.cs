namespace Authentication.API.Controllers.V1
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Authentication.Api.Controllers;
    using Application.Features.Auth.MenuRole.Queries;
    using Application.Features.Auth.TabMenu.Queries;
    using Application.Features.Auth.MenuButton.Queries;
    using Application.Features.Auth.Menu.Queries;
    using Application.Features.Auth.MenuPrivilege.Queries;
    using Application.Features.Auth.MenuRole.Commands;
    using Application.Features.Auth.MenuPrivilege.Commands;
    using Application.Features.Auth.UserRoleAssignment.Queries;

    [ApiVersion("1.0")]
    [Authorize]
    public class MenuRoleController : BaseApiController
    {
        [HttpGet]
        [Route("/v1/MenuRole")]
        public async Task<IActionResult> GetMenuRoleById([FromQuery] GetMenuRoleByIdQuery menuRoleRequest)
        {
            return this.Ok(await this.Mediator.Send(menuRoleRequest));
        }

        [HttpGet]
        [Route("/v1/MenuRole/{menuRoleId}/Role")]
        public async Task<IActionResult> GetMenuRoleByRoleId(long menuRoleId, [FromQuery] string name = "")
        {
            return this.Ok(await this.Mediator.Send(new GetMenuRoleByRoleIdQuery(menuRoleId, name)));
        }

        /// <summary>
        /// Menu tabs against role id comma separated.
        /// </summary>
        /// <param name="menuRoleId">menuRoleId.</param>
        /// <returns>An menu tab object list.</returns>
        [HttpGet]
        [Route("/v1/MenuRole/Tabs")]
        public async Task<IActionResult> GetTabsById(string menuRoleId = null)
        {
            return this.Ok(await this.Mediator.Send(new GetTabsByIdQuery(menuRoleId)));
        }

        /// <summary>
        /// Menu Buttons against role id comma separated.
        /// </summary>
        /// <param name="menuRoleId">menuRoleId.</param>
        /// <returns>An menu button object list.</returns>
        [HttpGet]
        [Route("/v1/MenuRole/Buttons")]
        public async Task<IActionResult> GetButtonsById(string menuRoleId = null)
        {
            return this.Ok(await this.Mediator.Send(new GetButtonsByIdQuery(menuRoleId)));
        }

        [HttpGet]
        [Route("/v1/MenuRole/Menus")]
        public async Task<IActionResult> GetMenusById(string menuRoleId = null)
        {
            return this.Ok(await this.Mediator.Send(new GetMenusByIdQuery(menuRoleId)));
        }

        [HttpGet]
        [Route("/v1/MenuRole/{menuRoleId}/Privileges")]
        public async Task<IActionResult> GetPrivilegesById(long menuRoleId)
        {
            return this.Ok(await this.Mediator.Send(new GetPrivilegesByIdQuery(menuRoleId)));
        }

        [HttpPost]
        [Route("/v1/MenuRole/{entityId}")]
        public async Task<IActionResult> CreateMenuRole(CreateMenuRoleCommand createMenuRoleCommand)
        {
            return this.Ok(await this.Mediator.Send(createMenuRoleCommand));
        }

        [HttpPut]
        [Route("/v1/MenuRole/{privilegeId}/Privileges")]
        public async Task<IActionResult> UpdatePrivileges(List<PrivilegeModel> updatePrivilegeCommand)
        {
            return this.Ok(await this.Mediator.Send(new UpdatePrivilegeCommand(updatePrivilegeCommand)));
        }


        [HttpGet]
        [Route("/v1/MenuRole/RoleAssignment")]
        public async Task<IActionResult> GetRoleAssignment( [FromQuery] GetRoleAssignmentByIdQuery roleAssignment)
        {
            return this.Ok(await this.Mediator.Send(roleAssignment));
        }

        /// <summary>
        /// Delete Menu role.
        /// </summary>
        /// <param name="menuRoleId"></param>
        /// <param name="emergencyRoleId"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpDelete]
        [Route("/v1/MenuRole/{menuRoleId}/Role")]
        public async Task<IActionResult> DeleteMenuRole(long menuRoleId, string emergencyRoleId = "")
        {
            return this.Ok(await this.Mediator.Send(new DeleteMenuRoleCommand(menuRoleId, emergencyRoleId)));
        }

        /// <summary>
        /// Create Privileges.
        /// </summary>
        /// <param name="createPrivilege"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [Route("/v1/MenuRole/Privileges")]
        public async Task<IActionResult> CreatePrivileges(List<PrivilegeModel> createPrivilege)
        {
            return this.Ok(await this.Mediator.Send(new CreatePrivilegeCommand(createPrivilege)));
        }
    }
}
