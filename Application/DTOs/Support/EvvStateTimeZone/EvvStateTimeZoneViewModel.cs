using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Support.EvvStateTimeZone
{
    public class EvvStateTimeZoneViewModel
    {
        public virtual long EvvStateId { get; set; }
        public virtual string State { get; set; }
        public virtual string TimeZone { get; set; }
        public virtual string Qualify { get; set; }
        public virtual string PlanFormat { get; set; }
        public virtual string ProviderEmailRequired { get; set; }
        public virtual string AppointmentRequired { get; set; }
        public virtual string ProviderSsnRequired { get; set; }
        public virtual string SSSNFormat { get; set; }
        public virtual string ContingencyPlanReview { get; set; }
        public virtual string ContingencyPlan { get; set; }
        public virtual string AuthorizationNumber { get; set; }
        public virtual string EvvTasks { get; set; }
        public virtual string VisitTimeDisplay { get; set; }
        public virtual string ServicesVerified { get; set; }
        public virtual string VisitTimeVerified { get; set; }
        public virtual string EvvAckWin { get; set; }
        public virtual string Signature { get; set; }
        public virtual string VoiceRecording { get; set; }
        public virtual string PlanValidation { get; set; }
        public virtual string TimezoneId { get; set; }
        public virtual string DST { get; set; }
    }
}
