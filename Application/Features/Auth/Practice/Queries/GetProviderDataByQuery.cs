namespace Application.Features.Auth
{
    using Application.DTOs;
    using Application.DTOs.Auth;
    using Application.DTOs.Auth.Provider;
    using Application.Interfaces.Repositories.Auth;
    using Application.Wrappers;
    using AutoMapper;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetProviderDataByQuery : IRequest<Response<List<ProviderViewModel>>>
    {
        public long SeqNum;
        public PayLoad Payload;
        public GetProviderDataByQuery(PayLoad payLoad, long seqNum)
        {
            Payload = payLoad;
            SeqNum = seqNum;
        }
    }

    public class GetProviderDataHandler : IRequestHandler<GetProviderDataByQuery, Response<List<ProviderViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly IPracticeRepositoryAsync _practiceRepo;
        public GetProviderDataHandler(IMapper mapper, IPracticeRepositoryAsync repo)
        {
            _mapper = mapper;
            _practiceRepo = repo;
        }
        public async Task<Response<List<ProviderViewModel>>> Handle(GetProviderDataByQuery request, CancellationToken cancellationToken)
        {
            var result = await _practiceRepo.GetProviderData(request.Payload, request.SeqNum);
            return new Response<List<ProviderViewModel>>(_mapper.Map<List<ProviderViewModel>>(result));
        }
    }
}
