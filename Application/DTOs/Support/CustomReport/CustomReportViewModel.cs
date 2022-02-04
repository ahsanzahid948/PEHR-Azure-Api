using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Support.CustomReports
{
    public class CustomReportViewModel
    {
        public virtual long ReportId { get; set; }
        public virtual long? EntityId { get; set; }
        public virtual string ReportDescription { get; set; }
        public virtual string ReportName { get; set; }
        public virtual string EnteredBy { get; set; }
        public virtual DateTime? EntryDate { get; set; }
        public virtual string TelerikReportId { get; set; }
        public virtual long? SortOrder { get; set; }
        public virtual string CustomView { get; set; }

    }
}
