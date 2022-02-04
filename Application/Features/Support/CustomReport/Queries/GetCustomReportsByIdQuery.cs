using Application.DTOs.Support.CustomReports;
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

namespace Application.Features.Support.CustomReport.Queries
{
    public class GetCustomReportsByIdQuery : IRequest<Response<IReadOnlyList<CustomReportViewModel>>>
    {
        public long EntityId { get; set; }
        public GetCustomReportsByIdQuery(long entityId)
        {
            this.EntityId = entityId;
        }
    }
    public class GetCustomReportsByIdQueryHandler : IRequestHandler<GetCustomReportsByIdQuery, Response<IReadOnlyList<CustomReportViewModel>>>
    {
        private readonly ISupportEntityRepositoryAsync _supportEntityRepository;
        private readonly IMapper _mapper;

        public GetCustomReportsByIdQueryHandler(ISupportEntityRepositoryAsync supportEntityRepository, IMapper mapper)
        {
            _supportEntityRepository = supportEntityRepository;
            _mapper = mapper;
        }
        public async Task<Response<IReadOnlyList<CustomReportViewModel>>> Handle(GetCustomReportsByIdQuery request, CancellationToken cancellationToken)
        {
            var customReportList = await _supportEntityRepository.GetCustomReportsByIdAsync(request.EntityId).ConfigureAwait(false);
            return new Response<IReadOnlyList<CustomReportViewModel>>(_mapper.Map<IReadOnlyList<CustomReportViewModel>>(customReportList));
        }
    }
}
