using Application.DTOs.Auth.PracticeComments;
using Application.Interfaces.Repositories.Auth;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Auth.PracticeComments.Queries
{
    public class GetCommentsByIdQuery : IRequest<Response<IReadOnlyList<PracticeCommentsViewModel>>>
    {
        public long EntityId { get; set; }
        public GetCommentsByIdQuery(long entityId)
        {
            this.EntityId = entityId;
        }
    }

    public class GetCommentsByIdQueryHandler : IRequestHandler<GetCommentsByIdQuery, Response<IReadOnlyList<PracticeCommentsViewModel>>>
    {
        private readonly IPracticeRepositoryAsync _practiceRepository;
        private readonly IMapper _mapper;
        public GetCommentsByIdQueryHandler(IPracticeRepositoryAsync practiceRepository, IMapper mapper)
        {
            _practiceRepository = practiceRepository;
            _mapper = mapper;
        }
        public async Task<Response<IReadOnlyList<PracticeCommentsViewModel>>> Handle(GetCommentsByIdQuery request, CancellationToken cancellationToken)
        {
            var comments = await _practiceRepository.GetPracticeCommentsByIdAsync(request.EntityId).ConfigureAwait(false);
            return new Response<IReadOnlyList<PracticeCommentsViewModel>>(_mapper.Map<IReadOnlyList<PracticeCommentsViewModel>>(comments));
        }
    }
}