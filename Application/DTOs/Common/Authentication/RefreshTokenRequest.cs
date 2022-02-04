using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Authentication
{
    public class RefreshTokenRequest
    {
        [Required(ErrorMessage = "Token is Required")]
        public virtual string Token { get; set; }
    }
}