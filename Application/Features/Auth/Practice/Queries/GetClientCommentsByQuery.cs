namespace Application.Features.Support.Practice.Queries
{
    using Application.DTOs;
    using Application.DTOs.Auth.Practice;
    using Application.DTOs.Auth.PracticeComments;
    using Application.Interfaces.Repositories.Auth;
    using Application.Wrappers;
    using AutoMapper;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetClientCommentsByQuery:IRequest<Response<List<PracticeCommentsViewModel>>>
    {
        public PayLoad payload;
        public long EntitySeqNum;
        public GetClientCommentsByQuery(PayLoad pay,long eSeqNum)
        {
            payload = pay;
            EntitySeqNum = eSeqNum;
        }
    }

    public class GetClientCommentsHandler : IRequestHandler<GetClientCommentsByQuery, Response<List<PracticeCommentsViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly IPracticeRepositoryAsync _practiceRepo;

        public GetClientCommentsHandler(IMapper mapper,IPracticeRepositoryAsync repo)
        {
            _mapper = mapper;
            _practiceRepo = repo;
        }
        public async Task<Response<List<PracticeCommentsViewModel>>> Handle(GetClientCommentsByQuery request, CancellationToken cancellationToken)
        {
            var response =await _practiceRepo.GetPracticeClientComments(request.payload, request.EntitySeqNum);
            return new Response<List<PracticeCommentsViewModel>>(_mapper.Map<List<PracticeCommentsViewModel>>(response));
        }
    }
}
