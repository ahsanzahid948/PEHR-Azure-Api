namespace Application.DTOs.Authentication
{
    public class AuthenticationResponse
    {
        
        public long UserId { get; set; }
        public virtual string Email { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual long EntityId { get; set; }
        public virtual string Active { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}