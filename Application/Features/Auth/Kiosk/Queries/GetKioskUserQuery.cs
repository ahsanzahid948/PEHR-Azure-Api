using Application.DTOs.KioskAdmin;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Kiosk.Queries
{
    public class GetKioskUserQuery : IRequest<Response<KioskViewModel>>
    {
        public long EntityId { get; set; }
        public string Email { get; set; }

        public GetKioskUserQuery(long entityId, string email)
        {
            this.EntityId = entityId;
            this.Email = email;
        }
    }
    public class GetKioskUserQueryHandler : IRequestHandler<GetKioskUserQuery, Response<KioskViewModel>>
    {
        private readonly IKioskRepositoryAsync _kioskRepository;
        private readonly IMapper _mapper;
        public GetKioskUserQueryHandler(IKioskRepositoryAsync kioskRepository, IMapper mapper)
        {
            _kioskRepository = kioskRepository;
            _mapper = mapper;
        }
        public async Task<Response<KioskViewModel>> Handle(GetKioskUserQuery request, CancellationToken cancellationToken)
        {
            var entityObject = await _kioskRepository.GetUserAsync(request.EntityId, request.Email).ConfigureAwait(false);
            return new Response<KioskViewModel>(_mapper.Map<KioskViewModel>(entityObject));
        }
    }
}
