using Application.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IUserRepositoryAsync userRepository;

        public CreateUserCommandValidator(IUserRepositoryAsync userRepository)
        {
            this.userRepository = userRepository;

            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

           
        }

        private async Task<bool> IsUniqueUserName(string username, CancellationToken cancellationToken)
        {
            var userObject = await userRepository.GetByFilterAsync(username.ToLower()).ConfigureAwait(false);
            if (userObject != null)
            {
                return false;
            }
            return true;
        }
    }
}
