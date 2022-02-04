using Application.DTOs.Support.PracticeSetup;
using Application.Interfaces.Repositories.Support;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Support.PracticeSetup.Queries
{
    public class GetPracticeSetupByIdQuery : IRequest<Response<IReadOnlyList<PracticeSetupViewModel>>>
    {
        public long EntityId { get; set; }
        public GetPracticeSetupByIdQuery(long entityId)
        {
            this.EntityId = entityId;
        }
    }
    public class GetPracticeSetupByIdQueryHandler : IRequestHandler<GetPracticeSetupByIdQuery, Response<IReadOnlyList<PracticeSetupViewModel>>>
    {
        private readonly ISupportPracticeRepositoryAsync _supportPracticeRepositoryAsync;
        private readonly IMapper _mapper;

        public GetPracticeSetupByIdQueryHandler(ISupportPracticeRepositoryAsync supportPracticeRepositoryAsync, IMapper mapper)
        {
            _supportPracticeRepositoryAsync = supportPracticeRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<IReadOnlyList<PracticeSetupViewModel>>> Handle(GetPracticeSetupByIdQuery request, CancellationToken cancellationToken)
        {
            var practiceSetupList = await _supportPracticeRepositoryAsync.GetPracticeSetupById(request.EntityId).ConfigureAwait(false);
            return new Response<IReadOnlyList<PracticeSetupViewModel>>(_mapper.Map<IReadOnlyList<PracticeSetupViewModel>>(practiceSetupList));
        }
    }
}
