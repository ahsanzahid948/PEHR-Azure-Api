using Application.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.ProviderLocation.Commands
{
    public class CreateProviderLocationCommandValidator : AbstractValidator<CreateProviderLocationCommand>
    {
        public CreateProviderLocationCommandValidator()
        {

            RuleFor(p => p.UserEmail)
                .EmailAddress().WithMessage("{PropertyName} is not valid.");

        }
    }
}
