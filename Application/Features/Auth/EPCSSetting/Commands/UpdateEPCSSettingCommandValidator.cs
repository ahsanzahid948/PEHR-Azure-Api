using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.EPCSSetting.Commands
{
    public class UpdateEPCSSettingCommandValidator : AbstractValidator<UpdateEPCSSettingCommand>
    {
        public UpdateEPCSSettingCommandValidator()
        {

            RuleFor(p => p.Email)
                .EmailAddress().WithMessage("{PropertyName} is not valid.");

        }
    }
}

