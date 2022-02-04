using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.MenuButtons
{
    public class MenuButtonViewModel
    {
        public virtual long ButtonId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Title { get; set; }
        public virtual string Icon { get; set; }
        public virtual long? SortOrder { get; set; }
        public virtual long? TabMenuId { get; set; }
        public virtual long? MenuId { get; set; }
        public virtual string Heading { get; set; }
        public virtual string CategoryType { get; set; }
    }
}
