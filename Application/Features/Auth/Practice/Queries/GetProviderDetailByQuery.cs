using Application.DTOs;
using Application.DTOs.Auth.Provider;
using Application.Interfaces.Repositories.Auth;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Auth
{
    public class GetProviderDetailByQuery : IRequest<Response<ProviderViewModel>>
    {
        public long ProviderSeqNum { get; set; }
        public long AuthEntitySeqNum { get; set; }
        public PayLoad PayLoad { get; set; }
        public GetProviderDetailByQuery(PayLoad payload, long AuthSeq, long providerSeq)
        {
            PayLoad = payload;
            ProviderSeqNum = providerSeq;
            AuthEntitySeqNum = AuthSeq;
        }
    }
    public class GetProviderDetailHandler : IRequestHandler<GetProviderDetailByQuery, Response<ProviderViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IPracticeRepositoryAsync _practiceRepo;
        public GetProviderDetailHandler(IMapper mapper, IPracticeRepositoryAsync repositoryAsync)
        {
            _mapper = mapper;
            _practiceRepo = repositoryAsync;
        }
        public async Task<Response<ProviderViewModel>> Handle(GetProviderDetailByQuery request, CancellationToken cancellationToken)
        {
            var result = await _practiceRepo.GetProviderDetail(request.PayLoad, request.AuthEntitySeqNum, request.ProviderSeqNum);
            return new Response<ProviderViewModel>(_mapper.Map<ProviderViewModel>(result));
        }
    }
}
