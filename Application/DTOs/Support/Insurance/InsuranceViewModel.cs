using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Support.Insurance
{
    public class InsuranceViewModel
    {
        public long InsuranceId { get; set; }
        public string InsuranceName { get; set; }
        public string PayorId { get; set; }
        public long? EntityId { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string Plantype { get; set; }
        public string Comments { get; set; }
        public string BcBsFlag { get; set; }
        public string Medicare { get; set; }
        public string MedicaId { get; set; }
        public string Commercial { get; set; }
        public string Other { get; set; }
    }
}
