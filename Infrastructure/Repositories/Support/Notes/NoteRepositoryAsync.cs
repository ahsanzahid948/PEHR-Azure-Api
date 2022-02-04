using Application.DTOs;
using Application.DTOs.Common.CreateRequestResponse;
using Application.DTOs.Notes;
using Application.Interfaces.Repositories;
using Dapper;
using Domain.Entities;
using Domain.Entities.Notes;
using Infrastructure.Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class NoteRepositoryAsync : INoteRepositoryAsync
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _host;
        public NoteRepositoryAsync(IConfiguration configuration, IHostingEnvironment hosting)
        {
            _configuration = configuration;
            _host = hosting;
        }

        public async Task<int> UpdateNote(NotesViewModel _clientNote)
        {
            var query = "UPDATE WEB_AUTH.SUPPORT_CLIENT_NOTES SET TYPE = :TYPE, COMMENTS = :COMMENTS, NOTE_DATE = :NOTE_DATE, RESOLVED = :RESOLVED, ASSIGNED_TO = :ASSIGNED_TO, FROM_TIME = :FROM_TIME, TO_TIME = :TO_TIME WHERE SEQ_NUM = :SEQ_NUM";
            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection")))
            {

                var response = await connection.ExecuteAsync(query, _clientNote);
                return response;
            }
        }

        public async Task<CreateRequestResponse> SaveNote(NotesViewModel _clientNotesViewModel)
        {
            _clientNotesViewModel.NotesId = CommonService.Assign(_clientNotesViewModel.NotesId);
            _clientNotesViewModel.AppUser = CommonService.Assign(_clientNotesViewModel.NotesId);
            _clientNotesViewModel.Comments = CommonService.Assign(_clientNotesViewModel.NotesId);
            _clientNotesViewModel.NoteDate = CommonService.Assign(_clientNotesViewModel.NotesId);
            _clientNotesViewModel.Type = CommonService.Assign(_clientNotesViewModel.NotesId);
            _clientNotesViewModel.AssignedTo = CommonService.Assign(_clientNotesViewModel.NotesId);
            _clientNotesViewModel.Resolved = CommonService.Assign(_clientNotesViewModel.NotesId);
            _clientNotesViewModel.FromTime = CommonService.Assign(_clientNotesViewModel.NotesId);
            _clientNotesViewModel.ToTime = CommonService.Assign(_clientNotesViewModel.NotesId);
            string query = "";
            int rowsInserted;

            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection")))
            {
                DynamicParameters dynamicParameters = new DynamicParameters(_clientNotesViewModel);
                dynamicParameters.Add(name: "SEQ_NUM", dbType: DbType.Double, direction: ParameterDirection.Output);

                query = "INSERT INTO WEB_AUTH.SUPPORT_CLIENT_NOTES(SEQ_NUM,ENTITY_SEQ_NUM,TYPE,COMMENTS,NOTE_DATE,RESOLVED,ASSIGNED_TO,ENTRY_DATE,ENTERED_BY,FROM_TIME,TO_TIME) VALUES (TICKET_SEQ.NEXTVAL,:ENTITY_SEQ_NUM,:TYPE,:COMMENTS,:NOTE_DATE,'N',:ASSIGNED_TO,SYSDATE,:ENTERED_BY,:FROM_TIME,:TO_TIME) returning SEQ_NUM into :SEQ_NUM";

                rowsInserted = await connection.ExecuteAsync(query, dynamicParameters);

                var id = dynamicParameters.Get<Double>("SEQ_NUM");

                if (rowsInserted > 0)
                {
                    return new CreateRequestResponse { Status = 1, Id = id };
                }

            }

            throw new Exception("Error Creating Note");

        }

        public bool SaveFile(byte[] file, double ticketNumber, double entitySeqNum, string fileExt, ref string filePath, ref string fileName)
        {
            try
            {
                string path = _host.ContentRootPath;
                path = path + "/" + entitySeqNum + "/" + ticketNumber;
                filePath = path;
                string fileN = string.Format(@"{0}{1}", DateTime.Now.Ticks, fileExt);
                if (!string.IsNullOrEmpty(fileName))
                {
                    fileN = string.Format(@"{0}{1}", fileName, fileExt);
                }
                fileName = fileN;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + "/" + fileN;
                File.WriteAllBytesAsync(path, file);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error Saving File To Server " + e.ToString());
            }
        }

        public bool AddNoteAttachment(double ticketNo, string commentSeqNum, string filePath, string fileName)
        {
            try
            {

                string strQuery = "INSERT INTO WEB_AUTH.SUPPORT_TICKETS_ATTACHMENTS(SEQ_NUM,TICKET_NUMBER,COMMENT_SEQ_NUM,FILE_PATH,FILE_NAME) ";
                strQuery += " VALUES(TICKET_SEQ.NEXTVAL,:TICKET_NUMBER,:COMMENT_SEQ_NUM,:FILE_PATH,:FILE_NAME)";
                using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection")))
                {
                    int isInserted = connection.Execute(strQuery, new { TICKET_NUMBER = ticketNo, COMMENT_SEQ_NUM = commentSeqNum, FILE_PATH = filePath, FILE_NAME = fileName });
                    if (isInserted > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<NotesView_VM> TicketClientNotes(PayLoad payload, long ticketno, long commentSeq)
        {
            payload.AppUser = payload.AppUser != null ? payload.AppUser : "";
            var query = "SELECT NOTE_DATE,TYPE,COMMENTS,ASSIGNED_TO,FROM_TIME,TO_TIME,RESOLVED,ATTACHED_FILE FROM WEB_AUTH.SUPPORT_CLIENT_NOTES WHERE SEQ_NUM = :SEQ_NUM";

            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection")))
            {
                var result = new NotesView_VM();

                if (commentSeq > 0)
                {
                    result.AssignedUser = await GetUserAssignee(payload);
                    return result;
                }

                var response = await connection.QueryFirstOrDefaultAsync<Notes>(query, new { SEQ_NUM = ticketno });
                result.AssignedUser = await GetUserAssignee(payload);
                result.ClientNote = response;
                return result;
            }
        }
        public async Task<List<SupportUserInfo>> GetUserAssignee(PayLoad payload)
        {
            payload.AppUser = payload.AppUser != null ? payload.AppUser : "";
            payload.Reseller = payload.Reseller != null ? payload.Reseller : "";
            payload.CP = payload.CP != null ? payload.CP : "";
            string sqlWhere = " WHERE ACTIVE= 'Y'";
            if (payload.Reseller == "Y")
            {
                sqlWhere += " AND UPPER(CP) = :CP";
            }
            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection")))
            {
                string query = "SELECT LOWER(EMAIL) AS EMAIL,UPPER(FIRST_NAME || ' ' || LAST_NAME) AS SHORT_NAME FROM SUPPORT_TICKET_LOGIN_INFO " + sqlWhere + " ORDER BY SHORT_NAME ASC";
                var response = await connection.QueryAsync<SupportUserInfo>(query, new { CP = payload.CP });

                return response.ToList();
            }
        }
        public async Task<List<SupportTicketAttachments>> GetNoteAttachments(long ticketno, long commentSeqnum)
        {
            string query = "SELECT SEQ_NUM,FILE_PATH,FILE_NAME,COMMENT_SEQ_NUM,TICKET_NUMBER,ENTRY_DATE FROM WEB_AUTH.SUPPORT_TICKETS_ATTACHMENTS WHERE TICKET_NUMBER = :TICKET_NUMBER";

            if (commentSeqnum > 0)
                query += " AND COMMENT_SEQ_NUM = :COMMENT_SEQ_NUM";
            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection")))
            {
                var response = await connection.QueryAsync<SupportTicketAttachments>(query, new { TICKET_NUMBER = ticketno, COMMENT_SEQ_NUM = commentSeqnum });

                return response.ToList();
            }
        }

        public async Task<List<Notes>> GetAllNotes(NotesFilter notesFilter, long entitySeqNum, string ddlNoteType_ClientDetail)
        {
            string sqlWhere = !string.IsNullOrEmpty(ddlNoteType_ClientDetail) ? " AND TYPE = :TYPE" : "";
            string query = "SELECT DATE,TYPE,COMMENTS,RESOLVED,ASSIGNED_TO,ENTERED_BY,ENTRY_DATE FROM WEB_AUTH.SUPPORT_CLIENT_NOTES WHERE ENTITY_SEQ_NUM = :ENTITY_SEQ_NUM" + sqlWhere;
            notesFilter.AppUser = !string.IsNullOrEmpty(notesFilter.AppUser) ? notesFilter.AppUser : "";
            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Notes>(query, new { ENTITY_SEQ_NUM = entitySeqNum });
                return result.ToList();
            }

        }

        public Task<NotesViewModel> GetByFilterAsync(string filterCriteria)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<NotesViewModel>> GetAllAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(NotesViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(NotesViewModel entity)
        {
            throw new NotImplementedException();
        }

        public async Task<NotesViewModel> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }
        public async Task<Notes> GetNotesByIdAsync(long id)
        {
            string query = "SELECT DATE,TYPE,COMMENTS,RESOLVED,ASSIGNED_TO,ENTERED_BY,ENTRY_DATE FROM WEB_AUTH.SUPPORT_CLIENT_NOTES WHERE SEQ_NUM = :SEQ_NUM";
            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportDBConnection")))
            {
                var response = await connection.QueryFirstOrDefaultAsync<Notes>(query, new { SEQ_NUM = id });
                return response;
            }
        }

        public Task<int> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}
