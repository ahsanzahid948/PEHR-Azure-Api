namespace Authentication.API.Controllers.V1
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Features.Auth.AdvanceOption.Queries;
    using Application.Features.Auth.Entity.Commads;
    using Application.Features.Auth.EntityUser.Queries;
    using Application.Features.Auth.MultiProvider.Commands;
    using Application.Features.Auth.MultiProvider.Queries;
    using Application.Features.Auth.ProviderAgent.Commands;
    using Application.Features.Auth.ProviderAgent.Queries;
    using Application.Features.Auth.ProviderLocation.Commands;
    using Application.Features.Auth.ProviderLocation.Queries;
    using Application.Features.Entity.Queries;
    using Application.Parameters;
    using Authentication.Api.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    [Authorize]
    public class EntityController : BaseApiController
    {

        [HttpGet]
        [Route("/v1/Entity/{entityId}/{dbEntity}")]
        public async Task<IActionResult> GetEntityById(long entityId, string dbEntity = "PEHR")
        {
            return this.Ok(await this.Mediator.Send(new GetEntityByIdQuery(entityId, dbEntity)));
        }

        /// <summary>
        /// Load Entity Configurations against given CustomerID.
        /// </summary>
        /// <param name="entityId">CustomerID.</param>
        /// <returns>An EntityConfiguration object.</returns>
        [HttpGet]
        [Route("/v1/Entity/{entityId}/Configurations")]
        public async Task<IActionResult> GetConfigurationsById(long entityId)
        {
            return this.Ok(await this.Mediator.Send(new GetConfigurationsByIdQuery(entityId)));
        }

        /// <summary>
        /// Load DOC Server Configurations against given EntityID and userEmail.
        /// </summary>
        /// <param name="entityId">EntityId.</param>
        /// <param name="email">User Email.</param>
        /// <returns>A DOC Server Configurations object.</returns>
        [HttpGet]
        [Route("/v1/Entity/{entityId}/Credentials")]
        public async Task<IActionResult> GetCredentials(long entityId, string email, string authEntityId)
        {
            return this.Ok(await this.Mediator.Send(new GetCredentialsQuery(entityId, email, authEntityId)));
        }

        [HttpGet]
        [Route("/v1/Entity/Users")]
        public async Task<IActionResult> GetEntityUsers(string entityId = "", string filter= "", string token="", string menuRoleId = "")
        {
            return this.Ok(await this.Mediator.Send(new GetEntityUsersByIdQuery(entityId, filter, token, menuRoleId)));
        }

        [HttpGet]
        [Route("/v1/Entity/{entityId}/{email}/ProviderLocation")]
        public async Task<IActionResult> GetProviderLocationById(long entityId, string email)
        {
            return this.Ok(await this.Mediator.Send(new GetProviderLocationByIdQuery(entityId, email)));
        }

        [HttpPost]
        [Route("/v1/Entity/ProviderLocation")]
        public async Task<IActionResult> CreateProviderLocation(CreateProviderLocationCommand creteProviderLocationCommand)
        {
            return this.Ok(await this.Mediator.Send(creteProviderLocationCommand));
        }

        [HttpPut]
        [Route("/v1/Entity/{entityId}/{email}/ProviderLocation")]
        public async Task<IActionResult> UpdateProviderLocation(UpdateProviderLocationCommand updateProviderLocationCommand)
        {
            return this.Ok(await this.Mediator.Send(updateProviderLocationCommand));
        }

        [HttpGet]
        [Route("/v1/Entity/{entityId}/{userId}/MultiProvider")]
        public async Task<IActionResult> GetMultiProviderById(long entityId, long userId)
        {
            return this.Ok(await this.Mediator.Send(new GetMultiProviderByIdQuery(entityId, userId)));
        }

        [HttpPost]
        [Route("/v1/Entity/{entityId}/{userId}/MultiProvider")]
        public async Task<IActionResult> CreateMultiProvider(List<MultiProviderModel> multiProvider)
        {
            return this.Ok(await this.Mediator.Send(new CreateMultiProviderCommand(multiProvider)));
        }

        [HttpDelete]
        [Route("/v1/Entity/{entityId}/{userId}/MultiProvider")]
        public async Task<IActionResult> DeleteMultiProvider(DeleteMultiProviderCommand deleteMultiProviderCommand)
        {
            return this.Ok(await this.Mediator.Send(deleteMultiProviderCommand));
        }

        [HttpGet]
        [Route("/v1/Entity/{entityId}/{providerId}/ProviderAgents")]
        public async Task<IActionResult> GetProviderAgentsById(long entityId, long providerId)
        {
            return this.Ok(await this.Mediator.Send(new GetProviderAgentsByIdQuery(entityId, providerId)));
        }

        [HttpPost]
        [Route("/v1/Entity/{entityId}/{providerId}/ProviderAgents")]
        public async Task<IActionResult> GetProviderAgentsById(CreateProviderAgentCommand createProviderAgentCommand)
        {
            return this.Ok(await this.Mediator.Send(createProviderAgentCommand));
        }

        [HttpGet]
        [Route("/v1/Entity/{entityId}/{userId}/UserAgents")]
        public async Task<IActionResult> GetUserAgentsById(long entityId, long userId)
        {
            return this.Ok(await this.Mediator.Send(new GetUserAgentsByIdQuery(entityId, userId)));
        }

        [HttpGet]
        [Route("/v1/Entity/{entityId}/AdvanceOptions")]
        public async Task<IActionResult> GetAdvanceOptionsById(long entityId)
        {
            return this.Ok(await this.Mediator.Send(new GetAdvanceOptionsByIdQuery(entityId)));
        }

        [HttpGet]
        [Route("/v1/Entity/{authEntityId}/EntityUsers")]
        public async Task<IActionResult> GetEntityUsersById(string authEntityId, string active = "Y")
        {
            return this.Ok(await this.Mediator.Send(new GetEntityUsersByQuery(authEntityId, active)));
        }

        [HttpPut]
        [Route("/v1/Entity")]
        public async Task<IActionResult> UpdateEntity(UpdateEntityCommand updateEntityCommand)
        {
            return this.Ok(await this.Mediator.Send(updateEntityCommand));
        }

        /// <summary>
        /// Delete ProviderAgents.
        /// </summary>
        /// <param name="authEntityId"></param>
        /// <param name="providerId"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpDelete]
        [Route("/v1/Entity/{authEntityId}/{providerId}/ProviderAgents")]
        public async Task<IActionResult> DeleteProviderAgent(long authEntityId, long providerId)
        {
            return this.Ok(await this.Mediator.Send(new DeleteProviderAgentCommand(authEntityId, providerId)));
        }
    }

}
