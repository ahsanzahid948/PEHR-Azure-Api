using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Support
{
    public class Insurance
    {
        public long Seq_Num { get; set; }
        public string Insurance_Name { get; set; }
        public string Payor_Id { get; set; }
        public long? Entitty_Seq_Num { get; set; }
        public string Short_Name { get; set; }
        public string Description { get; set; }
        public string PlanType { get; set; }
        public string Comments { get; set; }
        public string Bc_Bs_Flag { get; set; }
        public string Medicare_Flag { get; set; }
        public string Medicaid_Flag { get; set; }
        public string Commercial_Flag { get; set; }
        public string Other { get; set; }
    }
}
