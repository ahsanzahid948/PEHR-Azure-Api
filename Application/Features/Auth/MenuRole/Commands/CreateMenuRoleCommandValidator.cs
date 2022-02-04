using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.MenuRole.Commands
{
    public class CreateMenuRoleCommandValidator : AbstractValidator<CreateMenuRoleCommand>
    {
        public CreateMenuRoleCommandValidator()
        {

            RuleFor(p => p.DefaultEnable)
                .MaximumLength(1).WithMessage("{PropertyName} must not exceed 1 characters.");

        }
    }
}