using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using FluentValidation;

namespace Application.Features.Kiosk.Queries
{
    public class GetKioskByIdQueryValidator : AbstractValidator<GetKioskByIdQuery>
    {
        private readonly IKioskRepositoryAsync kioskRepository;

        public GetKioskByIdQueryValidator(IKioskRepositoryAsync kioskRepository)
        {
            this.kioskRepository = kioskRepository;
            RuleFor(p => p.EntityId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MustAsync(IsKioskEntityExists).WithMessage("{PropertyName} not exists.");
        }

        private async Task<bool> IsKioskEntityExists(long EntityId, CancellationToken cancellationToken)
        {
            var entityObject = await kioskRepository.GetByIdAsync(EntityId).ConfigureAwait(false);
            if (entityObject != null)
            {
                return true;
            }
            return false;
        }
    }
}
