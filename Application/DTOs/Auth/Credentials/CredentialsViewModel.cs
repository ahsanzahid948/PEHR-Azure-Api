using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.DOCServerCredentials
{
   public class CredentialsViewModel 
    {
        public virtual string AuthEntityId { get; set; }
        public virtual long EntityId { get; set; }
        public virtual string DefaultEntityId { get; set; }
        public virtual string UserName { get; set; }
        public virtual string DbUserName { get; set; }
        public virtual string DbPassword { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual string UserType { get; set; }
        public virtual string TnsName { get; set; }
        public virtual string RedirectUrl { get; set; }
        public virtual long AppUserId { get; set; }
        public virtual long? UserAccessId { get; set; }
        public virtual string MerchantClientId { get; set; }
        public virtual string GatwayId { get; set; }
        public virtual string TransFirst { get; set; }
        public virtual string DocsPath { get; set; }
        public virtual string DocsHostUser { get; set; }
        public virtual string DocsHostPassword { get; set; }
        public virtual string DocsHostName { get; set; }
        public virtual string EMRDocsPath { get; set; }
        public virtual string EMRDocsHostName { get; set; }
        public virtual string EMRDocsHostUser { get; set; }
        public virtual string EMRDocsHostPassword { get; set; }
        public virtual string ArchiveDocsPath { get; set; }
        public virtual string ArchiveDocsHostName { get; set; }
        public virtual string ArchiveDocsHostUser { get; set; }
        public virtual string ArchiveDocsHostPassword  { get; set; }
        public virtual string ProviderFee { get; set; }
        public virtual string FlatFee { get; set; }
        public virtual string KioskActive { get; set; }
        public virtual string WebServiceUrl { get; set; }
        public virtual string EmrUrl { get; set; }
        public virtual string Company { get; set; }
        public virtual string CompanyId { get; set; }
        public virtual string IpadUrl { get; set; }
        public virtual string IpadVersion { get; set; }
        public virtual string SwitchRequestUrl { get; set; }
    }
}
