using Application.DTOs.Auth.EPCSSetting;
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

namespace Application.Features.Auth.EPCSSetting.Queries
{
    public class GetEPCSSettingsByIdQuery : IRequest<Response<EPCSSettingViewModel>>
    {
        public string Email { get; set; }
        public GetEPCSSettingsByIdQuery(string email)
        {
            this.Email = email;
        }
    }
    public class GetEPCSSettingsByIdQueryHandler : IRequestHandler<GetEPCSSettingsByIdQuery, Response<EPCSSettingViewModel>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;
        public GetEPCSSettingsByIdQueryHandler(IUserRepositoryAsync userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Response<EPCSSettingViewModel>> Handle(GetEPCSSettingsByIdQuery request, CancellationToken cancellationToken)
        {
            var userSettingObject = await _userRepository.GetEPCSSettingsByEmailAsync(request.Email).ConfigureAwait(false);
            return new Response<EPCSSettingViewModel>(_mapper.Map<EPCSSettingViewModel>(userSettingObject));
        }
    }
}