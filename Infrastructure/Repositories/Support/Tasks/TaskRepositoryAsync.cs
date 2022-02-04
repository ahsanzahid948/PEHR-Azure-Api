using Application.DTOs;
using Application.DTOs.Task;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Wrappers;
using Dapper;
using Domain.Entities;
using Domain.Entities.Auth;
using Domain.Entities.Task;
using FluentValidation;
using FluentValidation.Results;
using Infrastructure.Common.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TaskRepositoryAsync : ITaskRepositoryAsync
    {
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment _host;
        private IEmailService emailService;
        public TaskRepositoryAsync(IConfiguration config, IHostingEnvironment host, IEmailService _emailService)
        {
            configuration = config;
            _host = host;
            emailService = _emailService;
        }
        public Task<int> AddAsync(TaskViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<TaskViewModel>> GetAllAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<TaskViewModel> GetByFilterAsync(string filterCriteria)
        {
            throw new NotImplementedException();
        }

        public Task<TaskViewModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TaskViewModel> GetByIdAsync(double id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SupportTask>> GetClientTasksAsync(long entityseqnum, string status)
        {
            string SqlWhere = "";
            if (status == "I")
            {
                SqlWhere += " AND STATUS <> 'R'";
            }
            else
            {
                SqlWhere += " AND STATUS = 'R'";
            }
            string query = "SELECT SEQ_NUM, TICKET_NO, DESCRIPTION, SHORT_NAME ||'-'|| TICKET_NO AS SHORT_TICKET,TICKET_TYPE,STATUS,PRIORITY,ASSIGNEE,REPORTER,COMMENTS,ATTACHED_FILE,PARENT_TICKET,CREATED_DATE,LAST_UPDATE_DATE,LAST_UPDATE_BY,TICKET_TITLE,ENTITY_SEQ_NUM,SHORT_NAME,BROWSER_TYPE,RESOLUTION,DOWNLOAD_SPEED,UPLOAD_SPEED, TARGET_COMPLETION_DATE, UPPER(ASSIGNEE_SHORT_NAME) AS A_SHORT_NAME, UPPER(REPORTER_SHORT_NAME) AS R_SHORT_NAME, ACCOUNT_TYPE, TEAMNAME FROM WEB_V_SUPPORT_TASKS WHERE ENTITY_SEQ_NUM = :ENTITY_SEQ_NUM AND UPPER(TICKET_TYPE) = :TICKET_TYPE" + SqlWhere;
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                var response = await connection.QueryAsync<SupportTask>(query, new { ENTITY_SEQ_NUM = entityseqnum, TICKET_TYPE = "TRAINING AND IMPLEMENTATION" });

                foreach (var item in response.ToList())
                {
                    if (item.Target_Completion_Date != DateTime.MinValue && item.Status.ToString() != "R" && item.Status.ToString() != "Q" && (DateTime.Parse(item.Target_Completion_Date.ToString()).Date < DateTime.Now.Date))
                    {
                        item.Status = "J";
                    }
                }
                return response.ToList();
            }

        }

        public Task<int> UpdateAsync(TaskViewModel entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<SupportTask>> GetTasksAsync(TaskFilter obj)
        {
            obj.AppUser = !string.IsNullOrEmpty(obj.AppUser) ? obj.AppUser : "";
            obj.Reseller = !string.IsNullOrEmpty(obj.Reseller) ? obj.Reseller : "";
            obj.CP = !string.IsNullOrEmpty(obj.CP) ? obj.CP : "";
            string SqlWhere = " WHERE UPPER(TICKET_TYPE) = 'TRAINING AND IMPLEMENTATION'";
            string whereStatement = "";
            if (!string.IsNullOrEmpty(obj.Status))//Status Bracket
            {
                if (obj.Status == "P")
                {
                    SqlWhere += " AND (STATUS = 'O' OR STATUS = 'I')";
                }
                else if (obj.Status == "R")
                {
                    SqlWhere += " AND STATUS = 'R'";
                }
                else if (obj.Status == "Q")
                {
                    SqlWhere += " AND STATUS = 'Q'";
                }
                else if (obj.Status == "U")
                {
                    SqlWhere += " AND STATUS <> 'R'";
                }
                else
                {
                    SqlWhere += " AND TRUNC(SYSDATE) > TARGET_COMPLETION_DATE AND STATUS NOT IN ('R','Q')";
                }

            }
            if (obj.Reseller == "Y")//Reseller Bracket
            {
                SqlWhere += " AND UPPER(CHANNEL_PARTNER) = :CHANNEL_PARTNER";
            }
            if (!string.IsNullOrEmpty(obj.TxtAllKeyWords))
            {
                whereStatement = DatabaseService.SQLWhere("UPPER(ASSIGNEE_SHORT_NAME) LIKE", "%" + obj.Assignee + "%", "UPPER(INTERNAL_ASSIGNEE_SHORT_NAME) LIKE", "%" + obj.TxtInternalAssignee + "%", "UPPER(DESCRIPTION) LIKE", "%" + obj.PracticeName + "%", "UPPER(PRIORITY)", obj.Priority, "ENTITY_SEQ_NUM", obj.Client, "ENTITY_SEQ_NUM", obj.CustAccNum, "SUP_TICKET_TEAM_INFO_SEQ_NUM", obj.Team);
                SqlWhere += " AND (UPPER(COMMENTS) LIKE :K_COMMENTS OR UPPER(DESCRIPTION) LIKE :K_DESCRIPTION OR (SHORT_NAME || '-' || TICKET_NO) LIKE :K_SHORT_TICKET_NO) ";

            }
            else
            {
                whereStatement = DatabaseService.SQLWhere("UPPER(ASSIGNEE_SHORT_NAME) LIKE", "%" + obj.Assignee + "%", "UPPER(INTERNAL_ASSIGNEE_SHORT_NAME) LIKE", "%" + obj.TxtInternalAssignee + "%", "UPPER(DESCRIPTION) LIKE", "%" + obj.PracticeName + "%", "UPPER(PRIORITY)", obj.Priority, "ENTITY_SEQ_NUM", obj.Client, "ENTITY_SEQ_NUM", obj.CustAccNum, "SUP_TICKET_TEAM_INFO_SEQ_NUM", obj.Team);
            }
            if (whereStatement != "")
            {
                whereStatement = whereStatement.Replace("WHERE", "AND");
            }
            string query = "SELECT SEQ_NUM, TICKET_NO, DESCRIPTION AS ENTITY_DESCRIPTION, SHORT_NAME ||'-'|| TICKET_NO AS SHORT_TICKET,TICKET_TYPE,STATUS,PRIORITY,ASSIGNEE,REPORTER,COMMENTS,ATTACHED_FILE,PARENT_TICKET,CREATED_DATE,LAST_UPDATE_DATE,LAST_UPDATE_BY,TICKET_TITLE,ENTITY_SEQ_NUM,SHORT_NAME,BROWSER_TYPE,RESOLUTION,DOWNLOAD_SPEED,UPLOAD_SPEED, TARGET_COMPLETION_DATE, UPPER(ASSIGNEE_SHORT_NAME) AS A_SHORT_NAME, UPPER(REPORTER_SHORT_NAME) AS R_SHORT_NAME, ACCOUNT_TYPE, TEAMNAME,ACTIVE_FLAG, INTERNAL_ASSIGNEE_SHORT_NAME FROM WEB_V_SUPPORT_TASKS " + SqlWhere;
            obj.TxtAllKeyWords = !string.IsNullOrEmpty(obj.TxtAllKeyWords) ? obj.TxtAllKeyWords.ToUpper() : "";
            obj.CP = !string.IsNullOrEmpty(obj.CP) ? obj.CP : "";
            var param = new
            {
                K_COMMENTS = "%" + obj.TxtAllKeyWords + "%",
                K_DESCRIPTION = "%" + obj.TxtAllKeyWords + "%",
                K_SHORT_TICKET_NO = "%" + obj.TxtAllKeyWords + "%",
                CHANNEL_PARTNER = obj.CP
            };
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var response = await connection.QueryAsync<SupportTask>(query + whereStatement, param);
                foreach (var item in response.ToList())
                {
                    if (item.Target_Completion_Date != DateTime.MinValue && item.Status != "R" && item.Status != "Q" && (DateTime.Parse(item.Target_Completion_Date.ToString()).Date < DateTime.Now.Date))
                    {
                        item.Status = "J";
                    }
                }
                return response.ToList();
            }
        }

        public Task<TaskViewModel> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<TicketProgress_VM> TicketsProgressData(PayLoad payload, long entity)
        {

            var query = "SELECT PRACTICE_SETUP FROM ENTITY WHERE SEQ_NUM=:SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                var response = await connection.QueryAsync<Entity>(query, new { SEQ_NUM = entity });
                var dsPracticeSetup = await LoadPracticeProgressData(entity);
                var allTasks = await GetAllTasks(entity);
                var overdueTask = await GetOverDueTask(payload, entity);

                var result = new TicketProgress_VM();
                result.Progress_Data = dsPracticeSetup;
                result.All_Tasks = allTasks;
                result.Practice_Setup = response.ToList().Count > 0 && !string.IsNullOrEmpty(response.ToList()[0].Practice_Setup) ? response.ToList()[0].Practice_Setup.ToUpper() : string.Empty;
                result.Overdue_Tasks = overdueTask;
                return result;
            }
          

        }
        public async Task<List<SupportTicket>> GetAllTasks(long entity, string status = null)
        {
           
            string SqlWhere = string.Empty;
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                SqlWhere = " WHERE ENTITY_SEQ_NUM = :ENTITY_SEQ_NUM AND UPPER(TICKET_TYPE) = 'TRAINING AND IMPLEMENTATION'";
                if (status != null)
                {
                    if (status == "P")
                    {
                        SqlWhere += " AND (STATUS = 'O' OR STATUS = 'I')";
                    }
                    else if (status == "Q")
                    {
                        SqlWhere += " AND STATUS = 'Q'";
                    }
                    else
                    {
                        SqlWhere += " AND STATUS = 'R'";
                    }
                }
                string strQuery = "SELECT STATUS FROM SUPPORT_TICKETS" + SqlWhere;
                var response = await connection.QueryAsync<SupportTicket>(strQuery, new { ENTITY_SEQ_NUM = entity });
                return response.ToList();
            }
        }

        public async Task<List<PracticeProgressDetail>> LoadPracticeProgressData(long entitySeqNum)
        {
            var query = "SELECT ACCOUNT_TYPE,ENTRY_DATE,SEQ_NUM,STATUS,TITLE,LAST_COMMENT,TICKET_NO FROM V_PRACTICE_PROGRESS_DETAIL WHERE ENTITY_SEQ_NUM = :ENTITY_SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                var response = await connection.QueryAsync<PracticeProgressDetail>(query, new { ENTITY_SEQ_NUM = entitySeqNum });

                return response.ToList();

            }

        }
        public async Task<List<SupportTicket>> GetOverDueTask(PayLoad payload, long entitySeqNum)
        {

            string sqlWhere = string.Empty;
            if (entitySeqNum > 0)
            {
                sqlWhere = "AND ENTITY_SEQ_NUM = :ENTITY_SEQ_NUM";
            }
            if (payload.Reseller == "Y")
            {
                sqlWhere += " AND UPPER(CHANNEL_PARTNER) = :CHANNEL_PARTNER";
            }
            var query = "SELECT TICKET_NO FROM SUPPORT_TICKETS WHERE TRUNC(SYSDATE) > TARGET_COMPLETION_DATE " + sqlWhere + " AND UPPER(TICKET_TYPE) = :TICKET_TYPE AND STATUS NOT IN ('R','Q')";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                var response = await connection.QueryAsync<SupportTicket>(query, new { TICKET_TYPE = "TRAINING AND IMPLEMENTATION", ENTITY_SEQ_NUM = entitySeqNum });

                return response.ToList();

            }

        }
    }
}
