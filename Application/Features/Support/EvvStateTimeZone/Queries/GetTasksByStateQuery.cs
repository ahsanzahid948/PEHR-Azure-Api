using Application.DTOs.Support.EvvTask;
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

namespace Application.Features.Support.EvvStateTimeZone.Queries
{
    public class GetTasksByStateQuery : IRequest<Response<IReadOnlyList<EvvTaskViewModel>>>
    {
        public string State { get; set; }
        public GetTasksByStateQuery(string state)
        {
            this.State = state;
        }
    }
    public class GetTasksByStateQueryQueryHandler : IRequestHandler<GetTasksByStateQuery, Response<IReadOnlyList<EvvTaskViewModel>>>
    {
        private readonly IEvvRepositoryAsync _evvRepository;
        private readonly IMapper _mapper;

        public GetTasksByStateQueryQueryHandler(IEvvRepositoryAsync evvRepository, IMapper mapper)
        {
            _evvRepository = evvRepository;
            _mapper = mapper;
        }
        public async Task<Response<IReadOnlyList<EvvTaskViewModel>>> Handle(GetTasksByStateQuery request, CancellationToken cancellationToken)
        {
            var paymentMethodsList = await _evvRepository.GetEvvTasksByStateAsync(request.State).ConfigureAwait(false);
            return new Response<IReadOnlyList<EvvTaskViewModel>>(_mapper.Map<IReadOnlyList<EvvTaskViewModel>>(paymentMethodsList));
        }
    }
}

