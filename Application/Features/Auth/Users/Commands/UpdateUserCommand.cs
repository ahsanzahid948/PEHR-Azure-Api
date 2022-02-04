using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Users.Commands
{
    public class UpdateUserCommand : IRequest<Response<int>>
    {
        [Required]
        public string Email { get; set; }
        public  string AllowEpcs { get; set; }
        public  string Epcs2ndApproval { get; set; }
        public  string AllowMultipleSessions { get; set; }
        public  string PMPNarx { get; set; }
        public  string PMPRole { get; set; }
        public  string Licensekey { get; set; }
        public  string TokenRef { get; set; }
        public  string EpcsRegistration { get; set; }
        public  string DefaultTokenType { get; set; }
        public string EmergencyAcquired { get; set; }
        public long? DefaultEntityId { get; set; } = null;
        public string Quicktour { get; set; }
        public string Token { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Response<int>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;


        public UpdateUserCommandHandler(IUserRepositoryAsync userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByFilterAsync(request.Email.ToLower()).ConfigureAwait(false);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(request.AllowEpcs))
                    user.Allow_EPCS = request.AllowEpcs;

                if (!string.IsNullOrEmpty(request.AllowMultipleSessions))
                    user.Allow_Multiple_Sessions = request.AllowMultipleSessions;

                if (!string.IsNullOrEmpty(request.PMPNarx))
                    user.Pmp_Narx = request.PMPNarx;

                if (!string.IsNullOrEmpty(request.PMPRole))
                    user.Pmp_Role = request.PMPRole;

                if (!string.IsNullOrEmpty(request.Epcs2ndApproval))
                    user.EPCS_2nd_Approval = request.Epcs2ndApproval;

                if (!string.IsNullOrEmpty(request.Licensekey))
                    user.Licensekey = request.Licensekey;

                if (!string.IsNullOrEmpty(request.TokenRef))
                    user.Token_Ref = request.TokenRef;

                if (!string.IsNullOrEmpty(request.EpcsRegistration))
                    user.EPCS_Registration = request.EpcsRegistration;

                if (!string.IsNullOrEmpty(request.DefaultTokenType))
                    user.Default_Token_Type = request.DefaultTokenType;

                if (!string.IsNullOrEmpty(request.EmergencyAcquired))
                    user.Is_Emergency_Acquired = request.EmergencyAcquired;

                if(request.DefaultEntityId != null)
                    user.Default_Entity_Seq_Num = (long)request.DefaultEntityId;

                if (!string.IsNullOrEmpty(request.Quicktour))
                    user.Quicktour_Dontshow = request.Quicktour;

                if (!string.IsNullOrEmpty(request.Token))
                    user.Token = request.Token;

                var userUpdateStatus = await _userRepository.UpdateAsync(user).ConfigureAwait(false);
                return new Response<int>(userUpdateStatus);
            }
            else
            {
                return new Response<int>(0);
            }
        }
    }
}