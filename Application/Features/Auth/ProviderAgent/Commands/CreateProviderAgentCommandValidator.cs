using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.ProviderAgent.Commands
{
    public class CreateProviderAgentCommandValidator : AbstractValidator<CreateProviderAgentCommand>
    {
        public CreateProviderAgentCommandValidator()
        {

            RuleFor(p => p.AuthEntityId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.ProviderId)
                .NotEmpty().WithMessage("{PropertyName} is required.");

        }
    }
}