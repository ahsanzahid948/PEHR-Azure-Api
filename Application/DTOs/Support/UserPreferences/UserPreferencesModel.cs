using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class UserPreferencesModel
    {
        public virtual long UserPreferencesId { get; set; }
        public virtual long UserId { get; set; }
        public virtual string Type { get; set; }
        public virtual string NotificationType { get; set; }
        public virtual string EnteredBy { get; set; }
        public virtual DateTime EnteredDate { get; set; }
    }
}
