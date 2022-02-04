using System;
using System.Collections.Generic;

namespace Domain.Entities.Auth
{
    public class User
    {
        public User()
        {
            UserTokens = new List<UserToken>();
        }

        public virtual long Seq_Num { get; set; }
        public virtual string Email { get; set; }
        public virtual string User_Name { get; set; }
        public virtual string User_Type { get; set; }
        public virtual string Super_User { get; set; }
        public virtual string Password { get; set; }
        public virtual string Password_Hash { get; set; }
        public virtual string Password_Salt { get; set; }
        public virtual string First_Name { get; set; }
        public virtual string Last_Name { get; set; }

        public virtual long Default_Entity_Seq_Num { get; set; }
        public virtual string Internal_NPI { get; set; }

        public virtual string Approve_EPCS { get; set; }
        public virtual string EPCS_Registration { get; set; }
        public virtual string Licensekey { get; set; }
        public virtual string Token_Ref { get; set; }
        public virtual string Default_Token_Type { get; set; }

        public virtual string Active_Flag { get; set; }
        public virtual string FULL_ACCESS { get; set; }
        public virtual string Secret_Key { get; set; }
        public virtual IList<UserToken> UserTokens { get; set; }
        
        public virtual string Admin_Flag { get; set; }
        public virtual string Entered_By { get; set; }
        public virtual DateTime Entry_Date { get; set; }
        public virtual long? Menu_Role_Seq_Num { get; set; }
        public virtual long? Emergency_Role_Seq_Num { get; set; }
        public virtual string Allow_Delete { get; set; }
        public virtual long? Idle_Time_Out { get; set; }
        public virtual string Rx_Alert_Severe { get; set; }
        public virtual string Rx_Alert_Major { get; set; }
        public virtual string Rx_Alert_Moderate { get; set; }
        public virtual string Rx_Alert_Minor { get; set; }
        public virtual string Alert_Life_Style { get; set; }
        public virtual string Alert_Allergy { get; set; }
        public virtual string Past_Med_To_Note { get; set; }
        public virtual double? Billing_Top { get; set; }
        public virtual double? Billing_Bottom { get; set; }
        public virtual double? Billing_Left { get; set; }
        public virtual double? Billing_Right { get; set; }
        public virtual double? Billing_Patient_Address_Top { get; set; }
        public virtual double? Billing_Patient_Address_Left { get; set; }
        public virtual double? Billing_Location_Address_Top { get; set; }
        public virtual double? Billing_Location_Address_Left { get; set; }
        public virtual double? Billing_Message_Top { get; set; }
        public virtual double? Billing_Message_Left { get; set; }
        public virtual string Fax_Sender_Name { get; set; }
        public virtual long? Fax_Sender_Phone_No { get; set; }
        public virtual long? Fax_Sender_Fax_No { get; set; }
        public virtual string Is_Emergency_Acquired { get; set; }
        public virtual string Quicktour_Dontshow { get; set; }
        public virtual string Token { get; set; }
        public virtual long? Provider_Seq_Num { get; set; }
        public virtual string EPCS { get; set; }
        public virtual string Etoken_Subscription { get; set; }
        public virtual string EPCS_2nd_Approval { get; set; }
        public virtual string Activation_Code { get; set; }
        public virtual DateTime? Proofing_Expiry_Date { get; set; }
        public virtual string EPCS_Proof_Type { get; set; }
        public virtual string Is_Show_Signedoff_Message { get; set; }
        public virtual string Allow_EPCS { get; set; }
        public virtual double? Scan_Page_Size { get; set; }
        public virtual double? Scan_Crop_Image_Left { get; set; }
        public virtual double? Scan_Crop_Image_Right { get; set; }
        public virtual double? Scan_Crop_Image_Top { get; set; }
        public virtual double? Scan_Crop_Image_Bottom { get; set; }
        public virtual double? Scan_Image_Quality { get; set; }
        public virtual double? Scan_Upload_Type { get; set; }
        public virtual double? Show_Last_Name_Chr { get; set; }
        public virtual double? Show_First_Name_Chr { get; set; }
        public virtual string Middle_Initial { get; set; }
        public virtual double? Duplex { get; set; }
        public virtual string Auth_Token { get; set; }
        public virtual string File_Name { get; set; }
        public virtual string Product_Name { get; set; }
        public virtual string Internal_Comments { get; set; }
        public virtual string Show_setup_Profiles { get; set; }
        public virtual string Allow_Pat_Tracker_Board { get; set; }
        public virtual double? Billing_Hcfa_Top { get; set; }
        public virtual double? Billing_Hcfa_Right { get; set; }
        public virtual double? Billing_Hcfa_Left { get; set; }
        public virtual double? Billing_Hcfa_Bottom { get; set; }
        public virtual double? Billing_Hcfa_Address_Top { get; set; }
        public virtual double? Billing_Hcfa_Address_Left { get; set; }
        public virtual string Visit_Heading_Collapsed { get; set; }
        public virtual double? Visit_Top { get; set; }
        public virtual double? Visit_Right { get; set; }
        public virtual double? Visit_Left { get; set; }
        public virtual double? Visit_Bottom { get; set; }
        public virtual string Cp_User { get; set; }
        public virtual string RCM_Type { get; set; }
        public virtual string Client_Secret { get; set; }
        public virtual string Allow_Multiple_Sessions { get; set; }
        public virtual double? Default_Visit_Type { get; set; }
        public virtual double? Batch_Num { get; set; }
        public virtual string Pmp_Narx { get; set; }
        public virtual string Pmp_Role { get; set; }
        public virtual string Internal_Npi { get; set; }
        public virtual string View_Invoices { get; set; }
        public virtual string ENT_User_Name { get; set; }
        public virtual string ENT_Password { get; set; }
        public virtual string User_Enterprise_Type { get; set; }
        public virtual string ENT_Db_User { get; set; }
        public virtual string ENT_Db_Passowrd { get; set; }
    }
}