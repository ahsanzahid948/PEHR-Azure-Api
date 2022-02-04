using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.UserSetting
{
    public class UserSettingVewModel
    {
        public virtual long SettingId { get; set; }
        public virtual long? MenuRoleId { get; set; }
        public virtual long? EmergencyRoleId { get; set; }
        public virtual string Admin { get; set; }
        public virtual long IdleTimeOut { get; set; }
        public virtual string RXAlertSevere { get; set; }
        public virtual string RXAlertMajor { get; set; }
        public virtual string RXAlertModerate { get; set; }
        public virtual string RXAlertMinor { get; set; }
        public virtual string AlertLifeStyle { get; set; }
        public virtual string AlertAllergy { get; set; }
        public virtual string PastMedToNote { get; set; }
        public virtual double? BillingTop { get; set; }
        public virtual double? BillingBottom { get; set; }
        public virtual double? BillingLeft { get; set; }
        public virtual double? BillingRight { get; set; }
        public virtual double? BillingPatientAddressTop { get; set; }
        public virtual double? BillingPatientAddressLeft { get; set; }
        public virtual double? BillingLocationAddressTop { get; set; }
        public virtual double? BillingLocationAddressLeft { get; set; }
        public virtual double? BillingMessageTop { get; set; }
        public virtual double? BillingMessageLeft { get; set; }
        public virtual string FaxSenderName { get; set; }
        public virtual long? FaxSenderPhoneNo { get; set; }
        public virtual long? FaxSenderFaxNo { get; set; }
        public virtual string EmergencyAcquired { get; set; }
        public virtual double? ScanPageSize { get; set; }
        public virtual double? ScanCropImageLeft { get; set; }
        public virtual double? ScanCropImageRight { get; set; }
        public virtual double? ScanCropImageTop { get; set; }
        public virtual double? ScanCropImageBottom { get; set; }
        public virtual double? ScanImageQuality { get; set; }
        public virtual double? ScanUploadType { get; set; }
        public virtual double? ShowLastName { get; set; }
        public virtual double? ShowFirstName { get; set; }
        public virtual double? Duplex { get; set; }
        public virtual string ShowSetupProfiles { get; set; }
        public virtual double? BillingHcfaTop { get; set; }
        public virtual double? BillingHcfaRight { get; set; }
        public virtual double? BillingHcfaLeft { get; set; }
        public virtual double? BillingHcfaBottom { get; set; }
        public virtual double? BillingHcfaAddressTop { get; set; }
        public virtual double? BillingHcfaAddressLeft { get; set; }
        public virtual string VisitHeadingCollapsed { get; set; }
        public virtual double? VisitTop { get; set; }
        public virtual double? VisitRight { get; set; }
        public virtual double? VisitLeft { get; set; }
        public virtual double? VisitBottom { get; set; }
        public virtual double? DefaultVisitType { get; set; }
        public virtual double? BatchNum { get; set; }


    }
}
