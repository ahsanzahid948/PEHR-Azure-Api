using Application.DTOs.Auth.Practice;
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

namespace Application.Features.Auth.Practice.Queries
{
    public class GetPracticeByIdQuery : IRequest<Response<PracticeViewModel>>
    {
        public long EntityId { get; set; }
        public GetPracticeByIdQuery(long entityId)
        {
            this.EntityId = entityId;
        }
    }

    public class GetPracticeByIdQueryHandler : IRequestHandler<GetPracticeByIdQuery, Response<PracticeViewModel>>
    {
        private readonly IPracticeRepositoryAsync _practiceRepository;
        private readonly IMapper _mapper;
        public GetPracticeByIdQueryHandler(IPracticeRepositoryAsync practiceRepository, IMapper mapper)
        {
            _practiceRepository = practiceRepository;
            _mapper = mapper;
        }
        public async Task<Response<PracticeViewModel>> Handle(GetPracticeByIdQuery request, CancellationToken cancellationToken)
        {
            var practice = await _practiceRepository.GetByIdAsync(request.EntityId).ConfigureAwait(false);
            return new Response<PracticeViewModel>(_mapper.Map<PracticeViewModel>(practice));
        }
    }
}
