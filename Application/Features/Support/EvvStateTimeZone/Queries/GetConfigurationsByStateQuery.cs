using Application.DTOs.Support.EvvStateTimeZone;
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
    public class GetConfigurationsByStateQuery : IRequest<Response<IReadOnlyList<EvvStateTimeZoneViewModel>>>
    {
        public string State { get; set; }
        public GetConfigurationsByStateQuery (string state)
        {
            this.State = state;
        }
    }
    public class GetCustomReportsByIdQueryHandler : IRequestHandler<GetConfigurationsByStateQuery, Response<IReadOnlyList<EvvStateTimeZoneViewModel>>>
    {
        private readonly IEvvRepositoryAsync _evvRepository;
        private readonly IMapper _mapper;

        public GetCustomReportsByIdQueryHandler(IEvvRepositoryAsync evvRepository, IMapper mapper)
        {
            _evvRepository = evvRepository;
            _mapper = mapper;
        }
        public async Task<Response<IReadOnlyList<EvvStateTimeZoneViewModel>>> Handle(GetConfigurationsByStateQuery request, CancellationToken cancellationToken)
        {
            var paymentMethodsList = await _evvRepository.GetConfigurationsByStateAsync(request.State).ConfigureAwait(false);
            return new Response<IReadOnlyList<EvvStateTimeZoneViewModel>>(_mapper.Map<IReadOnlyList<EvvStateTimeZoneViewModel>>(paymentMethodsList));
        }
    }
}
