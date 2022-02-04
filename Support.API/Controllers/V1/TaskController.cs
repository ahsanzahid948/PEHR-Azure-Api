namespace Authentication.API.Controllers.V1
{
    using System.Threading.Tasks;
    using Application.DTOs;
    using Application.DTOs.Task;
    using Application.DTOs.Ticket;
    using Application.Features.Support.Tasks.Queries;
    using Application.Features.Support.Tickets.Queries;
    using Application.Features.Tasks.Commands;
    using Application.Features.Tasks.Queries;
    using Application.Features.Users.Queries;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Support.Api.Controllers;

    /// <summary>
    /// User Tasks Controls.
    /// </summary>
    [ApiVersion("1.0")]
    [Authorize]
    public class TaskController : BaseApiController
    {
        /// <summary>
        /// Get Task List.
        /// </summary>
        /// <param name="task">Task Search Filter.</param>
        /// <returns>List of SupportTaskViewModel.</returns>
        [HttpGet]
        [Route("/v1/Tasks")]
        public async Task<IActionResult> GetTask([FromQuery] TaskFilter task)
        {
            return this.Ok(await this.Mediator.Send(new GetAllTasksByQuery(task)));
        }

        /// <summary>
        /// Get Client Tasks.
        /// </summary>
        /// <param name="entityId">Entity Id.</param>
        /// <param name="status">Task Status.</param>
        /// <returns>SupportTaskViewModel.</returns>
        [HttpGet]
        [Route("/v1/Tasks/{entityId}/{status}")]
        public async Task<IActionResult> GetClientTasks(long entityId, string status)
        {
            return this.Ok(await this.Mediator.Send(new GetClientTasksByIdQuery(entityId, status)));
        }

        /// <summary>
        /// Get All User Tasks.
        /// </summary>
        /// <param name="payload">payLoad.</param>
        /// <param name="userId">userId.</param>
        /// <returns>LoadProgressDataModel.</returns>
        [HttpGet]
        [Route("/v1/Tasks/{userId}")]
        public async Task<IActionResult> GetAllTasks([FromQuery] PayLoad payload, long userId)
        {
            return this.Ok(await this.Mediator.Send(new GetTasksProgressByIdQuery(payload, userId)));
        }

        /// <summary>
        /// Get Task Detail.
        /// </summary>
        /// <param name="taskId">TaskId.</param>
        /// <returns>SupportTicketViewModel.</returns>
        [HttpGet]
        [Route("/v1/Tasks/{taskId}")]
        public async Task<IActionResult> GetTaskDetail(long taskId)
        {
            return this.Ok(await this.Mediator.Send(new GetTicketByIdQuery(taskId)));
        }

        /// <summary>
        /// Save Task.
        /// </summary>
        /// <param name="supportTask">SupportTicketViewModel.</param>
        /// <returns>Created Successfully.</returns>
        [HttpPost]
        [Route("/v1/Tasks/")]
        public async Task<IActionResult> CreateTask(CreateTicketCommand supportTask)
        {
            return this.Ok(await this.Mediator.Send(supportTask));
        }
    }
}
