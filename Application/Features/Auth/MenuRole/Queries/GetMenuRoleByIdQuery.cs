using Application.DTOs.Auth.MenuRole;
using Application.DTOs.Common.PagingResponse;
using Application.Interfaces.Repositories.Auth;
using Application.Parameters;
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

namespace Application.Features.Auth.MenuRole.Queries
{
    public class GetMenuRoleByIdQuery : IRequest<Response<PageResponse<MenuRoleViewModel>>>
    {
        [Required]
        public long EntityId { get; set; }
        public string SortName { get; set; }
        public string SortOrder { get; set; }
        public RequestPageParameter PageParameter { get; set; }
    }

    public class GetMenuRoleByIdQueryHandler : IRequestHandler<GetMenuRoleByIdQuery, Response<PageResponse<MenuRoleViewModel>>>
    {
        private readonly IMenuRoleRepositoryAsync _menuRoleRepositry;
        private readonly IMapper _mapper;
        public GetMenuRoleByIdQueryHandler(IMenuRoleRepositoryAsync menuRoleRepositry, IMapper mapper)
        {
            _menuRoleRepositry = menuRoleRepositry;
            _mapper = mapper;
        }

        public async Task<Response<PageResponse<MenuRoleViewModel>>> Handle(GetMenuRoleByIdQuery request, CancellationToken cancellationToken)
        {
            string defaultEnable = "Y";
            var menuRolePageResponse = await _menuRoleRepositry.GetByIdAsync(request.EntityId, defaultEnable, request.SortName, request.SortOrder, request.PageParameter);
            var menuRoleList = _mapper.Map<List<MenuRoleViewModel>>(menuRolePageResponse.Rows);
            return new Response<PageResponse<MenuRoleViewModel>>(new PageResponse<MenuRoleViewModel>(menuRoleList, menuRolePageResponse.Total));
        }
    }
}
