using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain;
using MediatR;
using Application.DTOs.ApiUser;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Users.Commands
{
    public class UpdateApiUserCommand : IRequest<Response<int>>
    {
        [Required]
        public virtual long PatientId { get; set; }
        public virtual string PatAllowAccess { get; set; }
        public virtual string Password { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual string Gender { get; set; }
    }

    public class UpdateApiUserCommandHandler : IRequestHandler<UpdateApiUserCommand, Response<int>>
    {
        private readonly IApiUserRepositoryAsync _apiUserRepository;
        private readonly IMapper _mapper;


        public UpdateApiUserCommandHandler(IApiUserRepositoryAsync apiUserRepository, IMapper mapper)
        {
            _apiUserRepository = apiUserRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateApiUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _apiUserRepository.GetByIdAsync(request.PatientId).ConfigureAwait(false);
            if (user != null)
            {
                user.Pat_Allow_Access = request.PatAllowAccess;
                if (!string.IsNullOrEmpty(request.Password))
                {
                    user.Password = request.Password;
                }
                
                user.First_Name = string.IsNullOrEmpty(request.FirstName) ? user.First_Name : request.FirstName;
                user.Last_Name = string.IsNullOrEmpty(request.LastName) ? user.Last_Name : request.LastName;
                user.Dob = request.DateOfBirth.Date > DateTime.MinValue.Date ? request.DateOfBirth : user.Dob;
                user.Gender = string.IsNullOrEmpty(request.Gender) ? user.Gender : request.Gender;
                var userUpdateStatus = await _apiUserRepository.UpdateAsync(user).ConfigureAwait(false);
                return new Response<int>(userUpdateStatus);
            }
            else
            {
                throw new Exception("Patient doesnot exist");
            }

        }
    }
}