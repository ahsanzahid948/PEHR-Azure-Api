using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Task
{
    public class TaskViewModel
    {
        public long TaskId { get; set; }
        public string TicketNo { get; set; }
        public string TicketType { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string Assignee { get; set; }
        public string Reporter { get; set; }
        public string Comments { get; set; }
        public string AttachedFile { get; set; }
        public string ParentTicket { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public string TicketTitle { get; set; }
        public long EntityId { get; set; }
        public string ShortName { get; set; }
        public string BrowserType { get; set; }
        public string Resolution { get; set; }
        public string DownloadSpeed { get; set; }
        public string UploadSpeed { get; set; }
        public string PracticeSetupListSeqNum { get; set; }
        public string AssigneeShortName { get; set; }
        public string ReporterShortName { get; set; }
        public string Category { get; set; }
        public DateTime TargetCompletionDate { get; set; }
        public string Description { get; set; }
        public string ChannelPartner { get; set; }
        public string AccountType { get; set; }
        public string AssigneeTeam { get; set; }
        public string TeamName { get; set; }
        public string SuspendedFlag { get; set; }
        public string ActiveFlag { get; set; }
        public string SupTicketTeamInfoSeqNum { get; set; }
        public string InternalAssigneeShortname { get; set; }


    }
}
