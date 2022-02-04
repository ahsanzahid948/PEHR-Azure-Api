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
    public class GetMultiPracticeIdsByQuery : IRequest<Response<string>>
    {
        public bool AllPracticeUser { get; set; }
        public long EntityId { get; set; }
        public long AuthEntityId { get; set; }
        public string DbServer { get; set; }
    }

    public class GetMultiPracticeIdsByQueryHandler : IRequestHandler<GetMultiPracticeIdsByQuery, Response<string>>
    {
        private readonly IPracticeRepositoryAsync _practiceRepository;
        private readonly IMapper _mapper;
        public GetMultiPracticeIdsByQueryHandler(IPracticeRepositoryAsync practiceRepository, IMapper mapper)
        {
            _practiceRepository = practiceRepository;
            _mapper = mapper;
        }
        //public async Task<Response<string>> Handle(GetMultiPracticeIdsByQuery request, CancellationToken cancellationToken)
        //{
        //    var practice = await _practiceRepository.GetByIdAsync(request.EntityId).ConfigureAwait(false);
        //    return new Response<string>(_mapper.Map<string>(practice));
        //}

        public async Task<Response<string>> Handle(GetMultiPracticeIdsByQuery request, CancellationToken cancellationToken)
        {
            var practice = await _practiceRepository.GetMultiPracticeIdsAsync(request.AllPracticeUser, request.EntityId, request.AuthEntityId, request.DbServer).ConfigureAwait(false);
            return new Response<string>(data :practice);
        }
    }
}
