using Application.DTOs.Authentication;
using Application.Wrappers;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<KioskAuthenticatonResponse>> AuthenticateKioskAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<AuthenticationResponse>> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress);
        Task<Response<KioskAuthenticatonResponse>> RefreshKioskTokenAsync(RefreshTokenRequest request, string ipAddress);
       
    }
}