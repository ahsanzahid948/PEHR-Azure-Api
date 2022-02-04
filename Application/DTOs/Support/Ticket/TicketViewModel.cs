using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Ticket
{
    public class TicketViewModel
    {
        public virtual string AccountType { get; set; }
        public virtual string ActiveFlag { get; set; }
        public virtual string Assignee { get; set; }
        public virtual string AssigneeShortName { get; set; }
        public virtual string AssigneeTeam { get; set; }
        public virtual string AttachedFile { get; set; }
        public virtual string BrowserType { get; set; }
        public virtual string Category { get; set; }
        public virtual string ChannelPartner { get; set; }
        public virtual string Comments { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual string Description { get; set; }
        public virtual string DownloadSpeed { get; set; }
        public virtual long EntityId { get; set; }
        public virtual string SuspendedFlag { get; set; }
        public virtual string ImpManagerShortName { get; set; }
        public virtual string ImplementationContact { get; set; }
        public virtual string ImplementationCoordinator { get; set; }
        public virtual string ImplementationManager { get; set; }
        public virtual string InternalAssignee { get; set; }
        public virtual string InternalAssigneeShortName { get; set; }
        public virtual string LastUpdateby { get; set; }
        public virtual DateTime LastUpdatedate { get; set; }
        public virtual string PracticeName { get; set; }
        public virtual string Priority { get; set; }
        public virtual string ProductType { get; set; }
        public virtual string Reporter { get; set; }
        public virtual string ReporterShortName { get; set; }
        public virtual string Resolution { get; set; }
        public virtual string ResolvedDays { get; set; }
        public virtual long TicketId { get; set; }
        public virtual string ShortName { get; set; }
        public virtual string Status { get; set; }
        public virtual long SupTicketTeamInfoSeqNum { get; set; }
        public virtual DateTime TargetCompletionDate { get; set; }
        [MaxLength(1)]
        public virtual string TaskType { get; set; }
        public virtual string TicketCategory { get; set; }
        public virtual string TicketNo { get; set; }
        public virtual string TicketTitle { get; set; }
        public virtual string TicketType { get; set; }
        public virtual string TickleDate { get; set; }
        public virtual string UploadSpeed { get; set; }
    }
}
