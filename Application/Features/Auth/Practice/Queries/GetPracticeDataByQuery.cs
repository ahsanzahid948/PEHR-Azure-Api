namespace Application.Features.Auth
{
    using Application.DTOs;
    using Application.DTOs.Auth;
    using Application.DTOs.Auth.Practice;
    using Application.Interfaces.Repositories.Auth;
    using Application.Wrappers;
    using AutoMapper;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetPracticeDataByQuery : IRequest<Response<PracticeViewModel>>
    {
        public long EntitySeqNum;
        public PayLoad Payload;
        public GetPracticeDataByQuery(PayLoad payLoad, long eseqNum)
        {
            EntitySeqNum = eseqNum;
            Payload = payLoad;
        }
    }
    public class GetPracticeDataHandler : IRequestHandler<GetPracticeDataByQuery, Response<PracticeViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IPracticeRepositoryAsync _practiceRepo;
        public GetPracticeDataHandler(IMapper mapper, IPracticeRepositoryAsync repo)
        {
            _mapper = mapper;
            _practiceRepo = repo;
        }
        public async Task<Response<PracticeViewModel>> Handle(GetPracticeDataByQuery request, CancellationToken cancellationToken)
        {
            var response = await _practiceRepo.GetPracticeProfileData(request.Payload, request.EntitySeqNum);
            return new Response<PracticeViewModel>(_mapper.Map<PracticeViewModel>(response));
        }
    }
}
