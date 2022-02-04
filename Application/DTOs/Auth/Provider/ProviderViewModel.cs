using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.Provider
{
    public class ProviderViewModel
    {
        public virtual long ProviderId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string MiddleIntial { get; set; }
        public virtual string ProviderNPI { get; set; }
        public virtual string Speciality { get; set; }
        public virtual string TaxonomyCode { get; set; }
        public virtual string SSN { get; set; }
        public virtual string DEANo { get; set; }
        public virtual string Qualification { get; set; }
        public virtual string PhoneNo { get; set; }
        public virtual string FaxNo { get; set; }
        public virtual string Address { get; set; }
        public virtual string DMEPtan { get; set; }
        public virtual string MCRRRPtan { get; set; }
        public virtual string MCRPtan { get; set; }
        public virtual string ProviderSignature { get; set; }
        public virtual long? EntityId { get; set; }
    }
}
