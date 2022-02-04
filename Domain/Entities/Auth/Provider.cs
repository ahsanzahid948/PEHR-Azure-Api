using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth
{
    public class Provider
    {
        public virtual long Seq_Num { get; set; }
        public virtual string First_Name { get; set; }
        public virtual string Last_Name { get; set; }
        public virtual string Middle_Intial { get; set; }
        public virtual string NPI { get; set; }
        public virtual string Speciality { get; set; }
        public virtual string Taxonomy_Code { get; set; }
        public virtual string SSN { get; set; }
        public virtual string DEA_No { get; set; }
        public virtual string Qualification { get; set; }
        public virtual string Phone_No { get; set; }
        public virtual string Fax_No { get; set; }
        public virtual string Address { get; set; }
        public virtual string DME_Ptan { get; set; }
        public virtual string MCR_RR_Ptan { get; set; }
        public virtual string MCR_Ptan { get; set; }
        public virtual string Provider_Signature { get; set; }
        public virtual long? Entitty_Seq_Num { get; set; }
    }
}
