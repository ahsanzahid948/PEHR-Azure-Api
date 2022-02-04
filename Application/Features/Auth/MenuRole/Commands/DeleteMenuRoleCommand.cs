using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Auth;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities.Auth.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Auth.MenuRole.Commands
{
    public class DeleteMenuRoleCommand : IRequest<Response<MenuRoleDelete_Vm>>
    {
        public long MenuRoleId { get; set; }
        public string EmergencyRoleId { get; set; }
        public DeleteMenuRoleCommand(long menuRoleId, string emergencyRoleId)
        {
            this.MenuRoleId = menuRoleId;
            this.EmergencyRoleId = emergencyRoleId;
        }
    }

    public class DeleteMenuRoleCommandHandler : IRequestHandler<DeleteMenuRoleCommand, Response<MenuRoleDelete_Vm>>
    {
        private readonly IMenuRoleRepositoryAsync _menuRoleRepositry;
        private readonly IUserRepositoryAsync _userRepositoryAsync;
        private readonly IMapper _mapper;
        public DeleteMenuRoleCommandHandler(IMenuRoleRepositoryAsync menuRoleRepositry, IMapper mapper, IUserRepositoryAsync userRepositoryAsync)
        {
            _menuRoleRepositry = menuRoleRepositry;
            _mapper = mapper;
            _userRepositoryAsync = userRepositoryAsync;
        }

        public async Task<Response<MenuRoleDelete_Vm>> Handle(DeleteMenuRoleCommand request, CancellationToken cancellationToken)
        {
            var menuRoleAssigned = await _userRepositoryAsync.GetUserbyMenuRoleAsync(request.MenuRoleId.ToString(), request.EmergencyRoleId);
            MenuRoleDelete_Vm deleteRsponse = new MenuRoleDelete_Vm();
            deleteRsponse.Status = 0;
            if (menuRoleAssigned != null && menuRoleAssigned.Count == 0)
            {
                await _menuRoleRepositry.DeleteRolePrivilegesAsync(request.MenuRoleId).ConfigureAwait(false);
                deleteRsponse.Status = await _menuRoleRepositry.DeleteAsync(request.MenuRoleId).ConfigureAwait(false);
            }
            else
            {
                deleteRsponse.MenuRoleAssigned = true;
            }
            return new Response<MenuRoleDelete_Vm>(deleteRsponse);

        }
    }
}