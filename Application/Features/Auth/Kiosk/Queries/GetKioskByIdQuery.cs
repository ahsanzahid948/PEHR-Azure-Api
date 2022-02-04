using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Application.DTOs.Entity;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using Application.DTOs.KioskAdmin;

namespace Application.Features.Kiosk.Queries
{
    public class GetKioskByIdQuery : IRequest<Response<KioskViewModel>>
    {
        public long EntityId { get; set; }

        public GetKioskByIdQuery(long entityId)
        {
            this.EntityId = entityId;
        }
    }

    public class GetKioskByIdQueryHandler : IRequestHandler<GetKioskByIdQuery, Response<KioskViewModel>>
    {
        private readonly IKioskRepositoryAsync _kioskRepository;
        private readonly IMapper _mapper;
        public GetKioskByIdQueryHandler(IKioskRepositoryAsync kioskRepository, IMapper mapper)
        {
            _kioskRepository = kioskRepository;
            _mapper = mapper;
        }
        public async Task<Response<KioskViewModel>> Handle(GetKioskByIdQuery request, CancellationToken cancellationToken)
        {
            var entityObject = await _kioskRepository.GetByIdAsync(request.EntityId).ConfigureAwait(false);
            return new Response<KioskViewModel>(_mapper.Map<KioskViewModel>(entityObject));
        }
    }
}
