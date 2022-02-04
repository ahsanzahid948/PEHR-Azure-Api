namespace Application.Features.Auth
{
    using Application.DTOs;
    using Application.DTOs.Support.Practice;
    using Application.Interfaces.Repositories.Auth;
    using Application.Wrappers;
    using AutoMapper;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetAuditDataByQuery : IRequest<Response<List<AuditViewModel>>>
    {
        public PayLoad payload;
        public long seqNum;
        public GetAuditDataByQuery(PayLoad payLoad, long SeqNum)
        {
            payload = payLoad;
            seqNum = SeqNum;
        }
    }
    public class GetAuditDataHandler : IRequestHandler<GetAuditDataByQuery, Response<List<AuditViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly IPracticeRepositoryAsync _practiceRepo;

        public GetAuditDataHandler(IMapper mapper, IPracticeRepositoryAsync practiceRepo)
        {
            _mapper = mapper;
            _practiceRepo = practiceRepo;
        }
        public async Task<Response<List<AuditViewModel>>> Handle(GetAuditDataByQuery request, CancellationToken cancellationToken)
        {
            var response = await _practiceRepo.GetPracticeAuditData(request.payload, request.seqNum);
            return new Response<List<AuditViewModel>>(_mapper.Map<List<AuditViewModel>>(response));

        }
    }
}
