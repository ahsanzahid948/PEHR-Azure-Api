using Application.DTOs.Common.CreateRequestResponse;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Support.LoginLog.Commands
{
    public class CreateLoginLogCommand : IRequest<Response<CreateRequestResponse>>
    {
        public virtual string LoginUser { get; set; }
        public virtual DateTime? LoginTime { get; set; }
        public virtual string BrowserType { get; set; }
        public virtual string Resolution { get; set; }
        public virtual string DownloadSpeed { get; set; }
        public virtual string UploadSpeed { get; set; }
        public virtual long? AuthEntityId { get; set; }
        public virtual string FrontendVersion { get; set; }
        public virtual string BackendVersion { get; set; }
        public virtual string NPI { get; set; }
    }

    public class CreateLoginLogCommandHandler : IRequestHandler<CreateLoginLogCommand, Response<CreateRequestResponse>>
    {
        private readonly ISupportUserRepositoryAsync _supportUserRepository;
        private readonly IMapper _mapper;
        public CreateLoginLogCommandHandler(ISupportUserRepositoryAsync supportUserRepository, IMapper mapper)
        {
            _supportUserRepository = supportUserRepository;
            _mapper = mapper;

        }
        public async Task<Response<CreateRequestResponse>> Handle(CreateLoginLogCommand request, CancellationToken cancellationToken)
        {
            var loginLog = _mapper.Map<Domain.Entities.Support.LoginLog>(request);
            var userStatus = await _supportUserRepository.CreateLoginLogAsync(loginLog).ConfigureAwait(false);
            return new Response<CreateRequestResponse>(userStatus);
        }
    }
}