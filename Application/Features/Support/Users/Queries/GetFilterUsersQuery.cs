using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Support
{
    public class GetFilterUsersQuery : IRequest<Response<UserInfoViewModel>>
    {
        public string Fname { get; set; }
        public string LName { get; set; }
        public string Type { get; set; }
        public string Email { get; set; }
        public string Active { get; set; }
        public string AccountActivated { get; set; }

        public GetFilterUsersQuery(string fname, string lname, string email, string type, string active, string activeaccount)
        {
            Fname = fname;
            LName = lname;
            Type = type;
            Email = email;
            Active = active;
            AccountActivated = activeaccount;
        }
    }
    public class GetSupportUsersHandler : IRequestHandler<GetFilterUsersQuery, Response<UserInfoViewModel>>
    {
        private readonly ISupportUserRepositoryAsync _supportUserRepo;
        private readonly IMapper _mapper;
        public GetSupportUsersHandler(ISupportUserRepositoryAsync supportRepo, IMapper map)
        {
            _supportUserRepo = supportRepo;
            _mapper = map;
        }
        public async Task<Response<UserInfoViewModel>> Handle(GetFilterUsersQuery request, CancellationToken cancellationToken)
        {
            var response = await _supportUserRepo.GetFilteredUsersAsync(request.Fname, request.LName, request.Email, request.Type, request.Active, request.AccountActivated);
            return new Response<UserInfoViewModel>(_mapper.Map<UserInfoViewModel>(response));
        }
    }

}
