using Application.Interfaces.Repositories.Support;
using Domain.Entities;
using Domain.Entities.Notes;
using Domain.Entities.Support.Ticket;
using Infrastructure.Common.Services;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using Application.DTOs.Common.CreateRequestResponse;
using System.Data;

namespace Infrastructure.Repositories.Support
{
    public class CommentRepositoryAsync : ICommentRepositoryAsync
    {
        private readonly IConfiguration _configuration;
        public CommentRepositoryAsync(IConfiguration config)
        {
            _configuration = config;
        }
        public Task<int> AddAsync(SupportTicketComments entity)
        {
            throw new NotImplementedException();
        }
        public Task<int> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }
        public Task<IReadOnlyList<SupportTicketComments>> GetAllAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
        public Task<SupportTicketComments> GetByFilterAsync(string filterCriteria)
        {
            throw new NotImplementedException();
        }
        public Task<SupportTicketComments> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }
        public async Task<List<SupportTicketComments>> GetComments(long ticketNo)
        {
            var commentList = await DatabaseService.SelectCommandList<SupportTicketComments>(_configuration, "SupportDBConnection", "SELECT * FROM V_SUPPORT_TICKET_COMMENTS WHERE PARENT_TICKET = :PARENT_TICKET", new object[] { "PARENT_TICKET", ticketNo });
            //   var commentAttachment = await GetTicketCommentsAttachment(ticketNo, 0);
            return commentList.ToList();

        }
        public Task<int> UpdateAsync(SupportTicketComments entity)
        {
            throw new NotImplementedException();
        }
        public async Task<IReadOnlyList<SupportTicketAttachments>> GetCommentsAttachments(long ticketno, long commentSeqnum)
        {
            string query = "SELECT SEQ_NUM,FILE_PATH,FILE_NAME,COMMENT_SEQ_NUM,TICKET_NUMBER,ENTRY_DATE FROM WEB_AUTH.SUPPORT_TICKETS_ATTACHMENTS WHERE TICKET_NUMBER = :TICKET_NUMBER ORDER BY ENTRY_DATE ASC";

            if (commentSeqnum > 0)
                query += " AND COMMENT_SEQ_NUM = :COMMENT_SEQ_NUM";
            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection")))
            {
                var response = await connection.QueryAsync<SupportTicketAttachments>(query, new { TICKET_NUMBER = ticketno, COMMENT_SEQ_NUM = commentSeqnum });

                return response.ToList();
            }
        }
        public async Task<CreateRequestResponse> CreateComment(SupportTicketComments supportTicketComment)
        {
            var nextVal = await DatabaseService.SelectCommandValue<long>(_configuration, "SupportDBConnection", "SELECT TICKET_SEQ.NEXTVAL FROM DUAL");

            var query = "INSERT INTO WEB_AUTH.SUPPORT_TICKET_COMMENTS(SEQ_NUM,PARENT_TICKET,COMMENTS,ENTERED_BY,ENTERED_DATE) VALUES (TICKET_SEQ.NEXTVAL,:PARENT_TICKET,:COMMENTS,:ENTERED_BY,SYSDATE)";
            var isInserted = await DatabaseService.InsertUpdateCommandList(_configuration, "SupportDBConnection", query, new object[] { "PARENT_TICKET", supportTicketComment.Parent_Ticket, "COMMENTS", supportTicketComment.Comments, "ENTERED_BY", supportTicketComment.Entered_By });
            if (isInserted > 0)
            {
                return new CreateRequestResponse { Status = 1, Id = nextVal };
            }
            return new CreateRequestResponse { Status = 0, Id = 0 };
        }
        public async Task<int> UpdateComment(SupportTicketComments supportTicketComment)
        {
            var IsUpdated = await DatabaseService.InsertUpdateCommandList(_configuration, "SupportDBConnection", "UPDATE WEB_AUTH.SUPPORT_TICKET_COMMENTS SET PARENT_TICKET=:PARENT_TICKET,COMMENTS=:COMMENTS,ENTERED_BY=:ENTERED_BY,ENTERED_DATE=SYSDATE", new object[] { "PARENT_TICKET", supportTicketComment.Parent_Ticket, "COMMENTS", supportTicketComment.Comments, "ENTERED_BY", supportTicketComment.Entered_By });
            return IsUpdated;
        }
        public async Task<CreateRequestResponse> AddCommentsAttachment(string ticketNo, long commentSeqNo, string filePath, string fileName)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("TICKET_NUMBER", ticketNo);
            param.Add("COMMENT_SEQ_NUM", commentSeqNo);
            param.Add("FILE_PATH", filePath);
            param.Add("FILE_NAME", fileName);
            param.Add(name: "SEQ_NUM", dbType: DbType.Double, direction: ParameterDirection.Output);
            var sql = "INSERT INTO WEB_AUTH.SUPPORT_TICKETS_ATTACHMENTS(SEQ_NUM,TICKET_NUMBER,COMMENT_SEQ_NUM,FILE_PATH,FILE_NAME) VALUES(TICKET_SEQ.NEXTVAL,:TICKET_NUMBER,:COMMENT_SEQ_NUM,:FILE_PATH,:FILE_NAME) returning SEQ_NUM into :SEQ_NUM";

            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, param);
                var Id = param.Get<Double>("SEQ_NUM");
                return new CreateRequestResponse { Status = result, Id = Id };
            }
        }
    }
}
