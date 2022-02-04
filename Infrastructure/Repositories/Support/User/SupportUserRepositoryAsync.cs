using Application.DTOs.User;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Wrappers;
using Application.Exceptions;
using AutoMapper;
using Dapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Domain.Entities.Notes;
using Infrastructure.Common.Services;
using Application.DTOs;
using Domain.Entities.Auth;
using Domain.Entities.Support;
using Application.DTOs.Common.CreateRequestResponse;
using System.Data;

namespace Infrastructure.Repositories
{
    public class SupportUserRepositoryAsync : ISupportUserRepositoryAsync
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;
        private readonly IPasswordService _PasswordService;
        private readonly IEmailService _emailService;

        public SupportUserRepositoryAsync(IConfiguration configuration, IMapper mapper, IPasswordService PasswordService, IEmailService emailService)
        {
            this.configuration = configuration;
            this._mapper = mapper;
            this._PasswordService = PasswordService;
            this._emailService = emailService;

        }

        public async Task<int> AddAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> DeleteAsync(int id)
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
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<User>(sql, new { email = filterCriteria });
                return result;
            }
        }
        public async Task<User> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM app_user_prof WHERE seq_num = :Id";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<User> UserLoginAsync(string email, string password)
        {
            IList<ValidationFailure> messages = new List<ValidationFailure>();
            var sql = "SELECT ACTIVE,FULL_ACCESS,PASSWORD,EMAIL,SECRET_KEY FROM SUPPORT_TICKET_LOGIN_INFO WHERE EMAIL=: email";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<User>(sql, new { email = email });
                if (result != null)
                {
                    if (result.Active_Flag == "Y")
                    {
                        if (result.Password == password)//var decryptedPass = result.Password.Decrypt(username.Slice(0, end: 31).PadRight(32, '*'));
                        {
                            if (result.FULL_ACCESS != "N")
                            {
                                if (string.IsNullOrEmpty(result.Secret_Key))
                                {
                                    _PasswordService.GenerateTwoFactorAuthentication(true, "", email);
                                }
                                else
                                {
                                    _PasswordService.GenerateTwoFactorAuthentication(false, result.Secret_Key, email);//SECRET KEY DB COLUMN MISSING
                                }

                                return result;
                            }
                            return result;

                        }
                        else
                        {
                            messages.Add(new ValidationFailure("Password", "Incorrect Password"));
                        }
                    }
                }
                else
                {
                    messages.Add(new ValidationFailure("User", "No User Found"));
                }
                if (messages.Count > 0)
                {
                    throw new ValidationException(messages);
                }
                else
                {
                    return result;
                }
            }
        }

        public async Task<IReadOnlyList<User>> GetUserInfo(int pageNumber, int pageSize, int id)
        {
            var sql = "SELECT * FROM app_user_prof WHERE default_entity_seq_num = :Id";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<User>(sql, new { Id = id });
                return result.ToList();
            }
        }
        public async Task<int> UpdateAsync(User user)
        {
            throw new System.NotImplementedException();
        }
        public Task<int> UpdateAsync(Kiosk entity)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(double id)
        {
            throw new NotImplementedException();
        }
        public async Task<IReadOnlyList<UserPreferences>> AuthUserNotificationSettingsAsync(string seqnum)
        {
            IList<ValidationFailure> messages = new List<ValidationFailure>();
            var query = "SELECT TYPE,NOTIFICATION_TYPE,SEQ_NUM FROM WEB_AUTH.SUPPORT_USER_PREFRENCES WHERE USER_SEQ_NUM=:USER_SEQ_NUM";
            List<UserPreferences> list = new List<UserPreferences>();
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<UserPreferences>(query, new { USER_SEQ_NUM = seqnum });
                if (result.ToList().Count > 0)
                {
                    return result.ToList();

                }
                return result.ToList();
            }
            messages.Add(new ValidationFailure("Connection Error", "Connection String"));
            throw new ValidationException(messages.ToList());

        }

        public async Task<ClientDetail_VM> EntityManagersAsync(PayLoad payload)
        {
            string sqlWhere = " ";


            IList<ValidationFailure> messages = new List<ValidationFailure>();
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                if (payload.Reseller == "Y")
                {
                    sqlWhere = " AND UPPER(RESELLER) = :RESELLER";
                }
                var sql = "SELECT DISTINCT(LOWER(IMPLEMENTATION_MANAGER)) AS IMPLEMENTATION_MANAGER FROM CLIENT_PROF WHERE IMPLEMENTATION_MANAGER IS NOT NULL" + sqlWhere;

                var dsImpMngr = await connection.QueryAsync<ClientProfile>(sql, new { RESELLER = payload.Reseller });




                sql = "SELECT DISTINCT(UPPER(SALES_PERSON)) AS SALES_PERSON FROM CLIENT_PROF WHERE SALES_PERSON IS NOT NULL";
                var dsSaleMngr = await connection.QueryAsync<ClientProfile>(sql);



                sqlWhere = " WHERE UPPER(ACCOUNT_TYPE) = 'PRODUCTION' ";
                if (payload.Reseller == "Y")
                {
                    sqlWhere += " AND UPPER(CHANNEL_PARTNER) = :CHANNEL_PARTNER";
                }

                sql = "SELECT AUTH_ENTITY_SEQ_NUM FROM V_PEHR_CLIENTS_LIST " + sqlWhere + " ORDER BY PRACTICE_NAME ASC";
                var dsCustAccNum = await connection.QueryAsync<ClientsList>(sql, new { CHANNEL_PARTNER = payload.CP });

                ClientDetail_VM response = new ClientDetail_VM();
                response.IManager = dsImpMngr.ToList();
                response.SManager = dsSaleMngr.ToList();
                response.ClientNumbers = dsCustAccNum.ToList();

                return response;
            }
        }

        public async Task<IReadOnlyList<ClientsList>> GetClientListAsync(PayLoad payload, string ddlSalesMgr, string ddlImplement, string ddlCust)
        {
            IList<ValidationFailure> messages = new List<ValidationFailure>();
            string SqlWhere = " WHERE UPPER(ACCOUNT_TYPE) IN ('PRODUCTION', 'IMPLEMENTATION') ";
            if (ddlSalesMgr != null)
            {
                SqlWhere += " AND UPPER(SALES_PERSON) LIKE :SALES_PERSON";
            }
            if (ddlImplement != null)
            {
                SqlWhere += " AND UPPER(IMPLEMENTATION_MANAGER) LIKE :IMPLEMENTATION_MANAGER";
            }
            if (ddlCust != null)
            {
                SqlWhere += " AND AUTH_ENTITY_SEQ_NUM = :AUTH_ENTITY_SEQ_NUM";
            }
            if (payload.Reseller == "Y")
            {
                SqlWhere += " AND UPPER(CHANNEL_PARTNER) = :CHANNEL_PARTNER";
            }
            string strQuery = "SELECT SEQ_NUM, AUTH_ENTITY_SEQ_NUM, PRACTICE_NAME, ENTITY_SHORT_NAME, SPECIALITY, PRACTICE_CITY, CHANNEL_PARTNER, ALERT, USER_TYPE, BILLING_FLAG, RCMTYPE, CHTYPE, IS_TELE_CLINIC, KIOSK_FLAG, ACTIVE_FLAG, PRACTICE_SETUP,IMPLEMENTATION_MANAGER FROM V_PEHR_CLIENTS_LIST" + SqlWhere + " ORDER BY PRACTICE_NAME ASC";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                var response = await connection.QueryAsync<ClientsList>(strQuery, new { SALES_PERSON = ddlSalesMgr, IMPLEMENTATION_MANAGER = ddlImplement, CHANNEL_PARTNER = payload.CP, AUTH_ENTITY_SEQ_NUM = ddlCust });
                if (response.ToList().Count > 0)
                {

                    return response.ToList();
                }
                messages.Add(new ValidationFailure("Error", "No Record Found"));
            }

            throw new ValidationException(messages.ToList());


        }
        public async Task<AssignedTicket_VM> SupportAssignedTicketsAsync(PayLoad payload, string entity, string teamseqnum)
        {
            IList<ValidationFailure> messages = new List<ValidationFailure>();
            teamseqnum = teamseqnum != null ? teamseqnum : "";
            string sqlWhere = string.Empty;
            if (teamseqnum != "")
            {
                sqlWhere = " WHERE SEQ_NUM=" + teamseqnum;
            }
            sqlWhere = "SELECT SEQ_NUM,EMAIL,UPPER(NAME) AS SHORT_NAME FROM SUPPORT_TICKET_TEAM_INFO" + sqlWhere + " ORDER BY NAME ASC";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                var response = await connection.QueryAsync<SupportTicketTeamInfo>(sqlWhere);
                var result = new AssignedTicket_VM();

                var loginInfo = await GetSupportTicketUser(payload);
                result.Ds_Login = loginInfo;
                result.Ds_Team = response.ToList();
                return result;
            }
            messages.Add(new ValidationFailure("UserRepositoryAsync->GetSupportAssignedTicketsByQueryAsync()", "ConnectionString"));
            throw new ValidationException(messages.ToList());
        }
        public async Task<List<SupportUserInfo>> GetSupportTicketUser(PayLoad payload)
        {
            payload.AppUser = payload.AppUser != null ? payload.AppUser : "";
            payload.Reseller = payload.Reseller != null ? payload.Reseller : "";
            payload.CP = payload.CP != null ? payload.CP : "";
            string sqlWhere = " WHERE ACTIVE= 'Y'";
            if (payload.Reseller == "Y")
            {
                sqlWhere += " AND UPPER(CP) = :CP";
            }
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                string query = "SELECT LOWER(EMAIL) AS EMAIL,UPPER(FIRST_NAME || ' ' || LAST_NAME) AS SHORT_NAME FROM SUPPORT_TICKET_LOGIN_INFO " + sqlWhere + " ORDER BY SHORT_NAME ASC";
                var response = await connection.QueryAsync<SupportUserInfo>(query, new { CP = payload.CP });

                return response.ToList();
            }
        }
        public Task<IReadOnlyList<User>> GetByEntityIdAsync(int pageNumber, int pageSize, int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<SupportUserInfo>> GetFilteredUsersAsync(string FName, string LName, string Email, string Type, string Active, string AccountActivated)
        {
            string where = string.Empty;
            string query = string.Empty;
            if (!string.IsNullOrEmpty(FName))
            {
                where += "WHERE UPPER(FIRST_NAME) like '" + FName.ToUpper() + "%'";
            }
            if (!string.IsNullOrEmpty(LName))
            {
                if (where.Contains("WHERE"))
                    where += " AND  UPPER(LAST_NAME) like '" + LName.ToUpper() + "%'";
                else
                    where += "WHERE  UPPER(LAST_NAME) like '" + LName.ToUpper() + "%'";
            }
            if (!string.IsNullOrEmpty(Email))
            {
                if (where.Contains("WHERE"))
                    where += " AND  UPPER(EMAIL) like '" + Email.ToUpper() + "%'";
                else
                    where += "WHERE  UPPER(EMAIL) like '" + Email.ToUpper() + "%'";
            }
            if (!string.IsNullOrEmpty(Type))
            {
                if (Type == "S")
                {
                    if (where.Contains("WHERE"))
                        where += " AND (TYPE='" + Type + "' OR TYPE IS NULL)";
                    else
                        where += "WHERE  (TYPE='" + Type + "' OR TYPE IS NULL)";
                }
                else
                {
                    if (where.Contains("WHERE"))
                        where += " AND TYPE='" + Type + "'";
                    else
                        where += "WHERE TYPE='" + Type + "'";
                }
            }
            if (!string.IsNullOrEmpty(Active))
            {


                if (where.Contains("WHERE"))
                    where += " AND ACTIVE='" + Active + "'";
                else
                    where += "WHERE ACTIVE='" + Active + "'";

            }
            if (!string.IsNullOrEmpty(AccountActivated))
            {
                if (AccountActivated == "N")
                {
                    if (where.Contains("WHERE"))
                        where += " AND ACCOUNT_ACTIVATED='" + AccountActivated + "'";
                    else
                        where += "WHERE ACCOUNT_ACTIVATED='" + AccountActivated + "'";
                }

            }
            query = "SELECT * FROM WEB_AUTH.SUPPORT_TICKET_LOGIN_INFO " + where;
            var supportUsersInfo = DatabaseService.SelectCommandList<SupportUserInfo>(configuration, "SupportDBConnection", query);

            return supportUsersInfo;
        }

        public async Task<string> CreateSupportUser(SupportUserInfo supportUser, PayLoad payload)
        {
            string query = string.Empty;
            if (supportUser.Type != "SP" && supportUser.Type != "" && supportUser.Type != "S")
            {
                supportUser.View_Only = "N";
                if (supportUser.Type != "D")
                {
                    supportUser.Full_Access = "Y";
                }
                if (supportUser.Type != "IT" && supportUser.Type != "D")
                {
                    supportUser.Show_Support = "Y";
                    supportUser.Show_Implementation = "Y";
                }

            }
            else
                supportUser.Show_Implementation = "N";
            query = "SELECT EMAIL FROM WEB_AUTH.SUPPORT_TICKET_LOGIN_INFO WHERE EMAIL=:EMAIL";

            var ifExist = await DatabaseService.SelectCommandValue<string>(configuration, "SupportDBConnection", query, new object[] { "EMAIL", supportUser.Email });
            if (!string.IsNullOrEmpty(ifExist))
            {
                return "User with this email already exist";
            }
            else
            {
                if (supportUser.Seq_Num == 0)
                {
                    string token = string.Empty;
                    if (supportUser.Active == "Y")
                    {
                        query = "INSERT INTO WEB_AUTH.SUPPORT_TICKET_LOGIN_INFO(SEQ_NUM,FIRST_NAME,LAST_NAME,EMAIL,ACTIVE,CP,RESELLER,IS_2FAC,TYPE,ENTERED_BY,ENTRY_DATE,VIEW_ONLY,FULL_ACCESS,SHOW_SUPPORT,SHOW_IMPLEMENTATION,TOKEN,ACCOUNT_ACTIVATED,TEAM) VALUES (PRIMARY_SEQ.NEXTVAL,:FIRST_NAME,:LAST_NAME,:EMAIL,:ACTIVE,:CP,:RESELLER,:IS_2FAC,:TYPE,:ENTERED_BY,SYSDATE,:VIEW_ONLY,:FULL_ACCESS,:SHOW_SUPPORT,:SHOW_IMPLEMENTATION,:TOKEN,'N',:TEAM)";

                        var isInserted = await DatabaseService.InsertUpdateCommandList(configuration, "SupportDBConnection", query, new object[] { "FIRST_NAME", supportUser.First_Name, "LAST_NAME", supportUser.Last_Name, "EMAIL", supportUser.Email, "TOKEN", token, "ACTIVE", supportUser.Active, "CP", payload.CP, "RESELLER", payload.Reseller, "ENTERED_BY", payload.AppUser, "IS_2FAC", supportUser.Is_2Fac, "TYPE", supportUser.Type, "VIEW_ONLY", supportUser.View_Only, "FULL_ACCESS", supportUser.Full_Access, "SHOW_SUPPORT", supportUser.Show_Support, "SHOW_IMPLEMENTATION", supportUser.Show_Implementation, "TOKEN", token, "ENTERED_BY", supportUser.Entered_By, "TEAM", supportUser.Team });

                        if (isInserted > 0)
                        {
                            _emailService.SentUserCreationEmail(supportUser.Email, token);
                            return "User has been created successfully and activation email is sent on user's email";
                        }
                    }
                    else
                    {
                        query = "INSERT INTO WEB_AUTH.SUPPORT_TICKET_LOGIN_INFO(SEQ_NUM,FIRST_NAME,LAST_NAME,EMAIL,ACTIVE,CP,RESELLER,IS_2FAC,TYPE,ENTERED_BY,ENTRY_DATE,VIEW_ONLY,FULL_ACCESS,SHOW_SUPPORT,SHOW_IMPLEMENTATION,ACCOUNT_ACTIVATED,TEAM) VALUES (PRIMARY_SEQ.NEXTVAL,:FIRST_NAME,:LAST_NAME,:EMAIL,:ACTIVE,:CP,:RESELLER,:IS_2FAC,:TYPE,:ENTERED_BY,SYSDATE,:VIEW_ONLY,:FULL_ACCESS,:SHOW_SUPPORT,:SHOW_IMPLEMENTATION,'N',:TEAM)";

                        var isInserted = await DatabaseService.InsertUpdateCommandList(configuration, "SupportDBConnection", query, new object[] { "FIRST_NAME", supportUser.First_Name, "LAST_NAME", supportUser.Last_Name, "EMAIL", supportUser.Email, "ACTIVE", supportUser.Active, "CP", payload.CP, "RESELLER", payload.Reseller, "ENTERED_BY", payload.AppUser, "IS_2FAC", supportUser.Is_2Fac, "TYPE", supportUser.Type, "VIEW_ONLY", supportUser.View_Only, "FULL_ACCESS", supportUser.Full_Access, "SHOW_SUPPORT", supportUser.Show_Support, "SHOW_IMPLEMENTATION", supportUser.Show_Implementation, "ENTERED_BY", payload.AppUser, "TEAM", supportUser.Team });
                        if (isInserted > 0)
                        {
                            return "User has been created Successfully";
                        }
                    }

                }
                else
                {
                    if (supportUser.Active == "Y")
                    {
                        string token = string.Empty;
                        query = "UPDATE WEB_AUTH.SUPPORT_TICKET_LOGIN_INFO SET FIRST_NAME =:FIRST_NAME, LAST_NAME =:LAST_NAME,ACTIVE =:ACTIVE,CP=:CP,RESELLER=:RESELLER,IS_2FAC=:IS_2FAC,TYPE=:TYPE,ENTRY_DATE=SYSDATE,VIEW_ONLY=:VIEW_ONLY,FULL_ACCESS=:FULL_ACCESS,SHOW_SUPPORT=:SHOW_SUPPORT,SHOW_IMPLEMENTATION=:SHOW_IMPLEMENTATION,TOKEN=:TOKEN,TEAM=:TEAM WHERE SEQ_NUM =:SEQ_NUM";

                        var isUpdated = await DatabaseService.InsertUpdateCommandList(configuration, "SupportDBConnection", query, new object[] { "SEQ_NUM", supportUser.Seq_Num, "FIRST_NAME", supportUser.First_Name, "LAST_NAME", supportUser.Last_Name, "ACTIVE", supportUser.Active, "CP", payload.CP, "RESELLER", payload.Reseller, "IS_2FAC", supportUser.Is_2Fac, "TYPE", supportUser.Type, "VIEW_ONLY", supportUser.View_Only, "FULL_ACCESS", supportUser.Full_Access, "SHOW_SUPPORT", supportUser.Show_Support, "SHOW_IMPLEMENTATION", supportUser.Show_Implementation, "TOKEN", token, "TEAM", supportUser.Team });
                        if (isUpdated > 0)
                        {
                            _emailService.SentUserCreationEmail(supportUser.Email, token);
                            return "User has been updated successfully and activation email is sent on user's email";
                        }
                    }
                    else
                    {
                        query = "UPDATE WEB_AUTH.SUPPORT_TICKET_LOGIN_INFO SET FIRST_NAME =:FIRST_NAME, LAST_NAME =:LAST_NAME, ACTIVE =:ACTIVE,CP=:CP,RESELLER=:RESELLER,IS_2FAC=:IS_2FAC,TYPE=:TYPE,ENTRY_DATE=SYSDATE,VIEW_ONLY=:VIEW_ONLY,FULL_ACCESS=:FULL_ACCESS,SHOW_SUPPORT=:SHOW_SUPPORT,SHOW_IMPLEMENTATION=:SHOW_IMPLEMENTATION,TEAM=:TEAM WHERE SEQ_NUM =:SEQ_NUM";
                        var isUpdated = await DatabaseService.InsertUpdateCommandList(configuration, "SupportDBConnection", query, new object[] { "SEQ_NUM", supportUser.Seq_Num, "FIRST_NAME", supportUser.First_Name, "LAST_NAME", supportUser.Last_Name, "ACTIVE", supportUser.Active, "CP", payload.CP, "RESELLER", payload.Reseller, "IS_2FAC", supportUser.Is_2Fac, "TYPE", supportUser.Type, "VIEW_ONLY", supportUser.View_Only, "FULL_ACCESS", supportUser.Full_Access, "SHOW_SUPPORT", supportUser.Show_Support, "SHOW_IMPLEMENTATION", supportUser.Show_Implementation, "TEAM", supportUser.Team });
                        if (isUpdated > 0)
                        {
                            return "User has been updated successfully.";
                        }
                    }
                }
                throw new Exception("Error in SupportUserRepositoryAsync->CreateSupportUser()");
            }

        }

        public async Task<ClientsList> GetImplementationManager(long seqNum)
        {
            var query = "SELECT IMPLEMENTATION_MANAGER,IMPLEMENTATION_COORDINATOR FROM V_PEHR_CLIENTS_LIST WHERE SEQ_NUM = :SEQ_NUM ";
            return await DatabaseService.SelectCommandValue<ClientsList>(configuration, "SupportDBConnection", query,new object[] { "SEQ_NUM", seqNum });
        }
        public async Task<User> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<CreateRequestResponse> CreateLoginLogAsync(LoginLog loginLog)
        {
            var sql = "INSERT INTO PEHR_LOGIN_LOG (SEQ_NUM, LOGIN_USER, LOGIN_TIME, BROWSER_TYPE, RESOLUTION, DOWNLOAD_SPEED, UPLOAD_SPEED, AUTH_ENTITY_SEQ_NUM, BACKEND_VERSION, FRONTEND_VERSION,NPI) VALUES (PRIMARY_SEQ.NEXTVAL, :LOGIN_USER, SYSDATE, :BROWSER_TYPE, :RESOLUTION, :DOWNLOAD_SPEED, :UPLOAD_SPEED, :AUTH_ENTITY_SEQ_NUM, :BACKEND_VERSION, :FRONTEND_VERSION, :NPI) returning SEQ_NUM into: SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                DynamicParameters param = new DynamicParameters(loginLog);
                param.Add(name: "SEQ_NUM", dbType: DbType.Double, direction: ParameterDirection.Output);
                var result = await connection.ExecuteAsync(sql, param);
                var Id = param.Get<Double>("SEQ_NUM");
                return new CreateRequestResponse { Status = result, Id = Id };
            }
        }

        public async Task<int> UpdateLoginLogAsync(long lodinLodId)
        {
            var sql = "UPDATE WEB_AUTH.PEHR_LOGIN_LOG SET LOGOUT_TIME = SYSDATE WHERE LOGOUT_TIME IS NULL AND SEQ_NUM = :SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { SEQ_NUM = lodinLodId });
                return result;
            }
        }

        public async Task<SupportUserInfo> GetUserInfo(long seqNum)
        {
            string query = "SELECT * FROM SUPPORT_TICKET_LOGIN_INFO WHERE SEQ_NUM=:SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                var response = await connection.QueryFirstOrDefaultAsync<SupportUserInfo>(query, new { SEQ_NUM = seqNum });
                return response;
            }
        }

        public async Task<int> UpdateUserPassword(long seqNum,string password)
        {
            var query = "UPDATE WEB_AUTH.SUPPORT_TICKET_LOGIN_INFO SET PASSWORD =:PASSWORD WHERE SEQ_NUM =:SEQ_NUM";
            using(var connection=new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                var response = await connection.ExecuteAsync(query, new { PASSWORD = password ,SEQ_NUM=seqNum});
                return response;
            }
        }
    }
}

