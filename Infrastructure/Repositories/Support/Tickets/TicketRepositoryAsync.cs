using Application.DTOs.Ticket;
using Application.Interfaces.Repositories;
using Dapper;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Common.Services;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Web;
using Application.DTOs.Support.Ticket;
using Domain.Entities.Notes;
using Domain.Entities.Support.Ticket;
using Application.DTOs.Support.ViewModels.TicketViewModel;
using AutoMapper;
using Application.DTOs.Notes;
using Application.Interfaces.Services;
using Domain.Entities.Support;
using FluentValidation.Results;
using FluentValidation;
using Application.DTOs.Common.CreateRequestResponse;
using System.Data;

namespace Infrastructure.Repositories
{
    public class TicketRepositoryAsync : ITicketRepositoryAsync
    {

        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment host;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public TicketRepositoryAsync(IConfiguration config, IHostingEnvironment _host, IMapper mapper, IEmailService emailService)
        {
            _configuration = config;
            host = _host;
            _mapper = mapper;
            _emailService = emailService;
        }
        public async Task<IReadOnlyList<SupportTicket>> SupportTicketsListAsync(TicketFilter entity)
        {
            string SqlWhere = "";
            string TicketType = "";
            string StatementWhere = "";
            DynamicParameters param;
            entity.Status = entity.Status != "U" ? entity.Status : "";
            entity.UnResolvedFlag = entity.Status == "U" ? true : false;
            // CommonService.DoQueryFiltering("/users?first_name=Obaid&Id=30&Cust_Acc_Num=23&Priority=N&sort_by=desc(last_modified~first_name),asc(email)");
            entity.AppUser = !string.IsNullOrEmpty(entity.AppUser) ? entity.AppUser : "";

            if (entity.Reseller == "Y")
            {
                SqlWhere += " AND UPPER(CHANNEL_PARTNER) = :CHANNEL_PARTNER";
            }
            if (!string.IsNullOrEmpty(entity.ParentTicket))
            {
                SqlWhere += " AND (TICKET_NO<>:PARENT_TICKET AND (PARENT_TICKET<>:PARENT_TICKET OR PARENT_TICKET is NULL)) AND STATUS <> 'R'";
            }
            if (!string.IsNullOrEmpty(entity.SearchKeyWords))
            {
                StatementWhere = DatabaseService.SQLWhereParameterized(out param, "STATUS", entity.Status, "UPPER(ASSIGNEE_SHORT_NAME) LIKE", "%" + entity.Assignee + "%", "UPPER(INTERNAL_ASSIGNEE_SHORT_NAME) LIKE", "%" + entity.InternalAssignee + "%", "UPPER(DESCRIPTION) LIKE", "%" + entity.PracticeName + "%", "UPPER(PRIORITY)", entity.Priority, "ENTITY_SEQ_NUM", entity.Client, "ENTITY_SEQ_NUM", entity.EntitySeqNum, "SUP_TICKET_TEAM_INFO_SEQ_NUM", entity.Team);
                if (StatementWhere == "")
                    TicketType = " WHERE ((UPPER(COMMENTS) LIKE :K_COMMENTS) OR (UPPER(DESCRIPTION) LIKE :K_DESCRIPTION) OR ((SHORT_NAME || '-' || TICKET_NO) LIKE :K_SHORT_TICKET_NO)) ";
                else
                    TicketType = " AND ((UPPER(COMMENTS) LIKE :K_COMMENTS) OR (UPPER(DESCRIPTION) LIKE :K_DESCRIPTION) OR ((SHORT_NAME || '-' || TICKET_NO) LIKE :K_SHORT_TICKET_NO)) ";
            }
            else
            {
                StatementWhere = DatabaseService.SQLWhereParameterized(out param, "STATUS", entity.Status, "UPPER(ASSIGNEE_SHORT_NAME) LIKE", "%" + entity.Assignee + "%", "UPPER(INTERNAL_ASSIGNEE_SHORT_NAME) LIKE", "%" + entity.InternalAssignee + "%", "UPPER(DESCRIPTION) LIKE", "%" + entity.PracticeName + "%", "UPPER(PRIORITY)", entity.Priority, "ENTITY_SEQ_NUM", entity.Client, "ENTITY_SEQ_NUM", entity.EntitySeqNum, "SUP_TICKET_TEAM_INFO_SEQ_NUM", entity.Team);
            }
            if (string.IsNullOrEmpty(entity.Category))
            {
                TicketType += SqlWhere == "" && TicketType == "" ? "  UPPER(TICKET_TYPE) IN ('IMPLEMENTATION','ACCOUNT AND BILLING SUPPORT','TECHNICAL SUPPORT', 'CLINICAL')" : " AND UPPER(TICKET_TYPE) IN ('IMPLEMENTATION','ACCOUNT AND BILLING SUPPORT','TECHNICAL SUPPORT', 'CLINICAL')";
            }
            else if (entity.Category != null && entity.Category.ToUpper() == "SUPPORT")
            {
                TicketType += SqlWhere == "" && TicketType == "" ? "  UPPER(TICKET_TYPE) IN ('ACCOUNT AND BILLING SUPPORT','TECHNICAL SUPPORT', 'CLINICAL')" : " AND UPPER(TICKET_TYPE) IN ('ACCOUNT AND BILLING SUPPORT','TECHNICAL SUPPORT', 'CLINICAL')";
            }
            else
            {
                TicketType += SqlWhere == "" && TicketType == "" ? "  UPPER(TICKET_TYPE) = :C_TICKET_TYPE" : " AND UPPER(TICKET_TYPE) = :C_TICKET_TYPE";
            }
            if (string.IsNullOrEmpty(entity.SearchKeyWords) && string.IsNullOrEmpty(entity.Status) && string.IsNullOrEmpty(entity.Assignee) && string.IsNullOrEmpty(entity.InternalAssignee) && string.IsNullOrEmpty(entity.PracticeName) && string.IsNullOrEmpty(entity.Priority) && string.IsNullOrEmpty(entity.Category) && string.IsNullOrEmpty(entity.Client) && string.IsNullOrEmpty(entity.EntitySeqNum) && string.IsNullOrEmpty(entity.Team))
            {
                entity.UnResolvedFlag = true;
            }
            if (entity.UnResolvedFlag)
            {
                TicketType += " AND STATUS <> 'R' ";
            }
            if (entity.FromDate != DateTime.MinValue)
                TicketType += " AND TRUNC(CREATED_DATE) >= :FROM_CREATED_DATE";
            if (entity.Suspended == "N")
                SqlWhere += " AND (SUSPENDED_FLAG = 'N' OR SUSPENDED_FLAG is null)";
            else if (entity.Suspended == "Y")
                SqlWhere += " AND SUSPENDED_FLAG = 'Y'";
            if (entity.ToDate != DateTime.MinValue)
                TicketType += " AND TRUNC(CREATED_DATE) <= :TO_CREATED_DATE";

            var keys = new object[]
                 {
                        "K_COMMENTS", "%" + CommonService.Upper(entity.SearchKeyWords) + "%",
                        "K_DESCRIPTION", "%" + CommonService.Upper(entity.SearchKeyWords) + "%",
                        "K_SHORT_TICKET_NO","%" + CommonService.Upper(entity.SearchKeyWords)+ "%",
                        "CHANNEL_PARTNER", CommonService.Upper(entity.CP)
                 };

            for (int i = 0; i < keys.Length; i = i + 2)
            {
                param.Add(keys[i].ToString(), keys[i + 1].ToString());
            }
            string query = "SELECT TICKET_NO, SHORT_NAME || '-' || TICKET_NO AS SHORT_TICKET, STATUS, PRIORITY,TICKET_CATEGORY, COMMENTS, DESCRIPTION AS ENTITY_DESCRIPTION, CREATED_DATE, LAST_UPDATE_DATE, UPPER(ASSIGNEE_SHORT_NAME) AS A_SHORT_NAME, UPPER(REPORTER_SHORT_NAME) AS R_SHORT_NAME, RESOLVED_DAYS, LAST_ACTION_TIME, ACCOUNT_TYPE,SUSPENDED_FLAG,TEAMNAME,ACTIVE_FLAG,TICKET_TYPE, ENTITY_SEQ_NUM, INTERNAL_ASSIGNEE_SHORT_NAME FROM SUPPORT_TICKETS WHERE ";

            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<SupportTicket>(query + TicketType + StatementWhere + SqlWhere, param);
                return result.ToList();
            }
        }
        public async Task<IReadOnlyList<SupportInbox>> GetInboxListAsync(long userSeqNum, InboxFilter inboxFilter)
        {
            inboxFilter.AppUser = !string.IsNullOrEmpty(inboxFilter.AppUser) ? inboxFilter.AppUser : "";
            string query = "SELECT TICKET_NO,TYPE,EMAIL_BLOB,VIEW_STATUS,ENTERED_DATE FROM V_SUPPORT_INBOX WHERE USER_SEQ_NUM=:USER_SEQ_NUM";

            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var response = await connection.QueryAsync<SupportInbox>(query, new { USER_SEQ_NUM = userSeqNum });
                dynamic file;



                foreach (var item in response)
                {
                    file = item.Email_Text_Blob != null ? item.Email_Text_Blob : null;
                    string root = host.ContentRootPath + "/EmailTemplate";
                    DateTime now = DateTime.Now;
                    string path = root.Replace("\\", "/") + now.ToShortDateString().Replace("/", "") + now.Hour + now.Second + "";
                    if (file != null)
                    {
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        File.WriteAllBytes(path, file);
                        item.Email_Blob_Text_Js = File.ReadAllText(path);
                        File.Delete(path);
                    }

                    //      string path = root + DateTime.Now.ToShortDateString().Replace("/", "") + now.Hour + now.Second + "\\";
                }
                return response.ToList();
            }

        }
        public async Task<List<SupportTicket>> SearchTicketsAsync(TicketFilter ticketFilter)
        {


            ticketFilter.Status = ticketFilter.Status != "U" ? ticketFilter.Status : "";
            ticketFilter.UnResolvedFlag = ticketFilter.UnResolvedFlag.ToString() == "U" ? true : false;
            ticketFilter.Assignee = !string.IsNullOrEmpty(ticketFilter.Assignee) ? ticketFilter.Assignee.ToUpper() : "";
            ticketFilter.PracticeName = !string.IsNullOrEmpty(ticketFilter.PracticeName) ? ticketFilter.PracticeName.ToUpper() : "";
            ticketFilter.Priority = !string.IsNullOrEmpty(ticketFilter.Priority) ? ticketFilter.Priority.ToUpper() : "";
            ticketFilter.Reseller = !string.IsNullOrEmpty(ticketFilter.Reseller) ? ticketFilter.Reseller.ToUpper() : "";
            ticketFilter.InternalAssignee = !string.IsNullOrEmpty(ticketFilter.InternalAssignee) ? ticketFilter.InternalAssignee.ToUpper() : "";
            ticketFilter.AppUser = !string.IsNullOrEmpty(ticketFilter.AppUser) ? ticketFilter.AppUser : "";
            ticketFilter.CP = !string.IsNullOrEmpty(ticketFilter.CP) ? ticketFilter.CP : "";
            ticketFilter.SearchKeyWords = !string.IsNullOrEmpty(ticketFilter.SearchKeyWords) ? ticketFilter.SearchKeyWords.ToUpper() : "";
            ticketFilter.Category = !string.IsNullOrEmpty(ticketFilter.Category) ? ticketFilter.Category.ToUpper() : "";
            ticketFilter.CP = !string.IsNullOrEmpty(ticketFilter.CP) ? ticketFilter.CP.ToUpper() : "";



            string statement = "";
            string SqlWhere = "";
            string ticketType = "";
            if (ticketFilter.Reseller == "Y")
            {
                SqlWhere += " AND UPPER(CHANNEL_PARTNER) = :CHANNEL_PARTNER";
            }
            if (!string.IsNullOrEmpty(ticketFilter.ParentTicket))
            {
                SqlWhere += " AND (TICKET_NO<>:PARENT_TICKET AND (PARENT_TICKET<>:PARENT_TICKET OR PARENT_TICKET is NULL)) AND STATUS <> 'R'";
            }
            if (!string.IsNullOrEmpty(ticketFilter.SearchKeyWords))
            {
                statement = DatabaseService.SQLWhere("STATUS", ticketFilter.Status, "UPPER(ASSIGNEE_SHORT_NAME) LIKE", "%" + ticketFilter.Assignee + "%", "UPPER(INTERNAL_ASSIGNEE_SHORT_NAME) LIKE", "%" + ticketFilter.InternalAssignee + "%", "UPPER(DESCRIPTION) LIKE", "%" + ticketFilter.PracticeName + "%", "UPPER(PRIORITY)", ticketFilter.Priority, "ENTITY_SEQ_NUM", ticketFilter.Client, "ENTITY_SEQ_NUM", ticketFilter.EntitySeqNum, "SUP_TICKET_TEAM_INFO_SEQ_NUM", ticketFilter.Team);
                ticketType = statement == "" ? " WHERE ((UPPER(COMMENTS) LIKE :K_COMMENTS) OR (UPPER(DESCRIPTION) LIKE :K_DESCRIPTION) OR ((SHORT_NAME || '-' || TICKET_NO) LIKE :K_SHORT_TICKET_NO)) " : " AND ((UPPER(COMMENTS) LIKE :K_COMMENTS) OR (UPPER(DESCRIPTION) LIKE :K_DESCRIPTION) OR ((SHORT_NAME || '-' || TICKET_NO) LIKE :K_SHORT_TICKET_NO)) ";
            }
            else
            {
                statement = DatabaseService.SQLWhere("STATUS", ticketFilter.Status, "UPPER(ASSIGNEE_SHORT_NAME) LIKE", "%" + ticketFilter.Assignee + "%", "UPPER(INTERNAL_ASSIGNEE_SHORT_NAME) LIKE", "%" + ticketFilter.InternalAssignee + "%", "UPPER(DESCRIPTION) LIKE", "%" + ticketFilter.PracticeName + "%", "UPPER(PRIORITY)", ticketFilter.Priority, "ENTITY_SEQ_NUM", ticketFilter.Client, "ENTITY_SEQ_NUM", ticketFilter.EntitySeqNum, "SUP_TICKET_TEAM_INFO_SEQ_NUM", ticketFilter.Team);
            }
            if (string.IsNullOrEmpty(ticketFilter.Category))
            {
                ticketType += statement == "" && ticketType == "" ? " WHERE UPPER(TICKET_TYPE) IN ('IMPLEMENTATION','ACCOUNT AND BILLING SUPPORT','TECHNICAL SUPPORT', 'CLINICAL')" : " AND UPPER(TICKET_TYPE) IN ('IMPLEMENTATION','ACCOUNT AND BILLING SUPPORT','TECHNICAL SUPPORT', 'CLINICAL')";
            }
            else if (!string.IsNullOrEmpty(ticketFilter.Category) && ticketFilter.Category.ToUpper() == "SUPPORT")
            {
                ticketType += statement == "" && ticketType == "" ? " WHERE UPPER(TICKET_TYPE) IN ('ACCOUNT AND BILLING SUPPORT','TECHNICAL SUPPORT', 'CLINICAL')" : " AND UPPER(TICKET_TYPE) IN ('ACCOUNT AND BILLING SUPPORT','TECHNICAL SUPPORT', 'CLINICAL')";
            }
            else
            {
                ticketType += statement == "" && ticketType == "" ? " WHERE UPPER(TICKET_TYPE) = :C_TICKET_TYPE" : " AND UPPER(TICKET_TYPE) = :C_TICKET_TYPE";
            }
            if (string.IsNullOrEmpty(ticketFilter.SearchKeyWords) && string.IsNullOrEmpty(ticketFilter.Status) && string.IsNullOrEmpty(ticketFilter.Assignee) && string.IsNullOrEmpty(ticketFilter.InternalAssignee) && string.IsNullOrEmpty(ticketFilter.PracticeName) && string.IsNullOrEmpty(ticketFilter.Priority) && string.IsNullOrEmpty(ticketFilter.Category) && string.IsNullOrEmpty(ticketFilter.Client) && string.IsNullOrEmpty(ticketFilter.EntitySeqNum) && string.IsNullOrEmpty(ticketFilter.Team))
            {
                ticketFilter.UnResolvedFlag = true;
            }
            if (ticketFilter.UnResolvedFlag)
            {
                ticketType += " AND STATUS <> 'R' ";
            }
            if (ticketFilter.FromDate != DateTime.MinValue)
                ticketType += " AND TRUNC(CREATED_DATE) >= :FROM_CREATED_DATE";
            if (ticketFilter.Suspended == "N")
                SqlWhere += " AND (SUSPENDED_FLAG = 'N' OR SUSPENDED_FLAG is null)";
            else if (ticketFilter.Suspended == "Y")
                SqlWhere += " AND SUSPENDED_FLAG = 'Y'";
            if (ticketFilter.ToDate != DateTime.MinValue)
                ticketType += " AND TRUNC(CREATED_DATE) <= :TO_CREATED_DATE";


            string query = "SELECT TICKET_NO, SHORT_NAME || '-' || TICKET_NO AS SHORT_TICKET, STATUS, PRIORITY,TICKET_CATEGORY, COMMENTS, DESCRIPTION AS ENTITY_DESCRIPTION, CREATED_DATE, LAST_UPDATE_DATE, UPPER(ASSIGNEE_SHORT_NAME) AS A_SHORT_NAME, UPPER(REPORTER_SHORT_NAME) AS R_SHORT_NAME, RESOLVED_DAYS, LAST_ACTION_TIME, ACCOUNT_TYPE,SUSPENDED_FLAG,TEAMNAME,ACTIVE_FLAG, ENTITY_SEQ_NUM, INTERNAL_ASSIGNEE_SHORT_NAME FROM SUPPORT_TICKETS ";

            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var response = await connection.QueryAsync<SupportTicket>(query + statement + ticketType + SqlWhere, new
                {
                    K_COMMENTS =
                    "%" + ticketFilter.SearchKeyWords + "%",
                    K_DESCRIPTION =
                    "%" + ticketFilter.SearchKeyWords + "%",
                    K_SHORT_TICKET_NO =
                    "%" + ticketFilter.SearchKeyWords + "%",
                    C_TICKET_TYPE =
                    ticketFilter.Category,
                    FROM_CREATED_DATE =
                    ticketFilter.FromDate,
                    TO_CREATED_DATE =
                    ticketFilter.ToDate,
                    CHANNEL_PARTNER =
                    ticketFilter.CP,
                    PARENT_TICKET =
                    ticketFilter.ParentTicket
                });

                return response.ToList();
            }

        }
        public async Task<List<SupportTicket>> QuickSearchTicketsAsync(SearchFilter searchTicketFilter)
        {
            searchTicketFilter.TxtQuickSearch = string.IsNullOrEmpty(searchTicketFilter.TxtQuickSearch) ? "" : searchTicketFilter.TxtQuickSearch.ToUpper();
            searchTicketFilter.Reseller = string.IsNullOrEmpty(searchTicketFilter.Reseller) ? "" : searchTicketFilter.Reseller.ToUpper();
            searchTicketFilter.AppUser = string.IsNullOrEmpty(searchTicketFilter.AppUser) ? "" : searchTicketFilter.AppUser;
            searchTicketFilter.CP = string.IsNullOrEmpty(searchTicketFilter.CP) ? "" : searchTicketFilter.CP.ToUpper();

            string txtStatusSearch = searchTicketFilter.TxtQuickSearch.Replace("OPEN", "O").Replace("IN PROGRESS", "I").Replace("PENDING ON CLIENT", "Q").Replace("RESOLVED", "R").Replace("REOPENED", "K");
            string SqlWhere = "";
            if (searchTicketFilter.Reseller == "Y")
            {
                SqlWhere += " AND UPPER(CHANNEL_PARTNER) = :CHANNEL_PARTNER";
            }
            string query = "SELECT SEQ_NUM,TICKET_NO, DESCRIPTION AS ENTITY_DESCRIPTION, SHORT_NAME ||'-'|| TICKET_NO AS SHORT_TICKET,TICKET_TYPE,STATUS,PRIORITY,TICKET_CATEGORY,ASSIGNEE,REPORTER,COMMENTS,ATTACHED_FILE,PARENT_TICKET,CREATED_DATE,LAST_UPDATE_DATE,LAST_UPDATE_BY,TICKET_TITLE,ENTITY_SEQ_NUM,SHORT_NAME,BROWSER_TYPE,RESOLUTION,DOWNLOAD_SPEED,UPLOAD_SPEED, UPPER(ASSIGNEE_SHORT_NAME) AS A_SHORT_NAME, UPPER(REPORTER_SHORT_NAME) AS R_SHORT_NAME, ACCOUNT_TYPE, TEAMNAME, ACTIVE_FLAG FROM SUPPORT_TICKETS WHERE UPPER(TICKET_TYPE)  <> 'TRAINING AND IMPLEMENTATION' AND ((SHORT_NAME || '-' || TICKET_NO) LIKE :SHORT_TICKET_NO OR UPPER(COMMENTS) LIKE :COMMENTS OR UPPER(ASSIGNEE_SHORT_NAME) LIKE :ASSIGNEE_SHORT_NAME OR UPPER(REPORTER_SHORT_NAME) LIKE :REPORTER_SHORT_NAME OR UPPER(PRIORITY) LIKE :PRIORITY OR UPPER(DESCRIPTION) LIKE :DESCRIPTION OR UPPER(STATUS) = :STATUS OR UPPER(PRACTICE_NAME) LIKE :PRACTICE_NAME )" + SqlWhere;
            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var response = await connection.QueryAsync<SupportTicket>(query, new
                {
                    SHORT_TICKET_NO =
                    "%" + searchTicketFilter.TxtQuickSearch + "%",
                    COMMENTS =
                    "%" + searchTicketFilter.TxtQuickSearch + "%",
                    ASSIGNEE_SHORT_NAME =
                    "%" + searchTicketFilter.TxtQuickSearch + "%",
                    REPORTER_SHORT_NAME =
                    "%" + searchTicketFilter.TxtQuickSearch + "%",
                    PRIORITY =
                    "%" + searchTicketFilter.TxtQuickSearch + "%",
                    DESCRIPTION =
                    "%" + searchTicketFilter.TxtQuickSearch + "%",
                    STATUS =
                    txtStatusSearch,
                    PRACTICE_NAME =
                    "%" + searchTicketFilter.TxtQuickSearch + "%",
                    CHANNEL_PARTNER =
                    searchTicketFilter.CP
                });
                return response.ToList();
            }

        }
        public async Task<TicketStatusViewModel> SupportDashBoardStatus(SearchFilter searchTicketFilter)
        {
            var query = "SELECT * FROM V_TICKETS_STATUS_COUNT WHERE UPPER(TICKET_TYPE) = 'SUPPORT'";
            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection")))
            {
                var response = await connection.QueryAsync<TicketStatusViewModel>(query);
                if (response != null)
                {
                    //Get Priorty Count
                    //Get Status Count
                    //Get Assignee Count
                    //Get Team Count
                    // return response;

                }
            }
            throw new NotImplementedException();
        }
        public async Task<List<SupportTicketChangeHistory>> GetTicketHistory(long ticketNo)
        {
            var ticketCommentHistory = await DatabaseService.SelectCommandList<SupportTicketChangeHistory>(_configuration, "SupportDBConnection", "SELECT * FROM SUPPORT_TICKET_CHANGE_HISTORY WHERE PARENT_TICKET = :PARENT_TICKET", new object[] { "PARENT_TICKET", ticketNo });
            return ticketCommentHistory;

        }
        public async Task<SupportTicket> GetByIdAsync(long id)
        {
            var query = "SELECT * FROM SUPPORT_TICKETS WHERE TICKET_NO=:TICKET_NO";
            var response = await DatabaseService.SelectCommandValue<SupportTicket>(_configuration, "SupportDBConnection", query, new object[] { "TICKET_NO", id });
            return response;
        }
        Task<SupportTicket> IGenericRepositoryAsync<SupportTicket>.GetByFilterAsync(string filterCriteria)
        {
            throw new NotImplementedException();
        }
        Task<IReadOnlyList<SupportTicket>> IGenericRepositoryAsync<SupportTicket>.GetAllAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
        public Task<int> AddAsync(SupportTicket entity)
        {
            throw new NotImplementedException();
        }
        public async Task<int> UpdateAsync(SupportTicket entity)
        {
            throw new NotImplementedException();
        }
        public async Task<int> CreateTicketResolutionAsync(SupportTicketResolution ticketResolution)
        {
            var query = "INSERT INTO WEB_AUTH.SUPPORT_TICKET_RESOLUTIONS(SEQ_NUM,TICKET_NO,DESCRIPTION,COMMENTS,ENTERED_BY,ENTRY_DATE) VALUES (PRIMARY_SEQ.NEXTVAL,:TICKET_NO,:DESCRIPTION,:COMMENTS,:ENTERED_BY,SYSDATE)";
            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var isCreated = await connection.ExecuteAsync(query, ticketResolution);
                return isCreated;
            }
        }
        public async Task<int> UpdateTicketResolutionAsync(SupportTicketResolution ticketResolution)
        {
            var query = "UPDATE WEB_AUTH.SUPPORT_TICKET_RESOLUTIONS SET DESCRIPTION =:DESCRIPTION, COMMENTS =:COMMENTS WHERE SEQ_NUM =:SEQ_NUM";
            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var isUpdated = await connection.ExecuteAsync(query, ticketResolution);
                return isUpdated;
            }
        }
        public async Task<int> InsertTicketHistory(string parentTicket, string type, string oldValue, string newValue, string appUser)
        {
            string query = "INSERT INTO SUPPORT_TICKET_CHANGE_HISTORY(SEQ_NUM,PARENT_TICKET,TYPE,OLD_VALUE,NEW_VALUE,ENTERED_BY,ENTERED_DATE) VALUES (TICKET_SEQ.NEXTVAL, :PARENT_TICKET, :TYPE, :OLD_VALUE, :NEW_VALUE, :ENTERED_BY, SYSDATE)";
            return await DatabaseService.InsertUpdateCommandList(_configuration, "SupportDBConnection", query, new object[] { "PARENT_TICKET", parentTicket, "TYPE", type, "OLD_VALUE", oldValue, "NEW_VALUE", newValue, "ENTERED_BY", appUser });
        }
        public async Task<PracticeSetup> GetTicketAssignee(long practiceSeqNum)
        {
            string query = " SELECT ASSIGN_TO,EMAIL_TO,TITLE FROM WEB_AUTH.PRACTICE_SETUP_LIST WHERE SEQ_NUM = :SEQ_NUM";
            return await DatabaseService.SelectCommandValue<PracticeSetup>(_configuration, "SupportDBConnection", query, new object[] { "SEQ_NUM", practiceSeqNum });
        }
        public Task<int> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }
        public async Task<int> UpdateSupportTicket(SupportTicket ticket)
        {
            var query = "UPDATE WEB_AUTH.SUPPORT_TICKETS SET LAST_UPDATE_BY=:LAST_UPDATE_BY, LAST_UPDATE_DATE = SYSDATE, PRIORITY=:PRIORITY, SUSPENDED_FLAG=:SUSPENDED_FLAG, STATUS=:STATUS WHERE TICKET_NO=:TICKET_NO";
            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var isInserted = await connection.ExecuteAsync(query, ticket);
            }
            return 0;
        }
        public async Task<CreateRequestResponse> SaveTicketAsync(SupportTicket supportTicket)
        {
            string insertQuery = string.Empty;
            string insertParam = string.Empty;
            if (supportTicket.Sup_Ticket_team_Info_Seq_Num > 0)
            {
                insertQuery = ",SUB_TICKET_SEQ_NUM";
                insertParam = ",:SUB_TICKET_SEQ_NUM";
            }
            string query = "INSERT INTO WEB_AUTH.SUPPORT_TICKETS(SEQ_NUM,TICKET_NO,TICKET_TYPE,STATUS,PRIORITY,ASSIGNEE,REPORTER,COMMENTS,ATTACHED_FILE,ENTITY_SEQ_NUM,TARGET_COMPLETION_DATE,TASK_TYPE,ASSIGNEETEAM" + insertQuery + ") VALUES (TICKET_SEQ.NEXTVAL,TICKET_SEQ.NEXTVAL,:TICKET_TYPE,:STATUS,:PRIORITY,:ASSIGNEE,:REPORTER,:COMMENTS,:ATTACHED_FILE,:ENTITY_SEQ_NUM,:TARGET_COMPLETION_DATE,:TASK_TYPE,:ASSIGNEETEAM" + insertParam + ") returning SEQ_NUM into :SEQ_NUM";
            DynamicParameters dynamicParameters = new(supportTicket);
            dynamicParameters.Add(name: "SEQ_NUM", dbType: DbType.Double, direction: ParameterDirection.Output);
            supportTicket.Comments = !string.IsNullOrEmpty(supportTicket.Comments) ? supportTicket.Comments : "";
            supportTicket.Assignee = !string.IsNullOrEmpty(supportTicket.Assignee) ? supportTicket.Assignee : "";
            supportTicket.Sup_Ticket_team_Info_Seq_Num = supportTicket.Sup_Ticket_team_Info_Seq_Num;
            supportTicket.AssigneeTeam = !string.IsNullOrEmpty(supportTicket.AssigneeTeam) ? supportTicket.AssigneeTeam : "";
            supportTicket.Priority = !string.IsNullOrEmpty(supportTicket.Priority) ? supportTicket.Priority : "";
            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();

                int isInserted = await connection.ExecuteAsync(query, dynamicParameters);
                double getId = dynamicParameters.Get<Double>("SEQ_NUM");

                if (isInserted > 0)
                {

                    return new CreateRequestResponse { Status = 1, Id = getId };
                }
                return new CreateRequestResponse { Status = 0, Id = 0 };
            }

        }
        public async Task<SupportTicketResolution> GetSupportTicketResolutionAsync(long resolutionId)
        {
            var query = "SELECT SEQ_NUM ,Ticket_No, Description ,Comments,Entry_Date ,Entered_By from WEB_AUTH.SUPPORT_TICKET_RESOLUTIONS WHERE SEQ_NUM=:" + resolutionId;
            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var ticketResolution = await connection.QueryFirstOrDefaultAsync<SupportTicketResolution>(query);
                return ticketResolution;
            }
        }
        public async Task<List<SupportTicket>> GetPEHRDashboardTickets(long practiceSeqNum, long entitySeqNum)
        {
            var query = "SELECT PRACTICE_SETUP_LIST_SEQ_NUM, REPORTER, RESOLUTION, SEQ_NUM, STATUS, TICKET_NO, TICKET_TITLE, TICKET_TYPE FROM WEB_AUTH.SUPPORT_TICKETS WHERE PRACTICE_SETUP_LIST_SEQ_NUM =:PRACTICE_SETUP_LIST_SEQ_NUM AND ENTITY_SEQ_NUM = :ENTITY_SEQ_NUM";
            var response = await DatabaseService.SelectCommandList<SupportTicket>(_configuration, "SupportDBConnection", query, new object[] { "PRACTICE_SETUP_LIST_SEQ_NUM", practiceSeqNum, "ENTITY_SEQ_NUM", entitySeqNum });
            return response;
        }
    }
}
