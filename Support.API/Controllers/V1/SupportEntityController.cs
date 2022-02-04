namespace Support.API.Controllers.V1
{
    using System.Threading.Tasks;
    using Application.Features.Support.CustomReport.Queries;
    using Application.Features.Support.MergedDocument.Commands;
    using global::Support.Api.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    [ApiVersion("1.0")]
    [Authorize]
    public class SupportEntityController : BaseApiController
    {
        [HttpGet]
        [Route("/v1/Entity/{entityId}/CustomReports")]
        public async Task<IActionResult> GetCustomReportsById(long entityId)
        {
            return this.Ok(await this.Mediator.Send(new GetCustomReportsByIdQuery(entityId)));
        }

        [HttpPost]
        [Route("/v1/Entity/{entityId}/MergedDocuments")]
        public async Task<IActionResult> CreateMergedDocument(CreateMergedDocumentCommand createMergedDocumentCommand)
        {
            return this.Ok(await this.Mediator.Send(createMergedDocumentCommand));
        }
    }
}
