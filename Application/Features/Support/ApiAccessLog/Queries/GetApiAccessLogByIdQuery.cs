using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using Application.DTOs.ApiUser;
using Application.DTOs.ApiAccessLog;
using System.Collections.Generic;

namespace Application.Features.ApiUser.Queries
{
    public class GetApiAccessLogByIdQuery : IRequest<Response<IReadOnlyList<ApiAccessLogViewModel>>>
    {
        public string Email { get; set; }

        public GetApiAccessLogByIdQuery(string email)
        {
            this.Email = email;
        }
    }

    public class GetApiAccessLogByIdQueryHandler : IRequestHandler<GetApiAccessLogByIdQuery, Response<IReadOnlyList<ApiAccessLogViewModel>>>
    {
        private readonly IApiUserRepositoryAsync _supportApiUserRepository;
        private readonly IMapper _mapper;
        public GetApiAccessLogByIdQueryHandler(IApiUserRepositoryAsync supportApiUserRepository, IMapper mapper)
        {
            _supportApiUserRepository = supportApiUserRepository;
            _mapper = mapper;
        }
        public async Task<Response<IReadOnlyList<ApiAccessLogViewModel>>> Handle(GetApiAccessLogByIdQuery request, CancellationToken cancellationToken)
        {
            var entityObject = await _supportApiUserRepository.GetAccessLogByFilterAsync(request.Email.ToUpper()).ConfigureAwait(false);
            return new Response<IReadOnlyList<ApiAccessLogViewModel>>(_mapper.Map<IReadOnlyList<ApiAccessLogViewModel>>(entityObject));
        }
    }
}
