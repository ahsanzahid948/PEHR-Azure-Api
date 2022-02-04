using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PayLoad
    {
        public string Reseller { get; set; }
        public string AppUser { get; set; }
        public string CP { get; set; }

    }
}
