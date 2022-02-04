using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Support
{
    public class EvvStateTimeZone
    {
        public virtual long Seq_Num { get; set; }
        public virtual string State { get; set; }
        public virtual string TimeZone { get; set; }
        public virtual string Qualify { get; set; }
        public virtual string PlanFormat { get; set; }
        public virtual string Provider_Email_Req { get; set; }
        public virtual string Appointment_Req { get; set; }
        public virtual string Provider_Ssn_Req { get; set; }
        public virtual string SSNFormat { get; set; }
        public virtual string Contingency_Plan_Review { get; set; }
        public virtual string Contingency_Plan { get; set; }
        public virtual string Authorization_Number { get; set; }
        public virtual string Evv_Tasks { get; set; }
        public virtual string Visit_Time_Display { get; set; }
        public virtual string Services_Verified { get; set; }
        public virtual string VisitTime_Verified { get; set; }
        public virtual string Evv_Ack_Win { get; set; }
        public virtual string Signature { get; set; }
        public virtual string Voice_Recording { get; set; }
        public virtual string Plan_Validation { get; set; }
        public virtual string Timezone_Id { get; set; }
        public virtual string DST { get; set; }

    }
}
