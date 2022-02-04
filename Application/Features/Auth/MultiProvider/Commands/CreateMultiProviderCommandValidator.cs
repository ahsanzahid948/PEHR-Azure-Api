using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.MultiProvider.Commands
{
    public class CreateMultiProviderCommandValidator : AbstractValidator<MultiProviderModel>
    {
        public CreateMultiProviderCommandValidator()
        {

            RuleFor(p => p.AuthEntityId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.ProviderId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required.");

        }
    }
}
