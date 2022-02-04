using Application.DTOs.Auth.UserSetting;
using Application.DTOs.Common.PagingResponse;
using Application.DTOs.User;
using Application.Interfaces.Repositories;
using Application.Parameters;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Auth.Users.Queries
{
    public class GetUsersByFilterQuery : IRequest<Response<PageResponse<UserViewModel>>>
    {
        public string UserId { get; set; }
        public string Active { get; set; }
        public string SortName { get; set; }
        public string SortOrder { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public RequestPageParameter PageParameter { get; set; }

    }


    public class GetUsersByFilterQueryHandler : IRequestHandler<GetUsersByFilterQuery, Response<PageResponse<UserViewModel>>>
    {
        private readonly IUserRepositoryAsync _userRepository;

        private readonly IMapper _mapper;

        public GetUsersByFilterQueryHandler(IUserRepositoryAsync userRepository, IMapper mapper)
        {
            _userRepository = userRepository;

            _mapper = mapper;
        }
        public async Task<Response<PageResponse<UserViewModel>>> Handle(GetUsersByFilterQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetUsersByFilterAsync(request.SortName, request.SortOrder, request.PageParameter, request.UserId, request.Active, request.FirstName, request.LastName, request.Email);
            var userlist = _mapper.Map<List<UserViewModel>>(result.Rows);
            var userSerch = new PageResponse<UserViewModel>(userlist, result.Total);
            return new Response<PageResponse<UserViewModel>>(userSerch);
        }

    }

}
