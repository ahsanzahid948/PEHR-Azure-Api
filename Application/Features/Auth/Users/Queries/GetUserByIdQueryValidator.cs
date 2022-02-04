using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using FluentValidation;

namespace Application.Features.Users.Queries
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        private readonly IUserRepositoryAsync userRepository;

        public GetUserByIdQueryValidator(IUserRepositoryAsync userRepository)
        {
            this.userRepository = userRepository;
            RuleFor(p => p.UserId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MustAsync(IsUserExists).WithMessage("{PropertyName} not exists.");
        }

        private async Task<bool> IsUserExists(long UserId, CancellationToken cancellationToken)
        {
            var userObject = await userRepository.GetByIdAsync(UserId).ConfigureAwait(false);
            if (userObject != null)
            {
                return true;
            }
            return false;
        }
    }
}