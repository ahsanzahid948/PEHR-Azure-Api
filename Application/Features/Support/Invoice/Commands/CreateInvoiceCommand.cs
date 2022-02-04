using Application.DTOs.Common.CreateRequestResponse;
using Application.Interfaces.Repositories.Support;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Support.Invoice.Commands
{
    public class CreateInvoiceCommand : IRequest<Response<CreateRequestResponse>>
    {
        public virtual long? ClientId { get; set; }
        public virtual long? InvoiceNumber { get; set; }
        public virtual double? InvoiceAmount { get; set; }
        public virtual long? AuthEntityId { get; set; }
        public virtual string InvoiceName { get; set; }
    }

    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Response<CreateRequestResponse>>
    {
        private readonly ISubscriptionRepositoryAsync _paymentMethodRepository;
        private readonly IMapper _mapper;

        public CreateInvoiceCommandHandler(ISubscriptionRepositoryAsync paymentMethodRepository, IMapper mapper)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _mapper = mapper;
        }

        public async Task<Response<CreateRequestResponse>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var paymentInvoice = _mapper.Map<Domain.Entities.Support.PaymentInvoice>(request);
            var userStatus = await _paymentMethodRepository.CreateInvoiceAsync(paymentInvoice).ConfigureAwait(false);
            return new Response<CreateRequestResponse>(userStatus);
        }
    }
}