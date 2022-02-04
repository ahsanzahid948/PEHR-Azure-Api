using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ClientDetail_VM
    {
        public List<ClientProfile> IManager { get; set; }
        public List<ClientProfile> SManager { get; set; }
        public List<ClientsList> ClientNumbers { get; set; }


    }
}
