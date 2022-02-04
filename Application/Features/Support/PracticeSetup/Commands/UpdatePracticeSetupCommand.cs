using Application.Interfaces.Repositories.Support;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Support.PracticeSetup.Commands
{
    public class UpdatePracticeSetupCommand : IRequest<Response<int>>
    {
        public string Status { get; set; }
        [Required]
        public long EntityId { get; set; }
        [Required]
        public long PracticeId { get; set; }
    }
    public class UpdatePracticeSetupCommandHandler : IRequestHandler<UpdatePracticeSetupCommand, Response<int>>
    {
        private readonly ISupportPracticeRepositoryAsync _supportPracticeRepositoryAsync;
        private readonly IMapper _mapper;

        public UpdatePracticeSetupCommandHandler(ISupportPracticeRepositoryAsync supportPracticeRepositoryAsync, IMapper mapper)
        {
            _supportPracticeRepositoryAsync = supportPracticeRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdatePracticeSetupCommand request, CancellationToken cancellationToken)
        {
            var practiceSetupList = await _supportPracticeRepositoryAsync.UpdatePracticeSetupDetail(request.Status,request.PracticeId, request.EntityId).ConfigureAwait(false);
            return new Response<int>(_mapper.Map<int>(practiceSetupList));
        }
    }
}