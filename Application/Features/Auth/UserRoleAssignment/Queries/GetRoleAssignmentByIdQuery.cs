using Application.DTOs.Auth.UserButton;
using Application.DTOs.Auth.UserMenu;
using Application.DTOs.Auth.UserRoleAssignment;
using Application.DTOs.Auth.UserTab;
using Application.Interfaces.Repositories.Auth;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Auth.UserRoleAssignment.Queries
{
    public class GetRoleAssignmentByIdQuery : IRequest<Response<UserRoleAssignmentViewModel>>
    {
        [Required]
        public string RoleId { get; set; }
        public string AssignmentIds { get; set; }
        
    }

    public class GetRoleAssignmentByIdQueryHandler : IRequestHandler<GetRoleAssignmentByIdQuery, Response<UserRoleAssignmentViewModel>>
    {
        private readonly IMenuRoleRepositoryAsync _menuRoleRepositry;
        private readonly IMapper _mapper;
        public GetRoleAssignmentByIdQueryHandler(IMenuRoleRepositoryAsync menuRoleRepositry, IMapper mapper)
        {
            _menuRoleRepositry = menuRoleRepositry;
            _mapper = mapper;
        }


        public async Task<Response<UserRoleAssignmentViewModel>> Handle(GetRoleAssignmentByIdQuery request, CancellationToken cancellationToken)
        {
            List<long> seqNums = null;
            if (!string.IsNullOrEmpty(request.AssignmentIds))
            {
                seqNums = request.AssignmentIds.Split(',').Select(long.Parse).ToList();
            }
            var menuTabsList = await _menuRoleRepositry.GetUserTabsByIdsAsync(request.RoleId, seqNums);
            var menuTabsModel = _mapper.Map<IReadOnlyList<UserTabViewModel>>(menuTabsList);

            var menuList = await _menuRoleRepositry.GetUserMenusByIdsAsync(request.RoleId, seqNums);
            var menuModel = _mapper.Map< IReadOnlyList<UserMenuViewModel>>(menuList);

            var menuButtonList = await _menuRoleRepositry.GetUserButtonsByIdsAsync(request.RoleId, seqNums);
            var menuButtonModel = _mapper.Map< IReadOnlyList<UserButtonViewModel>>(menuButtonList);

            var roelAssignment = new UserRoleAssignmentViewModel(menuTabsModel, menuModel, menuButtonModel);

            return new Response<UserRoleAssignmentViewModel>(roelAssignment);
        }
    }
}