using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.Client
{

    public class ClientFilter : PayLoad
    {
        public string TxtPracticeName { get; set; }
        public string TxtPracticeEmail { get; set; }
        public string DdlSpeciality { get; set; }
        public string DdlAccountType { get; set; }
        public string TxtSalesPerson { get; set; }
        public string TxtCustomerAccountNum { get; set; }
        public string DdlChannelPartner { get; set; }
        public string DdlAlert { get; set; }
        public string Page { get; set; }
        public string Rp { get; set; }
        public string Sortname { get; set; }
        public string Sortorder { get; set; }
    }
}
