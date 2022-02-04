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

namespace Application.Features.Auth.PracticeComments.Commands
{
    public class CreateCommentCommand : IRequest<Response<int>>
    {
        public virtual string Comments { get; set; }
        public virtual string EnteredBy { get; set; }
        public virtual long? EntityId { get; set; }

    }

    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Response<int>>
    {
        private readonly IPracticeRepositoryAsync _practiceRepository;
        private readonly IMapper _mapper;
        public CreateCommentCommandHandler(IPracticeRepositoryAsync practiceRepository, IMapper mapper)
        {
            _practiceRepository = practiceRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var practice = _mapper.Map<Domain.Entities.Auth.PracticeComments>(request);
            var userStatus = await _practiceRepository.AddCommentAsync(practice).ConfigureAwait(false);
            return new Response<int>(userStatus);
        }
    }
}