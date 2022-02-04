namespace Application.DTOs
{
    using Application.DTOs.Ticket;
    using System.Collections.Generic;

    public class AssignTicketViewModel
    {
        public List<UserInfoViewModel> DsLogin { get; set; }
        public List<TicketTeamInfoViewModel> DsTeam { get; set; }
    }
}
