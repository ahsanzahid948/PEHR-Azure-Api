namespace Support.API.Controllers.V1
{
    using System.Threading.Tasks;
    using Application.Features.Support.Invoice;
    using Application.Features.Support.Invoice.Commands;
    using Application.Features.Support.PaymentMethod.Commands;
    using Application.Features.Support.PaymentMethod.Queries;
    using Application.Features.Support.PrefferedCreditCard.Commands;
    using Application.Features.Support.PrefferedCreditCard.Queries;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Support.Api.Controllers;

    [ApiVersion("1.0")]
    [Authorize]
    public class SubscriptionController : BaseApiController
    {
        [HttpGet]
        [Route("/v1/Subscription/{entityId}/PaymentMethods")]
        public async Task<IActionResult> GetPaymentMethodsById(long entityId, string type = "")
        {
            return this.Ok(await this.Mediator.Send(new GetPaymentMethodsByIdQuery(entityId, type)));
        }

        [HttpGet]
        [Route("/v1/Subscription/{entityId}/Payments")]
        public async Task<IActionResult> GetPaymentsById(long entityId, string clientId = "")
        {
            return this.Ok(await this.Mediator.Send(new GetPaymentsByIdQuery(entityId, clientId)));
        }

        [HttpGet]
        [Route("/v1/Subscription/{entityId}/Invoices")]
        public async Task<IActionResult> GetInvoicesById(long entityId)
        {
            return this.Ok(await this.Mediator.Send(new GetInvoicesByIdQuery(entityId)));
        }

        [HttpPost]
        [Route("/v1/Subscription/{entityId}/Invoices")]
        public async Task<IActionResult> GetInvoicesById(CreateInvoiceCommand createInvoiceCommand)
        {
            return this.Ok(await this.Mediator.Send(createInvoiceCommand));
        }

        [HttpPost]
        [Route("/v1/Subscription/{entityId}/PaymentMethods")]
        public async Task<IActionResult> CreatePaymentMethod(CreatePaymentMethodCommand createPaymentMethodCommand)
        {
            return this.Ok(await this.Mediator.Send(createPaymentMethodCommand));
        }

        [HttpPost]
        [Route("/v1/Subscription/{entityId}/Payments")]
        public async Task<IActionResult> CreatePayment(CreatePaymentCommand createPaymentCommand)
        {
            return this.Ok(await this.Mediator.Send(createPaymentCommand));
        }

        /// <summary>
        /// Get Preffered Card
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="type"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet]
        [Route("/v1/Subscription/{entityId}/PrefferedCard")]
        public async Task<IActionResult> GetPrefferedCreditCard(long entityId, string type = "PEHR")
        {
            return this.Ok(await this.Mediator.Send(new GetPrefferedCardByQuery(entityId, type)));
        }

        /// <summary>
        ///Update Preffered Card
        /// </summary>
        /// <param name="updatePrefferedCreditCardCommand"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPut]
        [Route("/v1/Subscription/PrefferedCard")]
        public async Task<IActionResult> PrefferedCreditCard(UpdatePrefferedCardCommand updatePrefferedCreditCardCommand)
        {
            return this.Ok(await this.Mediator.Send(updatePrefferedCreditCardCommand));
        }

        /// <summary>
        /// Delete paymnet method
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpDelete]
        [Route("/v1/Subscription/{paymentId}/PaymentMethods")]
        public async Task<IActionResult> DeletePaymentMethods(long paymentId)
        {
            return this.Ok(await this.Mediator.Send(new DeletePaymentMethodCommand(paymentId)));
        }

        /// <summary>
        /// Update Preffered Cards
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="type"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPut]
        [Route("/v1/Subscription/{entityId}/PrefferedCards")]
        public async Task<IActionResult> UpdatePrefferedCards(long entityId, string type = "PEHR")
        {
            return this.Ok(await this.Mediator.Send(new UpdatePrefferdCardsCommand(entityId, type)));
        }
    }
}
