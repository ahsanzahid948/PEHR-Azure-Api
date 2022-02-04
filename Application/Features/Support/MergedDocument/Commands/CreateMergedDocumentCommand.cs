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

namespace Application.Features.Support.MergedDocument.Commands
{
    public class CreateMergedDocumentCommand : IRequest<Response<int>>
    {
        public virtual long? EntityId { get; set; }
        public virtual long? PatientId { get; set; }
        public virtual string DocumentId { get; set; }
        public virtual string User { get; set; }
    }

    public class CreateMergedDocumentCommandHandler : IRequestHandler<CreateMergedDocumentCommand, Response<int>>
    {
        private readonly ISupportEntityRepositoryAsync _supportEntityRepository;
        private readonly IMapper _mapper;

        public CreateMergedDocumentCommandHandler(ISupportEntityRepositoryAsync supportEntityRepository, IMapper mapper)
        {
            _supportEntityRepository = supportEntityRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateMergedDocumentCommand request, CancellationToken cancellationToken)
        {
            var multiProvider = _mapper.Map<Domain.Entities.Support.MergedDocument>(request);
            var userStatus = await _supportEntityRepository.CreateMergedDocumentAsync(multiProvider).ConfigureAwait(false);
            return new Response<int>(userStatus);
        }
    }
}