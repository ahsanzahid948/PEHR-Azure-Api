using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Task
{
    public class SupportTask
    {
        public long Seq_Num { get; set; }
        public string Ticket_No { get; set; }
        public string Ticket_Type { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string Assignee { get; set; }
        public string Reporter { get; set; }
        public string Comments { get; set; }
        public string Attached_File { get; set; }
        public string Parent_Ticket { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Last_Update_Date { get; set; }
        public string Last_Update_By { get; set; }
        public string Ticket_Title { get; set; }
        public long Entity_Seq_Num { get; set; }
        public string Short_Name { get; set; }
        public string Browser_Type { get; set; }
        public string Resolution { get; set; }
        public string Download_Speed { get; set; }
        public string Upload_Speed { get; set; }
        public string Practice_Setup_List_Seq_Num { get; set; }
        public string Assignee_short_Name { get; set; }
        public string Reporter_Short_Name { get; set; }
        public string Category { get; set; }
        public DateTime Target_Completion_Date { get; set; }
        public string Description { get; set; }
        public string Channel_Partner { get; set; }
        public string Account_Type { get; set; }
        public string AssigneeTeam { get; set; }
        public string TeamName { get; set; }
        public string Suspended_Flag { get; set; }
        public string Active_Flag { get; set; }
        public long Sup_Ticket_Team_Info_Seq_Num { get; set; }
        public string Internal_Assignee_Short_Name { get; set; }
    }
}
