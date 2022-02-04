using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SupportTicket 
    {
        public virtual string Account_Type { get; set; }
        public virtual string Active_Flag { get; set; }
        public virtual string Assignee { get; set; }
        public virtual string Assignee_Short_Name { get; set; }
        public virtual string AssigneeTeam { get; set; }
        public virtual string Attached_File { get; set; }
        public virtual string Browser_Type { get; set; }
        public virtual string Category { get; set; }
        public virtual string Channel_Partner { get; set; }
        public virtual string Comments { get; set; }
        public virtual DateTime Created_Date { get; set; }
        public virtual string Description { get; set; }
        public virtual string IsOverDue { get; set; }
        public virtual string Download_Speed { get; set; }
        public virtual long Entity_Seq_Num { get; set; }
        public virtual string Imp_Manager_Short_Name { get; set; }
        public virtual string Implementation_Contact { get; set; }
        public virtual string Implementation_Coordinator { get; set; }
        public virtual string Implementation_Manager { get; set; }
        public virtual string Internal_Assignee { get; set; }
        public virtual string Internal_Assignee_Short_Name { get; set; }
        public virtual string Last_Action_Type { get; set; }
        public virtual string Last_Update_By { get; set; }
        public virtual DateTime Last_Update_Date { get; set; }
        public virtual string Parent_Ticket { get; set; }
        public virtual string Practice_Name { get; set; }
        public virtual long Practice_Setup_List_Seq_Num { get; set; }
        public virtual string Priority { get; set; }
        public virtual string Product_Type { get; set; }
        public virtual string Reporter { get; set; }
        public virtual string Reporter_Short_Name { get; set; }
        public virtual string Resolution { get; set; }
        public virtual string Resolved_Days { get; set; }
        public virtual long Seq_Num { get; set; }
        public virtual string Short_Name { get; set; }
        public virtual string Status { get; set; }
        public virtual long Sup_Ticket_team_Info_Seq_Num { get; set; }
        public virtual string Suspended_Flag { get; set; }
        public virtual DateTime Target_Completion_Date { get; set; }
        public virtual string Task_Type { get; set; }
        public virtual string Team_name { get; set; }
        public virtual string Ticket_Category { get; set; }
        public virtual string Ticket_No { get; set; }
        public virtual string Ticket_Source { get; set; }
        public virtual string Ticket_Title { get; set; }
        public virtual string Ticket_Type { get; set; }
        public virtual DateTime Tickle_Date { get; set; }
        public virtual string Upload_Speed { get; set; }
        public virtual string AppUser { get; set; }
    }
}
