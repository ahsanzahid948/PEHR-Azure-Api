using Application.DTOs.Auth.UserButton;
using Application.DTOs.Auth.UserMenu;
using Application.DTOs.Auth.UserTab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.UserRoleAssignment
{
    public class UserRoleAssignmentViewModel
    {
        public virtual IReadOnlyList<UserTabViewModel> UserTabs { get; set; }
        public virtual IReadOnlyList<UserMenuViewModel> UserMenus { get; set; }
        public virtual IReadOnlyList<UserButtonViewModel> UserButtons { get; set; }

        public UserRoleAssignmentViewModel(IReadOnlyList<UserTabViewModel> userTabs, IReadOnlyList<UserMenuViewModel> userMenus, IReadOnlyList<UserButtonViewModel> userButtons)
        {
            this.UserTabs = userTabs;
            this.UserMenus = userMenus;
            this.UserButtons = userButtons;
        }
    }
}
