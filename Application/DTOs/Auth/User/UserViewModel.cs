using System;

namespace Application.DTOs.User
{
    public class UserViewModel
    {
        public virtual long UserId { get; set; }
        public virtual string UserName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual long EntityId { get; set; }
        public virtual string Active { get; set; }
        public virtual string Admin { get; set; }
        public virtual long? MenuRoleId { get; set; }
        public virtual long? EmergencyRoleId { get; set; }
        public virtual string AllowDelete { get; set; }
        public virtual long? ProviderId { get; set; }
        public virtual string Epcs { get; set; }
        public virtual string ApproveEpcs { get; set; }
        public virtual string EpcsRegistration { get; set; }
        public virtual string Epcs2ndApproval { get; set; }
        public virtual string AllowEpcs { get; set; }
        public virtual string MiddleInitial { get; set; }
        public virtual string ShowSetupProfiles { get; set; }
        public virtual string CpUser { get; set; }
        public virtual string AllowMultipleSessions { get; set; }
        public virtual string PMPNarx { get; set; }
        public virtual string PMPRole { get; set; }
        public virtual string Password { get; set; }
        public virtual string QuicktourDontshow { get; set; }
        public virtual string Token { get; set; }
        public virtual string EtokenSubscription { get; set; }
        public virtual string ActivationCode { get; set; }
        public virtual DateTime? ProofingExpiryDate { get; set; }
        public virtual string Licensekey { get; set; }
        public virtual string TokenRef { get; set; }
        public virtual string EPCSProofType { get; set; }
        public virtual string IsShowSignedoffMessage { get; set; }
        public virtual string AuthToken { get; set; }
        public virtual string AllowPatTrackerBoard { get; set; }
        public virtual string RCMType { get; set; }
        public virtual string ClientSecret { get; set; }
        public virtual string SuperUser { get; set; }
        public virtual string InternalNPI { get; set; }
        public virtual string ViewInvoices { get; set; }
        public virtual double? ShowLastName { get; set; }
        public virtual double? ShowFirstName { get; set; }

    }
}