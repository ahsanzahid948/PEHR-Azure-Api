using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using FluentValidation;

namespace Application.Features.Entity.Queries
{
    public class GetEntityUsersByIdQueryValidator : AbstractValidator<GetEntityUsersByIdQuery>
    {
        private readonly IEntityRepositoryAsync entityRepository;

        public GetEntityUsersByIdQueryValidator(IEntityRepositoryAsync entityRepository)
        {
            this.entityRepository = entityRepository;
            //RuleFor(p => p.EntityId)
            //   .NotEmpty().WithMessage("{PropertyName} is required.")
            //   .NotNull();
               //.MustAsync(IsEntityExists).WithMessage("{PropertyName} not exists.");
        }

        private async Task<bool> IsEntityExists(long EntityId, CancellationToken cancellationToken)
        {
            var entityObject = await entityRepository.GetByIdAsync(EntityId).ConfigureAwait(false);
            if (entityObject != null)
            {
                return true;
            }
            return false;
        }
    }
}
