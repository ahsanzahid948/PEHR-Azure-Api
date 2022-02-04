using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using Application.DTOs.ApiUser;

namespace Application.Features.ApiUser.Queries
{
    public class GetApiUserByIdQuery : IRequest<Response<ApiUserViewModel>>
    {
        public long PatientId { get; set; }

        public GetApiUserByIdQuery(long patientId)
        {
            this.PatientId = patientId;
        }
    }

    public class GetApiUserByIdQueryHandler : IRequestHandler<GetApiUserByIdQuery, Response<ApiUserViewModel>>
    {
        private readonly IApiUserRepositoryAsync _supportApiUserRepository;
        private readonly IMapper _mapper;
        public GetApiUserByIdQueryHandler(IApiUserRepositoryAsync supportApiUserRepository, IMapper mapper)
        {
            _supportApiUserRepository = supportApiUserRepository;
            _mapper = mapper;
        }
        public async Task<Response<ApiUserViewModel>> Handle(GetApiUserByIdQuery request, CancellationToken cancellationToken)
        {
            var entityObject = await _supportApiUserRepository.GetByIdAsync(request.PatientId).ConfigureAwait(false);
            return new Response<ApiUserViewModel>(_mapper.Map<ApiUserViewModel>(entityObject));
        }
    }
}
