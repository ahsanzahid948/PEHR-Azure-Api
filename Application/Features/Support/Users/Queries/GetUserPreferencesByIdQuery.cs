using Application.DTOs.User;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries
{
    public class GetUserPreferencesByIdQuery : IRequest<Response<IReadOnlyList<UserPreferencesModel>>>
    {

        public string UserId { get; set; }
        public GetUserPreferencesByIdQuery(string seq_num)
        {
            this.UserId = seq_num;
        }
    }
    public class GetUserPreferencesByQueryHandler : IRequestHandler<GetUserPreferencesByIdQuery, Response<IReadOnlyList<UserPreferencesModel>>>
    {
        private readonly ISupportUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;
        public GetUserPreferencesByQueryHandler(ISupportUserRepositoryAsync userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<Response<IReadOnlyList<UserPreferencesModel>>> Handle(GetUserPreferencesByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _userRepository.AuthUserNotificationSettingsAsync(request.UserId);
            return new Response<IReadOnlyList<UserPreferencesModel>>(_mapper.Map<IReadOnlyList<UserPreferencesModel>>(response));

        }
    }
}
