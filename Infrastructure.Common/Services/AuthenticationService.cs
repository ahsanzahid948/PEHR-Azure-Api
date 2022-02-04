using Application.DTOs.Authentication;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Wrappers;
using Domain.Entities.Auth;
using Domain.Common;
using FluentValidation.Results;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepositoryAsync _UserRepository;
        private readonly IKioskRepositoryAsync _KioskRepository;
        private readonly IUserTokenRepositoryAsync _UserTokenRepository;
        private readonly IPasswordService _PasswordService;
        private readonly JwtConfig _jwtSettings;

        public AuthenticationService(IUserRepositoryAsync userRepository, IKioskRepositoryAsync kioskRepository, IOptions<JwtConfig> jwtSettings, IPasswordService passwordService, IUserTokenRepositoryAsync userTokenRepository)
        {
            _UserRepository = userRepository;
            _KioskRepository = kioskRepository;
            _jwtSettings = jwtSettings.Value;
            _PasswordService = passwordService;
            _UserTokenRepository = userTokenRepository;
            
        }

        public virtual async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            IList<ValidationFailure> errorMessages = new List<ValidationFailure>();

            var user = await _UserRepository.GetByFilterAsync($"{request.Email.ToLower()}").ConfigureAwait(false);

            if (user == null)
            {
                errorMessages.Add(new ValidationFailure("Email", $"No user is registered with {request.Email}."));
            }

            if (user != null)
            {
                var isPasswordValid = this._PasswordService.VerifyPassword(request.Password, user.Password);
                if (!isPasswordValid)
                {
                    errorMessages.Add(new ValidationFailure("Password", $"Password is Incorrect."));
                }
            }

            if (errorMessages.Count > 0) throw new ValidationException(errorMessages);

            
            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
            var JwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            var userToken = GenerateRefreshToken(ipAddress);
            userToken.Token = JwtToken;
            userToken.User_Seq_Num = user.Seq_Num;

            user.UserTokens.Add(userToken);
            await _UserTokenRepository.AddAsync(userToken).ConfigureAwait(false);

            AuthenticationResponse response = new AuthenticationResponse()
            {
                UserId = user.Seq_Num,
                Email = user.Email,
                FirstName = user.First_Name,
                LastName = user.Last_Name,
                EntityId = user.Default_Entity_Seq_Num,
                Active = user.Active_Flag,
                Token = JwtToken,
                RefreshToken = userToken.Token,
            };

            return new Response<AuthenticationResponse>(response, $"Authenticated {user.User_Name}");
        }

        public virtual async Task<Response<KioskAuthenticatonResponse>> AuthenticateKioskAsync(AuthenticationRequest request, string ipAddress)
        {
            IList<ValidationFailure> errorMessages = new List<ValidationFailure>();

            Kiosk kiosUser = await _KioskRepository.GetByFilterAsync($"{request.Email.ToLower()}").ConfigureAwait(false);

            if (kiosUser == null)
            {
                errorMessages.Add(new ValidationFailure("Email", $"No user is registered with {request.Email}."));
            }

            if (kiosUser != null)
            {
                var isPasswordValid = this._PasswordService.VerifyPassword(request.Password, kiosUser.Password);
                if (!isPasswordValid)
                {
                    errorMessages.Add(new ValidationFailure("Password", $"Password is Incorrect."));
                }
            }

            if (errorMessages.Count > 0) throw new ValidationException(errorMessages);


            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(kiosUser);
            var JwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            var userToken = GenerateRefreshToken(ipAddress);
            userToken.Token = JwtToken;
            userToken.User_Seq_Num = kiosUser.Seq_Num;
            kiosUser.UserTokens = new List<UserToken>();
            kiosUser.UserTokens.Add(userToken);
            await _UserTokenRepository.AddAsync(userToken).ConfigureAwait(false);

            KioskAuthenticatonResponse response = new KioskAuthenticatonResponse()
            {
                UserId = kiosUser.Seq_Num,
                Email = kiosUser.Email,
                EntityId = kiosUser.Entity_Seq_Num,
                Active = kiosUser.Active,
                PortalUrl = kiosUser.Portal_Url,
                TemplateSeqNum= kiosUser.Template_Seq_Num,
                StripePrivateKey = kiosUser.Stripe_Private_Key,
                StripePublicKey = kiosUser.Stripe_Public_Key,
                KioskOptions = kiosUser.Kiosk_Options,
                KioskApptSearch = kiosUser.Kiosk_Appt_Search,
                KioskUrl = kiosUser.Kiosk_Url,
                Token = JwtToken,
                RefreshToken = userToken.Token,
            };

            return new Response<KioskAuthenticatonResponse>(response);
        }


        public virtual async Task<Response<AuthenticationResponse>> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress)
        {
            IList<ValidationFailure> errorMessages = new List<ValidationFailure>();

            var userToken = await _UserTokenRepository.GetByFilterAsync(request.Token).ConfigureAwait(false);

            if (userToken == null)
            {
                errorMessages.Add(new ValidationFailure("token", $"refresh token does not exist."));

            }

            if (userToken != null && !userToken.IsActive)
            {
                errorMessages.Add(new ValidationFailure("token", $"refresh token has expired."));
            }

            if (errorMessages.Count > 0) throw new ValidationException(errorMessages);
            var user = await _UserRepository.GetByFilterAsync(userToken.User_Seq_Num.ToString()).ConfigureAwait(false);

            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
            var JwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            var userTokenNew = GenerateRefreshToken(ipAddress);
            userTokenNew.Token = JwtToken;
            userTokenNew.User_Seq_Num = user.Seq_Num;
            
            user.UserTokens.Add(userTokenNew);

            await _UserRepository.UpdateAsync(user).ConfigureAwait(false);

            AuthenticationResponse response = new AuthenticationResponse()
            {
                UserId = user.Seq_Num,
                Token = JwtToken,
                Email = user.Email,
                Active = user.Active_Flag,
                RefreshToken = userTokenNew.Token,
            };

            return new Response<AuthenticationResponse>(response, $"Refresh Token Successful.");
        }
        public virtual async Task<Response<KioskAuthenticatonResponse>> RefreshKioskTokenAsync(RefreshTokenRequest request, string ipAddress)
        {
            IList<ValidationFailure> errorMessages = new List<ValidationFailure>();

            var userToken = await _UserTokenRepository.GetByFilterAsync(request.Token).ConfigureAwait(false);

            if (userToken == null)
            {
                errorMessages.Add(new ValidationFailure("token", $"refresh token does not exist."));

            }

            if (userToken != null && !userToken.IsActive)
            {
                errorMessages.Add(new ValidationFailure("token", $"refresh token has expired."));
            }

            if (errorMessages.Count > 0) throw new ValidationException(errorMessages);
            Kiosk kiosUser = await _KioskRepository.GetByFilterAsync(userToken.User_Seq_Num.ToString()).ConfigureAwait(false);

            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(kiosUser);
            var JwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            var userTokenNew = GenerateRefreshToken(ipAddress);
            userTokenNew.Token = JwtToken;
            userTokenNew.User_Seq_Num = kiosUser.Seq_Num;

            kiosUser.UserTokens.Add(userTokenNew);

            await _UserRepository.UpdateAsync(kiosUser).ConfigureAwait(false);

            KioskAuthenticatonResponse response = new KioskAuthenticatonResponse()
            {
                UserId = kiosUser.Seq_Num,
                Token = JwtToken,
                Email = kiosUser.Email,
                Active = kiosUser.Active,
                RefreshToken = userTokenNew.Token,
            };

            return new Response<KioskAuthenticatonResponse>(response, $"Refresh Token Successful.");
        }

        
        private Task<JwtSecurityToken> GenerateJWToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.User_Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Seq_Num.ToString()),
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return Task.FromResult(jwtSecurityToken);
        }
        private Task<JwtSecurityToken> GenerateJWToken(Kiosk user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Seq_Num.ToString()),
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return Task.FromResult(jwtSecurityToken);
        }

        private UserToken GenerateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new UserToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expiry_Date = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenLifetimeInMin),
                    Created_Date = DateTime.UtcNow
                };
            }
        }
    }
}