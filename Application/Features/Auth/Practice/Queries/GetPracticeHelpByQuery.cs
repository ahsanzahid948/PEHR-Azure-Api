using Application.DTOs.Auth.PracticeHelp;
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
    public class GetPracticeHelpByQuery : IRequest<Response<IReadOnlyList<PracticeHelpViewModel>>>
    {
       
    }

    public class GetPracticeHelpByQueryHandler : IRequestHandler<GetPracticeHelpByQuery, Response<IReadOnlyList<PracticeHelpViewModel>>>
    {
        private readonly IPracticeRepositoryAsync _practiceRepository;
        private readonly IMapper _mapper;
        public GetPracticeHelpByQueryHandler(IPracticeRepositoryAsync practiceRepository, IMapper mapper)
        {
            _practiceRepository = practiceRepository;
            _mapper = mapper;
        }
        public async Task<Response<IReadOnlyList<PracticeHelpViewModel>>> Handle(GetPracticeHelpByQuery request, CancellationToken cancellationToken)
        {
            var practice = await _practiceRepository.GetPracticeHelpAsync().ConfigureAwait(false);
            return new Response<IReadOnlyList<PracticeHelpViewModel>>(_mapper.Map<IReadOnlyList<PracticeHelpViewModel>>(practice));
        }
    }
}