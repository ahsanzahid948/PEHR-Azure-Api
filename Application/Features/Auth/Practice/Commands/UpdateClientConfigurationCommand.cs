using Application.Interfaces.Repositories.Auth;
using Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Auth
{
    public class UpdateClientConfigurationCommand : IRequest<Response<string>>
    {
        public long SeqNum { get; set; }
        public long EntitySeqNum { get; set; }
        public string Flag { get; set; }
        public string Visible { get; set; }

    }
    public class UpdateClientConfigurationHandler : IRequestHandler<UpdateClientConfigurationCommand, Response<string>>
    {
        public IPracticeRepositoryAsync _practiceRepository;
        public UpdateClientConfigurationHandler(IPracticeRepositoryAsync repo)
        {
            _practiceRepository = repo;
        }
        public async Task<Response<string>> Handle(UpdateClientConfigurationCommand request, CancellationToken cancellationToken)
        {
            var response = await _practiceRepository.UpdateClientConfiguration(request.SeqNum, request.EntitySeqNum, request.Flag, request.Visible);
            return new Response<string>(data: response);

        }
    }
}
