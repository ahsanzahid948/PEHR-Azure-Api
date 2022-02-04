using Application.Interfaces.Repositories.Auth;
using Dapper;
using Application.DTOs;
using Domain.Entities.Auth;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Application.Exceptions;
using Domain.Entities.Support;
using AutoMapper;
using Application.DTOs.Entity;
using Infrastructure.Common.Services;
using Microsoft.Extensions.Hosting;
using System.IO;
using Application.DTOs.Auth.Client;
using Application.DTOs.Common.CreateRequestResponse;
using Application.Interfaces.Repositories;
using System.Data;

namespace Infrastructure.Repositories
{
    public class PracticeRepositoryAsync : IPracticeRepositoryAsync
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IHostEnvironment _host;

        public PracticeRepositoryAsync(IConfiguration config, IMapper mapper, IHostEnvironment host)
        {
            _configuration = config;
            _mapper = mapper;
            _host = host;
        }



        public Task<IReadOnlyList<Practice>> GetAllAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Practice> GetByFilterAsync(string filterCriteria)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Audit>> GetPracticeAuditData(PayLoad payload, long entitySeqNum)
        {
            var query = "SELECT SEQ_NUM,OLDSTATUS,NEWSTATUS=ENTERED_BY,ENTRYDATE,ENTITY_SEQ_NUM FROM WEB_AUTH.PRACTICESETUP_AUDIT WHERE ENTITY_SEQ_NUM=:ENTITY_SEQ_NUM";
            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var response = await connection.QueryAsync<Audit>(query, new { ENTITY_SEQ_NUM = entitySeqNum });
                return response.ToList();
            }

        }

        public async Task<ClientComments_VM> GetPracticeClientComments(PayLoad payload, long entitySeqNum)
        {
            var query = "SELECT SEQ_NUM,ENTITTY_SEQ_NUM,ENTERED_DATE,ENTERED_BY,COMMENTS FROM WEB_AUTH.PRACTICE_COMMENTS WHERE ENTITTY_SEQ_NUM=:ENTITY_SEQ_NUM ORDER BY ENTERED_DATE DESC";
            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var clientComments = new ClientComments_VM();
                var practiceComments = await connection.QueryAsync<PracticeComments>(query, new { ENTITY_SEQ_NUM = entitySeqNum });
                string practiceSetupStatus = await connection.QueryFirstAsync<string>("SELECT PRACTICE_SETUP from ENTITY WHERE SEQ_NUM=:SEQ_NUM", new { SEQ_NUM = entitySeqNum });
                clientComments.PracticeComments = practiceComments.ToList();
                clientComments.PracticeSetup = practiceSetupStatus;

                return clientComments;
            }

        }

        public async Task<List<Practice>> GetPracticeProfileData(PayLoad payload, long entitySeqNum)
        {
            string query = @"SELECT SEQ_NUM,PRACTICE_NAME,ADDRESS,CITY,STATE,ZIP_CODE,ZIP_CODE_EXT,SPECIALITY,TAX_ID,GROUP_NPI,PHONE_NO,PEHR_PROVIDED_FAX_NO,PRODUCT_TYPE,NO_OF_PROVIDERS,PRE_EHR_VENDOR,IMP_CONACT_NAME,IMP_CONACT_PHONE,IMP_CONACT_EMAIL,DATA_MIGRATION,EMAIL_APPT_REMINDER,CALL_SMS_APPT_REMINDER,DIRECT_EMAIL,ELECTRONIC_PRESCRIPTION,ELECT_PAT_STATMENT_SERVICE,LAB_INEGRATION,EPCS_SET_UP,TELEMEDICINE,COMPANY_LOGO,FEE_SCHEDULE,PAPER_ENCOUNTER_SUPER_BILL,KIOSK_CHECKIN,PREPOPULATE_PATIENT,GROUP_MCR_PTAN,GROUP_MCR_DME,GROUP_MCR_RR,ENTITTY_SEQ_NUM,COMMENTS,MEDCAID_PROVIDER,BCBS_PROVIDER,PROVIDER_TYPE,EHR_NOTIFIED,NO_INSURANCE_NEEDED       
              FROM WEB_AUTH.PRACTICE Where ENTITTY_SEQ_NUM = :ENTITYSEQNUM";

            IList<ValidationFailure> MessagesException = new List<ValidationFailure>();
            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var response = await connection.QueryAsync<Practice>(query, new { ENTITYSEQNUM = entitySeqNum });
                return response.ToList();
            }
            MessagesException.Add(new ValidationFailure("ConnectionString", "PracticeRepositoryAsync->LoadPracticeData()"));
            throw new ValidationException(MessagesException.ToList());
        }

        public async Task<int> SaveClientComments(PayLoad payload, long entitySeqNum, string comment)
        {
            var query = "INSERT INTO WEB_AUTH.PRACTICE_COMMENTS(SEQ_NUM, COMMENTS, ENTERED_BY, ENTERED_DATE, ENTITTY_SEQ_NUM) " +
                       "VALUES (PRIMARY_SEQ.NEXTVAL,:COMMENTS,:ENTERED_BY,SYSDATE,:ENTITY_SEQ_NUM)";
            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                int isInserted = await connection.ExecuteAsync(query, new { COMMENTS = comment, ENTERED_BY = payload.AppUser, ENTITY_SEQ_NUM = payload.AppUser });

                if (isInserted > 0)
                {
                    throw new Exception("Insert Error");
                }
                return isInserted;
            }
        }
        public async Task<List<Provider>> GetProviderData(PayLoad payload, long Seqnum)
        {
            string query = @"SELECT FIRST_NAME,LAST_NAME,NPI,SPECIALITY FROM WEB_AUTH.PROVIDER WHERE ENTITTY_SEQ_NUM=:ENTITY_SEQ_NUM ";

            var response = await DatabaseService.SelectCommandList<Provider>(_configuration, "SupportDBConnection", query, new object[] {
                "ENTITY_SEQ_NUM", Seqnum});

            return response;


        }

        public Task<Provider> GetProviderDetail(PayLoad payload, long authEntitySeqNum, long providerSeqNum)
        {
            string query = "Select FIRST_NAME,MIDDLE_INITIAL,LAST_NAME,NPI,SPECIALITY,TAXONOMY_CODE,SSN,DEA_NO,QUALIFICATION,FAX_NO,PHONE_NO,ADDRESS,MCR_PTAN,MCR_RR_PTAN,DME_PTAN,BCBS_PROVIDER,MEDCAID_PROVIDER,PROVIDER_SIGNATURE from WEB_AUTH.PROVIDER Where ENTITTY_SEQ_NUM=:ENTITY_SEQ_NUM AND SEQ_NUM=:SEQ_NUM";

            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection")))
            {
                var result = connection.QuerySingleAsync<Provider>(query, new { SEQ_NUM = providerSeqNum, ENTITY_SEQ_NUM = authEntitySeqNum });
                return result;
            }

        }
        public async Task<string> UpdateClientEntityProfile(ClientProfileViewModel clientProf, EntityViewModel entity)
        {
            var query = "UPDATE CLIENT_PROF SET IMPLEMENTATION_MANAGER = :IMPLEMENTATION_MANAGER, IMPLEMENTATION_CONTACT = :IMPLEMENTATION_CONTACT, IMPLEMENTATION_COORDINATOR = :IMPLEMENTATION_COORDINATOR, IMPLEMENTATION_COMMENTS = :IMPLEMENTATION_COMMENTS WHERE SEQ_NUM = :SEQ_NUM ";
            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection")))
            {

                var updateProf = await connection.ExecuteAsync(query, new { SEQ_NUM = clientProf.CompanySeqNum, IMPLEMENTATION_MANAGER = clientProf.ImplementationManager, IMPLEMENTATION_CONTACT = clientProf.ImplementationContact, IMPLEMENTATION_COORDINATOR = clientProf.ImplementationCoordinator, IMPLEMENTATION_COMMENTS = clientProf.ImplementationComments });
                if (updateProf > 0)
                {
                    query = "UPDATE ENTITY SET ACCOUNT_TYPE = :ACCOUNT_TYPE, LIVE_DATE = :LIVE_DATE, EXPECTED_LIVE_DATE = :EXPECTED_LIVE_DATE, STAGE = :STAGE, ALERT = :ALERT WHERE SEQ_NUM = :SEQ_NUM ";
                    var updateEntity = await connection.ExecuteAsync(query, new { SEQ_NUM = entity.PracticeSeqNum, ACCOUNT_TYPE = entity.AccountType, LIVE_DATE = entity.LiveDate, EXPECTED_LIVE_DATE = entity.ExpectedLiveDate, ALERT = entity.Alert, STAGE = entity.Stage });

                    if (updateEntity > 0)
                    {
                        return "Updated Successfully";
                    }
                }
                return "Updated Successfully";
            }

        }
        public async Task<int> UpdateAsync(Practice entity)
        {
            var sql = "UPDATE WEB_AUTH.PRACTICE SET PRACTICE_NAME=:PRACTICE_NAME,ADDRESS=:ADDRESS,CITY=:CITY,STATE=:STATE,ZIP_CODE=:ZIP_CODE," +
                    "ZIP_CODE_EXT=:ZIP_CODE_EXT,SPECIALITY=:SPECIALITY,TAX_ID=:TAX_ID,GROUP_NPI=:GROUP_NPI,PHONE_NO=:PHONE_NO," +
                    "PRODUCT_TYPE=:PRODUCT_TYPE,NO_OF_PROVIDERS=:NO_OF_PROVIDERS,PRE_EHR_VENDOR=:PRE_EHR_VENDOR," +
                    "IMP_CONACT_NAME=:IMP_CONACT_NAME,IMP_CONACT_PHONE=:IMP_CONACT_PHONE,IMP_CONACT_EMAIL=:IMP_CONACT_EMAIL," +
                    "DATA_MIGRATION=:DATA_MIGRATION,EMAIL_APPT_REMINDER=:EMAIL_APPT_REMINDER,CALL_SMS_APPT_REMINDER=:CALL_SMS_APPT_REMINDER," +
                    "DIRECT_EMAIL=:DIRECT_EMAIL,ELECTRONIC_PRESCRIPTION=:ELECTRONIC_PRESCRIPTION,ELECT_PAT_STATMENT_SERVICE=:ELECT_PAT_STATMENT_SERVICE," +
                    "LAB_INEGRATION=:LAB_INEGRATION,EPCS_SET_UP=:EPCS_SET_UP,COMPANY_LOGO=:COMPANY_LOGO,PREPOPULATE_PATIENT=:PREPOPULATE_PATIENT," +
                    "GROUP_MCR_PTAN=:GROUP_MCR_PTAN,GROUP_MCR_DME=:GROUP_MCR_DME,GROUP_MCR_RR=:GROUP_MCR_RR,COMMENTS=:COMMENTS," +
                    "PEHR_PROVIDED_FAX_NO=:PEHR_PROVIDED_FAX_NO,MEDCAID_PROVIDER=:MEDCAID_PROVIDER,BCBS_PROVIDER=:BCBS_PROVIDER,PROVIDER_TYPE=:PROVIDER_TYPE," +
                    "EHR_NOTIFIED=:EHR_NOTIFIED WHERE ENTITTY_SEQ_NUM=:ENTITTY_SEQ_NUM";
            using (var connection = new OracleConnection(_configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, entity);
                return result;
            }
        }

        public async Task<string> UpdateClientConfiguration(long seqNum, long eSeqNum, string flag, string visible)
        {
            string query = string.Empty;
            var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection"));
            connection.Open();
            if (flag == "CSSN_LICENSE_KEY")
            {
                string Licensekey = visible == "Y" ? _configuration["Amber:LicenseKey"] : "";
                query = "update WEB_AUTH.Entity set CSSN_LICENCE_KEY=:LicenseKey WHERE SEQ_NUM=:AUTH_ENTITY_SEQ_NUM";
                var updateCSSN = await connection.ExecuteAsync(query, new { CSSN_LICENCE_KEY = Licensekey, SEQ_NUM = seqNum });
            }
            else
            {
                if (flag == "IS_TELE_CLINIC")
                {
                    query = "Update PHR_PORTAL_OPTIONS set OPENTOK_APIKEY=:OPENTOKAPIKEY,OPENTOK_SECRETKEY=:OPENTOKSECRETKEY where ENTITY_SEQ_NUM=:ENTITY_SEQ_NUM";
                    var updateTeleFlag = await connection.ExecuteAsync(query, new { ENTITY_SEQ_NUM = eSeqNum, OPENTOKAPIKEY = "", OPENTOKSECRETKEY = "" });
                }
                query = "UPDATE WEB_AUTH.ENTITY SET " + flag + " =:VISIBLE WHERE SEQ_NUM=:AUTH_SEQ_NUM";
                var updateEntityFlag = await connection.ExecuteAsync(query, new { AUTH_SEQ_NUM = seqNum, VISIBLE = visible });
            }
            return "Configuration Updated Successfully";
        }

        public async Task<int> UpdatePracticeSetupDetail(string status, long practiceSeqNum, long entitySeqNum)
        {
            string query = "UPDATE PRACTICE_SETUP_DETAIL ET STATUS = :STATUS WHERE PRACTICE_LIST_SEQ_NUM = :PRACTICE_LIST_SEQ_NUM AND ENTITY_SEQ_NUM = :ENTITY_SEQ_NUM";
            int isUpdated = await DatabaseService.InsertUpdateCommandList(_configuration, "SupportDBConnection", query, new object[] { "STATUS", status, "PRACTICE_LIST_SEQ_NUM", practiceSeqNum, "ENTITY_SEQ_NUM", entitySeqNum });
            return isUpdated;
        }
       
        public async Task<Practice> GetByIdAsync(long id)
        {
            var sql = "SELECT Seq_Num,Practice_Name,Address,City,State,Zip_Code,Zip_Code_Ext,Speciality,Tax_Id," +
                "Group_Npi,Phone_No,Pehr_Provided_Fax_No,Product_Type,No_Of_Providers,Pre_Ehr_Vendor," +
                "Imp_Conact_Name,Imp_Conact_Phone,Imp_Conact_Email,Data_Migration,Email_Appt_Reminder," +
                "Call_Sms_Appt_Reminder,Direct_Email,Electronic_Prescription,Elect_Pat_Statment_Service," +
                "Lab_Inegration,Epcs_Set_Up,Telemedicine,Company_Logo,FEE_Schedule,Paper_Encounter_Super_Bill," +
                "Kiosk_Checkin,Prepopulate_Patient,Group_MCR_PTAN,Group_MCR_DME,Group_MCR_RR,Entitty_Seq_Num," +
                "Comments,Medcaid_Provider,BCBS_Provider,Provider_Type,EHR_Notified,No_Insurance_Needed" +
                " FROM PRACTICE WHERE ENTITTY_SEQ_NUM = :Id";
            using (var connection = new OracleConnection(_configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<Domain.Entities.Auth.Practice>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<CreateRequestResponse> AddPracticeAsync(Practice practice)
        {
            var sql = "INSERT INTO WEB_AUTH.PRACTICE(SEQ_NUM,PRACTICE_NAME,ADDRESS,CITY,STATE,ZIP_CODE,ZIP_CODE_EXT,SPECIALITY," +
                    "TAX_ID,GROUP_NPI,PHONE_NO,PRODUCT_TYPE,NO_OF_PROVIDERS,PRE_EHR_VENDOR,IMP_CONACT_NAME,IMP_CONACT_PHONE,IMP_CONACT_EMAIL," +
                    "DATA_MIGRATION,EMAIL_APPT_REMINDER,CALL_SMS_APPT_REMINDER,DIRECT_EMAIL,ELECTRONIC_PRESCRIPTION,ELECT_PAT_STATMENT_SERVICE," +
                    "LAB_INEGRATION,EPCS_SET_UP,COMPANY_LOGO,PREPOPULATE_PATIENT," +
                    "GROUP_MCR_PTAN,GROUP_MCR_DME,GROUP_MCR_RR,ENTITTY_SEQ_NUM,COMMENTS,PEHR_PROVIDED_FAX_NO,MEDCAID_PROVIDER,BCBS_PROVIDER,PROVIDER_TYPE,EHR_NOTIFIED) VALUES(PRIMARY_SEQ.NEXTVAL," +
                    ":PRACTICE_NAME,:ADDRESS,:CITY,:STATE,:ZIP_CODE,:ZIP_CODE_EXT,:SPECIALITY," +
                    ":TAX_ID,:GROUP_NPI,:PHONE_NO,:PRODUCT_TYPE,:NO_OF_PROVIDERS,:PRE_EHR_VENDOR,:IMP_CONACT_NAME,:IMP_CONACT_PHONE,:IMP_CONACT_EMAIL," +
                    ":DATA_MIGRATION,:EMAIL_APPT_REMINDER,:CALL_SMS_APPT_REMINDER,:DIRECT_EMAIL,:ELECTRONIC_PRESCRIPTION,:ELECT_PAT_STATMENT_SERVICE," +
                    ":LAB_INEGRATION,:EPCS_SET_UP,:COMPANY_LOGO,:PREPOPULATE_PATIENT," +
                    ":GROUP_MCR_PTAN,:GROUP_MCR_DME,:GROUP_MCR_RR,:ENTITTY_SEQ_NUM,:COMMENTS,:PEHR_PROVIDED_FAX_NO,:MEDCAID_PROVIDER,:BCBS_PROVIDER,:PROVIDER_TYPE,:EHR_NOTIFIED) returning SEQ_NUM into: SEQ_NUM";
            using (var connection = new OracleConnection(_configuration.GetConnectionString("AuthDBConnection")))
            {
                DynamicParameters param = new DynamicParameters(practice);
                param.Add(name: "SEQ_NUM", dbType: DbType.Double, direction: ParameterDirection.Output);
                connection.Open();
                var result = await connection.ExecuteAsync(sql, param);
                var Id = param.Get<Double>("SEQ_NUM");
                return new CreateRequestResponse { Status = result, Id = Id };
            }
        }

        public Task<int> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }


        public async Task<IReadOnlyList<PracticeComments>> GetPracticeCommentsByIdAsync(long id)
        {
            var sql = "SELECT SEQ_NUM, COMMENTS, ENTERED_BY,ENTERED_DATE, ENTITTY_SEQ_NUM   FROM WEB_AUTH.PRACTICE_COMMENTS WHERE ENTITTY_SEQ_NUM=:ENTITY_SEQ_NUM ORDER BY ENTERED_DATE DESC";
            using (var connection = new OracleConnection(_configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<PracticeComments>(sql, new { ENTITY_SEQ_NUM = id });
                return result.ToList();
            }
        }

        public async Task<int> AddCommentAsync(PracticeComments comment)
        {
            var sql = "INSERT INTO WEB_AUTH.PRACTICE_COMMENTS (SEQ_NUM,COMMENTS,ENTERED_BY,ENTERED_DATE,ENTITTY_SEQ_NUM) " +
              "VALUES (PRIMARY_SEQ.NEXTVAL,:COMMENTS,:ENTERED_BY,SYSDATE,:ENTITTY_SEQ_NUM)";
            using (var connection = new OracleConnection(_configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, comment);
                return result;
            }
        }

        public async Task<string> GetMultiPracticeIdsAsync(bool allPracticeUser, long entityId, long authEntityId, string dbServer)
        {
            string practiceIds = "";

            List<long> autIds = new List<long>();
            string strQuery = "SELECT E.SEQ_NUM FROM ENTITY E , IIS_SERVER IIS, DB_SERVER DS WHERE E.IIS_SERVER_SEQ_NUM=IIS.SEQ_NUM AND IIS.DB_SERVER_SEQ_NUM=DS.SEQ_NUM AND E.ENTITY_SEQ_NUM= :ENTITY_SEQ_NUM AND DS.TNS_NAME= :TNS";
            using (var connection = new OracleConnection(_configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                if (allPracticeUser)
                {
                    var autIdResult = await connection.QueryAsync<long>(strQuery, new { ENTITY_SEQ_NUM = entityId, TNS = dbServer });
                    autIds = autIdResult.ToList();
                }
                else
                {
                    autIds.Add(authEntityId);
                }

                if (autIds.Count > 0)
                {
                    strQuery = "SELECT DISTINCT APP_USER_SEQ_NUM FROM APP_USER_ENTITY WHERE ENTITY_SEQ_NUM IN :ENTITY_SEQ_NUM";
                    var result = await connection.QueryAsync<string>(strQuery, new { ENTITY_SEQ_NUM = autIds });
                    practiceIds = string.Join(",", result.ToList());
                }
            }

            return practiceIds;
        }

        public async  Task<IReadOnlyList<PracticeHelp>> GetPracticeHelpAsync()
        {
            var sql = "SELECT SEQ_NUM, BUTTON_ID, URL FROM PRACTICEEHR_HELP";
            using (var connection = new OracleConnection(_configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<PracticeHelp>(sql);
                return result.ToList();
            }
        }

        Task<int> IGenericRepositoryAsync<Practice>.AddAsync(Practice entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdatePracticePatchAsync(Practice practice)
        {
            var sql = "UPDATE WEB_AUTH.PRACTICE SET NO_INSURANCE_NEEDED=:NO_INSURANCE_NEEDED, COMPANY_LOGO=:COMPANY_LOGO WHERE ENTITTY_SEQ_NUM=:ENTITTY_SEQ_NUM";
            using (var connection = new OracleConnection(_configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, practice);
                return result;
            }
        }
    }
}
