using Application.DTOs;
using Application.DTOs.Auth.ViewModel;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries
{
    public class GetManagersQuery : IRequest<Response<ClientDataManagerViewModel>>
    {
        public PayLoad Payload;

        public GetManagersQuery(PayLoad obj)
        {
            Payload = obj;
        }
    }
    public class GetManagersQueryHandler : IRequestHandler<GetManagersQuery, Response<ClientDataManagerViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ISupportUserRepositoryAsync _userRepositoryAsync;
        public GetManagersQueryHandler(IMapper map, ISupportUserRepositoryAsync repo)
        {
            _mapper = map;
            _userRepositoryAsync = repo;
        }
        public async Task<Response<ClientDataManagerViewModel>> Handle(GetManagersQuery request, CancellationToken cancellationToken)
        {
            var response = await _userRepositoryAsync.EntityManagersAsync(request.Payload);
            return new Response<ClientDataManagerViewModel>(_mapper.Map<ClientDataManagerViewModel>(response));
        }


    }
}
