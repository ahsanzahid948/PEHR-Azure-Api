using Application.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.ProviderLocation.Commands
{
    public class UpdateProviderLocationCommandValidator : AbstractValidator<UpdateProviderLocationCommand>
    {
        public UpdateProviderLocationCommandValidator()
        {

            RuleFor(p => p.UserEmail)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .EmailAddress().WithMessage("{PropertyName} is not valid.");
            RuleFor(p => p.AuthEntityId)
                .NotEmpty().WithMessage("{PropertyName} is required.");

        }
    }
}