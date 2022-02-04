using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.DataExportSetting
{
    public class DataExportSettingViewModel
    {
        public virtual long UserSettingId { get; set; }
        public virtual DateTime? DateFrom { get; set; }
        public virtual DateTime? DateTo { get; set; }
        public virtual string ExportType { get; set; }
        public virtual string RelativeExportType { get; set; }
        public virtual long? RelativeExportValue { get; set; }
        public virtual DateTime? SpecificDate { get; set; }
        public virtual string ExportTime { get; set; }
        public virtual DateTime? EntryDate { get; set; }
        public virtual string AccountNum { get; set; }
        public virtual string AllPatient { get; set; }
        public virtual string Status { get; set; }
        public virtual long? UserId { get; set; }
        public virtual long? AuthEntityId { get; set; }
    }
}
