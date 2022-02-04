using Application.DTOs.Common.CreateRequestResponse;
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

namespace Application.Features.Auth.MenuRole.Commands
{
    public class CreateMenuRoleCommand : IRequest<Response<CreateRequestResponse>>
    {
        public virtual string Name { get; set; }
        public virtual string Comments { get; set; }
        public virtual long? EntityId { get; set; }
        public virtual string DefaultEnable { get; set; }
    }

    public class CreateMenuRoleCommandHandler : IRequestHandler<CreateMenuRoleCommand, Response<CreateRequestResponse>>
    {
        private readonly IMenuRoleRepositoryAsync _menuRoleRepositry;
        private readonly IMapper _mapper;
        public CreateMenuRoleCommandHandler(IMenuRoleRepositoryAsync menuRoleRepositry, IMapper mapper)
        {
            _menuRoleRepositry = menuRoleRepositry;
            _mapper = mapper;
        }

        public async Task<Response<CreateRequestResponse>> Handle(CreateMenuRoleCommand request, CancellationToken cancellationToken)
        {
            var menuRole = _mapper.Map<Domain.Entities.Auth.MenuRole>(request);
            var userStatus = await _menuRoleRepositry.AddAsync(menuRole).ConfigureAwait(false);
            return new Response<CreateRequestResponse>(userStatus);
        }
    }
}