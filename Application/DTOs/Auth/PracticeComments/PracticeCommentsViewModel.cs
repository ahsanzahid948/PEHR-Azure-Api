using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.PracticeComments
{
    public class PracticeCommentsViewModel
    {
        public virtual long CommentId { get; set; }
        public virtual string Comments { get; set; }
        public virtual string EnteredBy { get; set; }
        public virtual DateTime? EnteredDate { get; set; }
        public virtual long? EntityId { get; set; }
    }
}
