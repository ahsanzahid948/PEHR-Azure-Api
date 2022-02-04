using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.Practice
{
    public class PracticeViewModel
    {
        public virtual long PracticeId { get; set; }
        public virtual string PracticeName { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual string ZipCodeExt { get; set; }
        public virtual string Speciality { get; set; }
        public virtual string TaxId { get; set; }
        public virtual string GroupNpi { get; set; }
        public virtual string PhoneNo { get; set; }
        public virtual string ProvidedFaxNo { get; set; }
        public virtual string ProductType { get; set; }
        public virtual long? NoOfProviders { get; set; }
        public virtual string PreEhrVendor { get; set; }
        public virtual string ImpConactName { get; set; }
        public virtual string ImpConactPhone { get; set; }
        public virtual string ImpConactEmail { get; set; }
        public virtual string Migration { get; set; }
        public virtual string EmailApptReminder { get; set; }
        public virtual string CallSmsApptReminder { get; set; }
        public virtual string DirectMail { get; set; }
        public virtual string ElectronicPrescription { get; set; }
        public virtual string ElectPatStatmentService { get; set; }
        public virtual string LabInegration { get; set; }
        public virtual string EpcsSetUp { get; set; }
        public virtual string Telemedicine { get; set; }
        public virtual string CompanyLogo { get; set; }
        public virtual string FEESchedule { get; set; }
        public virtual string PaperEncounterSuperBill { get; set; }
        public virtual string KioskCheckin { get; set; }
        public virtual string PrepopulatePatient { get; set; }
        public virtual string GroupMCRPTAN { get; set; }
        public virtual string GroupMCRDME { get; set; }
        public virtual string GroupMCRRR { get; set; }
        public virtual long? EntityId { get; set; }
        public virtual string Comments { get; set; }
        public virtual string MedcaidProvider { get; set; }
        public virtual string BCBSProvider { get; set; }
        public virtual string ProviderType { get; set; }
        public virtual string EHRNotified { get; set; }
        public virtual string InsuranceRequired { get; set; }
    }
}
