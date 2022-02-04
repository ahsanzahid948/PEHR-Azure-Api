using Application.DTOs.Common.CreateRequestResponse;
using Application.DTOs.Common.PagingResponse;
using Application.Interfaces.Repositories;
using Application.Parameters;
using Dapper;
using Domain.Entities.Auth;
using Infrastructure.Utilities;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepositoryAsync : IUserRepositoryAsync
    {

        private readonly IConfiguration configuration;
        public UserRepositoryAsync(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<int> AddAsync(User user)
        {
            string storeProcedure = "WEB_AUTH.PR_CREATE_AUTH_USER_NEW";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add(@"P_EMAIL", user.Email);
                parameters.Add(@"P_PASSWORD", user.Password);
                parameters.Add(@"P_FIRST_NAME", user.First_Name);
                parameters.Add(@"P_LAST_NAME", user.Last_Name);
                parameters.Add(@"P_USERNAME", user.User_Name);
                parameters.Add(@"P_ENTITY_sQ_NUM", user.Default_Entity_Seq_Num);
                parameters.Add(@"P_ACTIVE", user.Active_Flag);
                parameters.Add(@"P_MENU_ROLE_SEQ_NUM", user.Menu_Role_Seq_Num);
                parameters.Add(@"P_EMERGENCY_ROLE_sEQ_NUM", user.Emergency_Role_Seq_Num);
                parameters.Add(@"P_ALLOW_DELETE", user.Allow_Delete);
                parameters.Add(@"P_TOKEN", user.Token);
                parameters.Add(@"P_PROVIDER_SEQ_NUM", user.Provider_Seq_Num);
                parameters.Add(@"P_EPCS", user.EPCS);
                parameters.Add(@"P_APPROVE_EPCS", user.Approve_EPCS);
                parameters.Add(@"P_MIDDLE_INITIAL", user.Middle_Initial);
                parameters.Add(@"P_ADMIN_FLAG", user.Admin_Flag);
                parameters.Add(@"P_SHOW_SETUP_PROFILES", user.Show_setup_Profiles);
                var result = await connection.ExecuteAsync(@storeProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return 1;
            }
        }

        public async Task<int> DeleteAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyList<User>> GetAllAsync(int pageNumber, int pageSize)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> GetByFilterAsync(string filterCriteria)
        {
            var sql = "SELECT * FROM app_user_prof WHERE lower(email) = :email";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<User>(sql, new { email = filterCriteria });
                return result;
            }
        }

        public async Task<User> GetByIdAsync(long id)
        {
            var sql = "SELECT * FROM app_user_prof WHERE seq_num = :Id";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<User>> GetByEntityIdAsync(string id, string filter)
        {

            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                if (string.IsNullOrEmpty(id))
                {
                    var sql = "SELECT SEQ_NUM, PASSWORD, EMAIL FROM WEB_AUTH.APP_USER_PROF";
                    var result = await connection.QueryAsync<User>(sql);
                    return result.ToList();
                }
                else
                {
                    var sql = "SELECT * FROM app_user_prof WHERE " + filter + " AND default_entity_seq_num = :Id";
                    var result = await connection.QueryAsync<User>(sql, new { Id = id });
                    return result.ToList();
                }

            }
        }
        public async Task<int> UpdateAsync(User user)
        {
            string sql = "UPDATE WEB_AUTH.APP_USER_PROF SET EPCS_REGISTRATION = :EPCS_REGISTRATION, EPCS_2ND_APPROVAL = :EPCS_2ND_APPROVAL, ALLOW_EPCS = :ALLOW_EPCS, ALLOW_MULTIPLE_SESSIONS = :ALLOW_MULTIPLE_SESSIONS, PMP_NARX = :PMP_NARX, PMP_ROLE = :PMP_ROLE, DEFAULT_TOKEN_TYPE = :DEFAULT_TOKEN_TYPE, TOKEN_REF= :TOKEN_REF, LICENSEKEY= :LICENSEKEY, IS_EMERGENCY_ACQUIRED=:IS_EMERGENCY_ACQUIRED, DEFAULT_ENTITY_SEQ_NUM = :DEFAULT_ENTITY_SEQ_NUM, QUICKTOUR_DONTSHOW = :QUICKTOUR_DONTSHOW, TOKEN = :TOKEN  WHERE UPPER(EMAIL) = UPPER(:EMAIL)";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, user);
                return result;
            }
        }
        public async Task<int> UpdateAsync(Kiosk user)
        {
            throw new System.NotImplementedException();
        }
        public async Task<User> GetEPCSSettingsByEmailAsync(string email)
        {
            var sql = "SELECT Seq_Num, Email, Approve_EPCS, EPCS_Registration, Licensekey, Token_Ref, Default_Token_Type, Provider_Seq_Num FROM APP_USER_PROF WHERE UPPER(EMAIL) = :EMAIL";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<User>(sql, new { EMAIL = email.ToUpper() });
                return result;
            }
        }

        public async Task<IReadOnlyList<DataExportSetting>> GetDataExportSettingsByIdAsync(long id)
        {
            var sql = "SELECT Seq_Num, Date_From, Date_To, Export_Type_Flag, Relative_Export_Type, Relative_Export_Value, Specific_Date, Export_Time, Entry_Date, Account_Num, All_Patient_Flag, Status, USER_SEQ_NUM,AUTH_ENTITY_SEQ_NUM  FROM APP_USER_SETTINGS WHERE USER_SEQ_NUM = :USER_SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<DataExportSetting>(sql, new { USER_SEQ_NUM = id });
                return result.ToList();
            }
        }

        public async Task<int> UpdateEPCSSettingAsync(User user)
        {

            var sql = "UPDATE APP_USER_PROF SET Approve_EPCS =: Approve_EPCS, EPCS_Registration =:EPCS_Registration, Licensekey =:Licensekey, Token_Ref =:Token_Ref, Default_Token_Type =:Default_Token_Type WHERE UPPER(EMAIL) = UPPER(:EMAIL)";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, user);
                return result;
            }
        }

        public async Task<int> CreateDataExportSettingAsync(DataExportSetting dataExportSetting)
        {
            var sql = "INSERT INTO APP_USER_SETTINGS ( Seq_Num,Auth_Entity_Seq_Num,User_Seq_Num, Date_From, Date_To, Export_Type_Flag, Relative_Export_Type, Relative_Export_Value, Specific_Date, Export_Time, Entry_Date, Account_Num, All_Patient_Flag, Status ) VALUES(PRIMARY_SEQ.NEXTVAL, :Auth_Entity_Seq_Num,:User_Seq_Num,:Date_From, :Date_To, :Export_Type_Flag, :Relative_Export_Type, :Relative_Export_Value, :Specific_Date, :Export_Time, :Entry_Date, :Account_Num, :All_Patient_Flag, :Status )";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, dataExportSetting);
                return result;
            }
        }

        public async Task<int> UpdateDataExportSettingAsync(DataExportSetting dataExportSetting)
        {
            var sql = "UPDATE APP_USER_SETTINGS  SET Auth_Entity_Seq_Num=:Auth_Entity_Seq_Num,User_Seq_Num=:User_Seq_Num, Date_From=:Date_From, Date_To=:Date_To, Export_Type_Flag=:Export_Type_Flag, Relative_Export_Type=:Relative_Export_Type, Relative_Export_Value=:Relative_Export_Value, Specific_Date=:Specific_Date, Export_Time=:Export_Time, Entry_Date=:Entry_Date, Account_Num=:Account_Num, All_Patient_Flag=:All_Patient_Flag, Status=:Status  WHERE Seq_Num =:Seq_Num";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, dataExportSetting);
                return result;
            }
        }

        public async Task<IReadOnlyList<User>> GetUserSettingsByIdAsync(string email)
        {
            var sql = "SELECT SEQ_NUM,MENU_ROLE_SEQ_NUM,EMERGENCY_ROLE_SEQ_NUM, ADMIN_FLAG,DECODE(NVL(IDLE_TIME_OUT,0), 0 ,  120, IDLE_TIME_OUT) IDLE_TIME_OUT, RX_ALERT_SEVERE, RX_ALERT_MAJOR, RX_ALERT_MODERATE, RX_ALERT_MINOR, ALERT_LIFE_STYLE, ALERT_ALLERGY, PAST_MED_TO_NOTE,BILLING_TOP, BILLING_BOTTOM, BILLING_LEFT,BILLING_RIGHT, BILLING_PATIENT_ADDRESS_TOP, BILLING_PATIENT_ADDRESS_LEFT, BILLING_LOCATION_ADDRESS_TOP, BILLING_LOCATION_ADDRESS_LEFT, BILLING_MESSAGE_TOP, BILLING_MESSAGE_LEFT, FAX_SENDER_NAME, FAX_SENDER_PHONE_NO, FAX_SENDER_FAX_NO,IS_EMERGENCY_ACQUIRED,SCAN_PAGE_SIZE,SCAN_CROP_IMAGE_LEFT,SCAN_CROP_IMAGE_RIGHT,SCAN_CROP_IMAGE_TOP,SCAN_CROP_IMAGE_BOTTOM,SCAN_IMAGE_QUALITY,SCAN_UPLOAD_TYPE,SHOW_LAST_NAME_CHR,SHOW_FIRST_NAME_CHR,DUPLEX, SHOW_SETUP_PROFILES, BILLING_HCFA_TOP, BILLING_HCFA_RIGHT, BILLING_HCFA_LEFT, BILLING_HCFA_BOTTOM, BILLING_HCFA_ADDRESS_TOP, BILLING_HCFA_ADDRESS_LEFT, VISIT_HEADING_COLLAPSED,VISIT_TOP,VISIT_RIGHT,VISIT_LEFT,VISIT_BOTTOM, DEFAULT_VISIT_TYPE, BATCH_NUM FROM WEB_AUTH.APP_USER_PROF WHERE UPPER(EMAIL) = :EMAIL";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<User>(sql, new { EMAIL = email.ToUpper() });
                return result.ToList();
            }
        }

        public async Task<int> UpdateUserSettingAsync(User user)
        {
            var sql = "UPDATE APP_USER_PROF SET MENU_ROLE_SEQ_NUM=:MENU_ROLE_SEQ_NUM,EMERGENCY_ROLE_SEQ_NUM=:EMERGENCY_ROLE_SEQ_NUM,ADMIN_FLAG=:ADMIN_FLAG,IDLE_TIME_OUT=:IDLE_TIME_OUT,RX_ALERT_SEVERE=:RX_ALERT_SEVERE,RX_ALERT_MAJOR=:RX_ALERT_MAJOR,RX_ALERT_MODERATE=:RX_ALERT_MODERATE, RX_ALERT_MINOR=:RX_ALERT_MINOR, ALERT_LIFE_STYLE=:ALERT_LIFE_STYLE, ALERT_ALLERGY=:ALERT_ALLERGY, PAST_MED_TO_NOTE=:PAST_MED_TO_NOTE,BILLING_TOP=:BILLING_TOP, BILLING_BOTTOM=:BILLING_BOTTOM, BILLING_LEFT=:BILLING_LEFT,BILLING_RIGHT=:BILLING_RIGHT, BILLING_PATIENT_ADDRESS_TOP=:BILLING_PATIENT_ADDRESS_TOP, BILLING_PATIENT_ADDRESS_LEFT=:BILLING_PATIENT_ADDRESS_LEFT, BILLING_LOCATION_ADDRESS_TOP=:BILLING_LOCATION_ADDRESS_TOP, BILLING_LOCATION_ADDRESS_LEFT=:BILLING_LOCATION_ADDRESS_LEFT, BILLING_MESSAGE_TOP=:BILLING_MESSAGE_TOP, BILLING_MESSAGE_LEFT=:BILLING_MESSAGE_LEFT, FAX_SENDER_NAME=:FAX_SENDER_NAME, FAX_SENDER_PHONE_NO=:FAX_SENDER_PHONE_NO, FAX_SENDER_FAX_NO=:FAX_SENDER_FAX_NO,IS_EMERGENCY_ACQUIRED=:IS_EMERGENCY_ACQUIRED,SCAN_PAGE_SIZE=:SCAN_PAGE_SIZE,SCAN_CROP_IMAGE_LEFT=:SCAN_CROP_IMAGE_LEFT,SCAN_CROP_IMAGE_RIGHT=:SCAN_CROP_IMAGE_RIGHT,SCAN_CROP_IMAGE_TOP=:SCAN_CROP_IMAGE_TOP,SCAN_CROP_IMAGE_BOTTOM=:SCAN_CROP_IMAGE_BOTTOM,SCAN_IMAGE_QUALITY=:SCAN_IMAGE_QUALITY,SCAN_UPLOAD_TYPE=:SCAN_UPLOAD_TYPE,SHOW_LAST_NAME_CHR=:SHOW_LAST_NAME_CHR,SHOW_FIRST_NAME_CHR=:SHOW_FIRST_NAME_CHR,DUPLEX=:DUPLEX, SHOW_SETUP_PROFILES=:SHOW_SETUP_PROFILES, BILLING_HCFA_TOP=:BILLING_HCFA_TOP, BILLING_HCFA_RIGHT=:BILLING_HCFA_RIGHT, BILLING_HCFA_LEFT=:BILLING_HCFA_LEFT, BILLING_HCFA_BOTTOM=:BILLING_HCFA_BOTTOM, BILLING_HCFA_ADDRESS_TOP=:BILLING_HCFA_ADDRESS_TOP, BILLING_HCFA_ADDRESS_LEFT=:BILLING_HCFA_ADDRESS_LEFT, VISIT_HEADING_COLLAPSED=:VISIT_HEADING_COLLAPSED,VISIT_TOP=:VISIT_TOP,VISIT_RIGHT=:VISIT_RIGHT,VISIT_LEFT=:VISIT_LEFT,VISIT_BOTTOM=:VISIT_BOTTOM,DEFAULT_VISIT_TYPE=:DEFAULT_VISIT_TYPE, BATCH_NUM=:BATCH_NUM WHERE UPPER(EMAIL) = :EMAIL";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, user);
                return result;
            }
        }

        public async Task<PageResponse<User>> GetUsersByFilterAsync(string sortname, string sortorder, RequestPageParameter requestPageParameter, string userId, string active, string firstName, string lastName, string email)
        {

            if (!string.IsNullOrEmpty(userId))
            {
                var sql = "SELECT SEQ_NUM,EMAIL,FIRST_NAME,LAST_NAME,ACTIVE_FLAG,ALLOW_DELETE,MENU_ROLE_SEQ_NUM,EMERGENCY_ROLE_SEQ_NUM,DEFAULT_ENTITY_SEQ_NUM,PROVIDER_SEQ_NUM,EPCS,APPROVE_EPCS,ALLOW_EPCS,USER_NAME,EPCS_REGISTRATION,EPCS_2ND_APPROVAL, MIDDLE_INITIAL, ADMIN_FLAG,SHOW_SETUP_PROFILES,CP_USER, ALLOW_MULTIPLE_SESSIONS,PMP_NARX,PMP_ROLE FROM WEB_AUTH.V_AUTH_USER_SEARCH WHERE SEQ_NUM IN :SEQ_NUM";
                StringBuilder filter = new StringBuilder();
                if (!string.IsNullOrEmpty(firstName))
                {
                    filter.Append(" AND UPPER(FIRST_NAME) LIKE '" + firstName.ToUpper() + "'");
                }

                if (!string.IsNullOrEmpty(lastName))
                {
                    filter.Append(" AND UPPER(LAST_NAME) LIKE '" + firstName.ToUpper() + "'");
                }

                if (!string.IsNullOrEmpty(email))
                {
                    filter.Append(" AND UPPER(EMAIL) LIKE '" + email.ToUpper() + "'");
                }

                if (!string.IsNullOrEmpty(active))
                {
                    filter.Append(" AND UPPER(ACTIVE_FLAG) ='" + active.ToUpper() + "'");
                }
                var totalRowsSql = "SELECT COUNT (SEQ_NUM) AS TOTAL_ROWS FROM WEB_AUTH.V_AUTH_USER_SEARCH WHERE SEQ_NUM IN :SEQ_NUM" + filter;
                List<Int64> seqNums = userId.Split(',').Select(Int64.Parse).ToList();
                using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
                {
                    connection.Open();
                    var total = int.Parse((await connection.ExecuteScalarAsync(totalRowsSql, new { SEQ_NUM = seqNums }))?.ToString());
                    DynamicParameters parameters = new DynamicParameters();

                    sql = Utility.FilterQuery(sql + filter, null, sortname, sortorder, out parameters);
                    sql = requestPageParameter != null ? Utility.PaginatedQuery(sql, requestPageParameter) : sql;
                    var result = await connection.QueryAsync<User>(sql, new { SEQ_NUM = seqNums });
                    return new PageResponse<User>(result.ToList(), total);
                };
            }
            else
            {
                var sql = "SELECT SEQ_NUM,EMAIL,FIRST_NAME,LAST_NAME,ACTIVE_FLAG,ALLOW_DELETE,MENU_ROLE_SEQ_NUM,EMERGENCY_ROLE_SEQ_NUM,DEFAULT_ENTITY_SEQ_NUM,PROVIDER_SEQ_NUM,EPCS,APPROVE_EPCS,ALLOW_EPCS,USER_NAME,EPCS_REGISTRATION,EPCS_2ND_APPROVAL, MIDDLE_INITIAL, ADMIN_FLAG,SHOW_SETUP_PROFILES,CP_USER, ALLOW_MULTIPLE_SESSIONS,PMP_NARX,PMP_ROLE FROM WEB_AUTH.V_AUTH_USER_SEARCH WHERE UPPER(EMAIL) LIKE :Email";
                using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<User>(sql, new { Email = email.ToUpper()});
                    return new PageResponse<User>(result.ToList(), 1);
                };
            }
        }

        public async Task<IReadOnlyList<UserEntities>> GetUsersEntitesAsync(string email, string epcs, string approveEpcs, string authEntityId)
        {
            var sql = "SELECT DESCRIPTION, ENTITY_SHORT_NAME, SEQ_NUM, ENTITY_SEQ_NUM, EMAIL FROM WEB_AUTH.V_APP_USER_ACCESS WHERE UPPER(TRIM(EMAIL)) = UPPER(TRIM(:EMAIL))";

            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                if (!string.IsNullOrEmpty(authEntityId) && (!string.IsNullOrEmpty(epcs) || !string.IsNullOrEmpty(approveEpcs)))
                {

                    sql = "SELECT DISTINCT APP_USER_SEQ_NUM,EMAIL,USER_NAME FROM WEB_AUTH.V_APP_USER_ACCESS WHERE (Upper(EPCS) = :EPCS OR Upper(APPROVE_EPCS) = :APPROVE_EPCS) AND SEQ_NUM= :SEQ_NUM ORDER BY USER_NAME";
                    var result = await connection.QueryAsync<UserEntities>(sql, new { EPCS = epcs.ToUpper(), APPROVE_EPCS = approveEpcs.ToUpper(), SEQ_NUM = authEntityId });
                    return result.ToList();
                }
                else if (!string.IsNullOrEmpty(authEntityId))
                {
                    sql = "SELECT DISTINCT(EMAIL) FROM WEB_AUTH.V_APP_USER_ACCESS WHERE SEQ_NUM= :SEQ_NUM";
                    var result = await connection.QueryAsync<UserEntities>(sql, new { SEQ_NUM = authEntityId });
                    return result.ToList();
                }
                else
                {
                    var result = await connection.QueryAsync<UserEntities>(sql, new { EMAIL = email });
                    return result.ToList();
                }
            }

        }

        public async Task<CreateRequestResponse> AddUserLogAsync(AuditSession userLog, bool authSequence)
        {
            var sql = "INSERT INTO WEB_AUTH.AUDIT_TABLE_SESSION(SEQ_NUM,URL,IP_ADDRESS,APP_USER,DB_USER,LOGIN_TIME,LOGOUT_TIME) VALUES(:SEQ_NUM,:URL,:IP_ADDRESS,:APP_USER,:DB_USER,SYSDATE,NULL)";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var logSeqNum = -1L;
                if (authSequence)
                {
                    logSeqNum = await connection.ExecuteScalarAsync<long>("SELECT AUDIT_TABLE_SESSION_SEQ.NEXTVAL FROM DUAL");
                }
                else
                {
                    logSeqNum = await connection.ExecuteScalarAsync<long>("SELECT MAX(SEQ_NUM) AS MAX_SEQ_NUM FROM WEB_AUTH.AUDIT_TABLE_SESSION") + 1;
                }
                userLog.Seq_Num = logSeqNum;
                var result = await connection.ExecuteAsync(@sql, userLog);
                return new CreateRequestResponse { Status = result, Id = logSeqNum };
            }
        }

        public async Task<int> UpdateUserLogAsync(long userLogSeqNum)
        {
            var sql = "UPDATE WEB_AUTH.AUDIT_TABLE_SESSION SET LOGOUT_TIME = SYSDATE WHERE LOGOUT_TIME IS NULL AND SEQ_NUM = :SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                var result = await connection.ExecuteAsync(@sql, new { SEQ_NUM = userLogSeqNum });
                return result;
            }
        }

        public async Task<int> UpdateUserPasswordByEmailAsync(string email, string password)
        {
            var sql = "UPDATE WEB_AUTH.APP_USER_PROF SET PASSWORD = :PASSWORD WHERE UPPER(EMAIL) = :EMAIL";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                var result = await connection.ExecuteAsync(@sql, new { EMAIL = email.ToUpper(), PASSWORD = password });
                return result;
            }
        }

        public async Task<int> UpdateUserTokenAsync(string email, string token)
        {
            var sql = "UPDATE WEB_AUTH.APP_USER_PROF SET TOKEN = :TOKEN WHERE UPPER(EMAIL) = :EMAIL";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                var result = await connection.ExecuteAsync(@sql, new { TOKEN = token, EMAIL = email.ToUpper() });
                return result;
            }
        }

        public async Task<int> UpdateUserPasswordAsync(string token, string password)
        {
            var sql = "UPDATE WEB_AUTH.APP_USER_PROF SET PASSWORD = :PASSWORD, TOKEN = NULL WHERE UPPER(TOKEN) = :TOKEN";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                var result = await connection.ExecuteAsync(@sql, new { TOKEN = token.ToUpper(), PASSWORD = password });
                return result;
            }
        }

        public async Task<User> GetByFilterAsync(string email, string token)
        {
            if (!string.IsNullOrEmpty(email))
                return await GetByFilterAsync(email.ToLower());
            else
            {
                var sql = "SELECT * FROM app_user_prof WHERE UPPER(TOKEN) = :token";
                using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
                {
                    connection.Open();
                    var result = await connection.QuerySingleOrDefaultAsync<User>(sql, new { TOKEN = token?.ToUpper() });
                    return result;
                }
            }
        }

        public async Task<int> UpdateEPCSApproveAsync(List<long> userId, string epcs2ndApproval, string epcsApproval)
        {
            var sql = "UPDATE WEB_AUTH.APP_USER_PROF SET EPCS_2ND_APPROVAL = :EPCS_2ND_APPROVAL WHERE SEQ_NUM IN :USER_SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                if (string.IsNullOrEmpty(epcsApproval))
                {
                    var result = await connection.ExecuteAsync(@sql, new { USER_SEQ_NUM = userId, EPCS_2ND_APPROVAL = epcs2ndApproval });
                    return result;
                }
                else
                {
                    sql = "UPDATE WEB_AUTH.APP_USER_PROF SET EPCS_2ND_APPROVAL = :EPCS_2ND_APPROVAL WHERE SEQ_NUM IN :USER_SEQ_NUM AND EPCS_2ND_APPROVAL = :EPCS_APPROVAL";
                    var result = await connection.ExecuteAsync(@sql, new { USER_SEQ_NUM = userId, EPCS_2ND_APPROVAL = epcs2ndApproval, EPCS_APPROVAL = epcsApproval });
                    return result;
                }
            }
        }

        public async Task<int> DeleteDataExportSettingAsync(long settingId)
        {
            var sql = "DELETE FROM WEB_AUTH.APP_USER_SETTINGS WHERE SEQ_NUM=:SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                var result = await connection.ExecuteAsync(@sql, new { SEQ_NUM = settingId });
                return result;
            }
        }

        public async Task<int> GetUserProviderCountAsync(string email, long providerId)
        {
            var sql = "SELECT COUNT(*) AS ROWSFOUND FROM WEB_AUTH.APP_USER_PROF WHERE UPPER(EMAIL) <> :EMAIL AND PROVIDER_SEQ_NUM = :PROVIDER_SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                var result = int.Parse((await connection.ExecuteScalarAsync(sql, new { EMAIL = email.ToUpper(), PROVIDER_SEQ_NUM = providerId }))?.ToString());
                return result;
            }
        }

        public async Task<IReadOnlyList<User>> GetUserbyTokeAsync(string token)
        {
            var sql = "SELECT * FROM app_user_prof WHERE UPPER(TOKEN_REF) = :TOKEN_REF";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<User>(sql, new { TOKEN_REF = token?.ToUpper() });
                return result.ToList();
            }
        }

        public async Task<IReadOnlyList<User>> GetUserbyMenuRoleAsync(string menuRoleId, string emergencyRoleId = "")
        {
            var sql = "SELECT * FROM app_user_prof WHERE MENU_ROLE_SEQ_NUM = :MENU_ROLE_SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                if (string.IsNullOrEmpty(emergencyRoleId))
                {
                    var result = await connection.QueryAsync<User>(sql, new { MENU_ROLE_SEQ_NUM = menuRoleId });
                    return result.ToList();
                }
                else
                {
                    sql = "SELECT * FROM app_user_prof WHERE MENU_ROLE_SEQ_NUM = :MENU_ROLE_SEQ_NUM OR EMERGENCY_ROLE_SEQ_NUM =:EMERGENCY_ROLE_SEQ_NUM";
                    var result = await connection.QueryAsync<User>(sql, new { MENU_ROLE_SEQ_NUM = menuRoleId, EMERGENCY_ROLE_SEQ_NUM = emergencyRoleId });
                    return result.ToList();
                }
            }
        }

        public async Task<IReadOnlyList<User>> GetInternalUsersAsync(long entityId, bool practiceRcm, string active, string rcmType, string rcmTypeTwo)
        {
            string sqlWhere = "";
            if (practiceRcm)
            {
                sqlWhere = " E.SEQ_NUM = :ENTITY_SEQ_NUM";
            }
            else
            {
                sqlWhere = " E.ENTITY_SEQ_NUM = :ENTITY_SEQ_NUM";
            }
            var sql = "SELECT DISTINCT (A.EMAIL) FROM WEB_AUTH.APP_USER_PROF A, WEB_AUTH.APP_USER_ENTITY B, WEB_AUTH.ENTITY E WHERE A.SEQ_NUM = B.APP_USER_SEQ_NUM AND B.ENTITY_SEQ_NUM = E.SEQ_NUM AND A.ACTIVE_FLAG = :ACTIVE_FLAG AND ( (UPPER (A.RCM_TYPE) = :RCM_TYPE OR UPPER (A.RCM_TYPE) = :RCM_TYPE2) AND " + sqlWhere + ") ORDER BY EMAIL ASC";
            using (var connection = new OracleConnection(configuration.GetConnectionString("AuthDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<User>(sql, new { ACTIVE_FLAG = active, RCM_TYPE = rcmType, RCM_TYPE2 = rcmTypeTwo, ENTITY_SEQ_NUM = entityId });
                return result.ToList();

            }
        }
    }
}