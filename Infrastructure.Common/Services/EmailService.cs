using Application.Interfaces.Services;
using Dapper;
using Domain.Entities;
using Domain.Entities.Group_Email;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Common.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _host;


        public EmailService(IConfiguration configuration, IHostingEnvironment host)
        {
            _configuration = configuration;
            _host = host;

        }
        public void SendTicketEmail(string ticketNumber, string emailType, string oldAssignee, string oldinternalassignee, string newinternalassignee,
            string oldStatus, string newStatus, string oldteamassignee, string currentteamassignee, string emailSubject, SupportTicket supportTicket, ref bool isSentToReporter)
        {
            try
            {
                IList<ValidationFailure> messages = new List<ValidationFailure>();
                messages.Add(new ValidationFailure("Error in Sending Ticket", "Error in Email Service"));
                bool mailSentToImpMgr = false;
                string mailBody = string.Empty;
                List<string> mailToList = new List<string>();

                if (supportTicket.Comments.Contains("\n"))
                {
                    supportTicket.Comments = supportTicket.Comments.Replace("\n", "<br>") + "<br>";
                }
                if (supportTicket.Ticket_Type == "COMMENTS")
                {
                    mailBody += "<br><b>Comments Added</b> (" + supportTicket.Reporter + ") <hr/>";
                    mailBody += supportTicket.Comments + "<hr/>";
                }
                else if (supportTicket.Ticket_Type == "ASSIGNEE")
                {
                    mailBody += " <br><b>Assignee Detail</b><hr/>";
                    mailBody += "From : " + oldAssignee + "<br>" + "To : " + supportTicket.Assignee + "<br>";
                    if (!string.IsNullOrEmpty(supportTicket.Comments))
                    {
                        mailBody += "<br><b>Comments Added</b> (" + supportTicket.Reporter + ") <hr/>";
                        mailBody += supportTicket.Comments;
                    }
                    mailBody += "<hr/>";
                }
                else if (supportTicket.Ticket_Type == "INTERNAL ASSIGNEE")
                {
                    mailBody += " <br><b>Internal Assignee Detail</b><hr/>";
                    mailBody += "From : " + oldinternalassignee + "<br>" + "To : " + supportTicket.Reporter + "<br>";
                    if (!string.IsNullOrEmpty(supportTicket.Comments))
                    {
                        mailBody += "<br><b>Comments Added</b> (" + supportTicket.Reporter + ") <hr/>";
                        mailBody += supportTicket.Comments;
                    }
                    mailBody += "<hr/>";
                }
                else if (supportTicket.Ticket_Type == "NOTIFICATIONTYPE")
                {
                    mailBody += " <br><b>Notification Type</b><hr/>";
                    mailBody += "From : " + oldStatus + "<br>" + "To : " + newStatus + "<br>";
                    if (!string.IsNullOrEmpty(supportTicket.Comments))
                    {
                        mailBody += "<br><b>Comments Added</b> (" + supportTicket.Reporter + ") <hr/>";
                        mailBody += supportTicket.Comments;
                    }
                    mailBody += "<hr/>";
                }
                else if (supportTicket.Ticket_Type == "TEAM")
                {
                    mailBody += " <br><b>Team Detail</b><hr/>";
                    mailBody += "From : " + oldteamassignee + "<br>" + "To : " + currentteamassignee + "<br>";
                    if (!string.IsNullOrEmpty(supportTicket.Comments))
                    {
                        mailBody += "<br><b>Comments Added</b> (" + supportTicket.Reporter + ") <hr/>";
                        mailBody += supportTicket.Comments;
                    }
                    mailBody += "<hr/>";
                }
                else if (supportTicket.Ticket_Type == "NEWTASK")
                {
                    mailBody += " <br><b>Task Created</b><hr/>";
                    mailBody += ticketNumber + "<br>";
                    mailBody += "<hr/>";
                }
                else
                {
                    mailBody += " <br><b>Status Detail</b><hr/>";
                    mailBody += "From : " + oldStatus + "<br>" + "To : " + newStatus + "<br>";
                    if (!string.IsNullOrEmpty(supportTicket.Comments))
                    {
                        mailBody += "<br><b>Comments Added</b> (" + supportTicket.Reporter + ") <hr/>";
                        mailBody += supportTicket.Comments;
                    }
                    mailBody += "<hr/>";
                }
                mailBody += "Key : " + ticketNumber + " <br>";
                mailBody += "Customer Id : " + supportTicket.Entity_Seq_Num + " <br>";
                mailBody += "Practice Name : " + supportTicket.Description + " <br>";
                mailBody += "Project : PEHR <br>";
                mailBody += supportTicket.Category == "TRAINING AND IMPLEMENTATION" ? "" : "Category :  " + supportTicket.Category + " <br>";
                mailBody += "Reported By :  " + supportTicket.Reporter + " <br>";
                mailBody += "Assignee :  " + supportTicket.Assignee + " <br>";
                mailBody += "Team :  " + supportTicket.AssigneeTeam + " <br>";
                mailBody += "Internal Assignee :  " + supportTicket.Internal_Assignee + " <br>";
                mailBody += "Priority :  " + supportTicket.Priority + " <br>";
                mailBody += "Description :  " + supportTicket.Comments.Replace(">", "&gt;").Replace("<", "&lt;").Replace("\n", "<br>") + " <br><br><br>";
                mailBody += "*Disclaimer: PLEASE DO NOT REPLY TO THIS EMAIL.This e-mail was sent from a notification-only address that cannot accept incoming mail.  <br><br>";


                if (!string.IsNullOrEmpty(supportTicket.Assignee))
                    setMailPreferences(ref mailToList, supportTicket.Assignee, emailType, mailBody, supportTicket);
                if (!string.IsNullOrEmpty(oldAssignee) && oldAssignee != supportTicket.Assignee)
                    setMailPreferences(ref mailToList, oldAssignee, emailType, mailBody, supportTicket);
                if (!string.IsNullOrEmpty(newinternalassignee))
                    setMailPreferences(ref mailToList, newinternalassignee, emailType, mailBody, supportTicket);
                if (!string.IsNullOrEmpty(oldinternalassignee) && oldinternalassignee != newinternalassignee)
                    setMailPreferences(ref mailToList, oldinternalassignee, emailType, mailBody, supportTicket);
                if (!string.IsNullOrEmpty(currentteamassignee))
                    setMailPreferences(ref mailToList, currentteamassignee, emailType, mailBody, supportTicket);
                if (!string.IsNullOrEmpty(oldteamassignee) && oldteamassignee != currentteamassignee)
                    setMailPreferences(ref mailToList, oldteamassignee, emailType, mailBody, supportTicket);
                if (mailToList.Count == 1 && string.IsNullOrEmpty(mailToList[0]) && !string.IsNullOrEmpty(supportTicket.Implementation_Manager) && (supportTicket.Category.ToUpper() == "IMPLEMENTATION" || supportTicket.Category.ToUpper() == "TRAINING AND IMPLEMENTATION"))
                    setMailPreferences(ref mailToList, supportTicket.Implementation_Manager, emailType, mailBody, supportTicket);
                if (!string.IsNullOrEmpty(supportTicket.Implementation_Coordinator) && !mailToList.Contains(supportTicket.Implementation_Coordinator) && (supportTicket.Category.ToUpper() == "IMPLEMENTATION" || supportTicket.Category.ToUpper() == "TRAINING AND IMPLEMENTATION"))
                    setMailPreferences(ref mailToList, supportTicket.Implementation_Coordinator, emailType, mailBody, supportTicket);

                foreach (string mailTo in mailToList)
                {
                    if (!string.IsNullOrEmpty(mailTo))
                    {
                        using (MailMessage mail = new MailMessage())
                        {

                            mail.From = new MailAddress(_configuration["EMAIL:FROM_EMAIL"], _configuration["EMAIL:FROM_NAME"]);
                            mail.To.Add(mailTo);
                            mail.Subject = supportTicket.Category != null && supportTicket.Category.ToUpper() == "TRAINING AND IMPLEMENTATION" ? emailSubject.Replace("Ticket", "Task") : emailSubject;
                            mail.Body = mailBody;
                            mail.IsBodyHtml = true;
                            //ADD CC Section to be Implemented
                            if (!string.IsNullOrEmpty(supportTicket.Assignee) && mailTo == supportTicket.Assignee)
                            {
                                var dsEmailGroups = GetMailToGroups(mailTo);
                                foreach (var item in dsEmailGroups)
                                {
                                    if (!string.IsNullOrEmpty(item.Email))
                                    {

                                        mail.CC.Add(item.Email);

                                    }
                                }
                            }
                            if (isSentToReporter && !string.IsNullOrEmpty(supportTicket.Reporter) && !string.IsNullOrEmpty(supportTicket.Comments) && (!string.IsNullOrEmpty(supportTicket.Category) && supportTicket.Category.ToUpper() == "TRAINING AND IMPLEMENTATION") && mailTo.ToUpper() != supportTicket.Reporter.ToUpper())
                            {
                                if (!mailToList.Contains(supportTicket.Reporter) && !mail.CC.Contains(new MailAddress(supportTicket.Reporter)) && !mail.Bcc.Contains(new MailAddress(supportTicket.Reporter)))
                                    setMailPreferences(ref mailToList, supportTicket.Reporter, emailType, mailBody, supportTicket);
                                isSentToReporter = true;
                            }


                            if (!mailSentToImpMgr && !string.IsNullOrEmpty(supportTicket.Implementation_Manager) && (CommonService.Upper(supportTicket.Category) == "IMPLEMENTATION" || CommonService.Upper(supportTicket.Category) == "TRAINING AND IMPLEMENTATION") && mailTo.ToUpper() != CommonService.Upper(supportTicket.Implementation_Manager) && emailType != "ASSIGNEE")
                            {
                                if (!mailToList.Contains(supportTicket.Implementation_Manager) && !mail.CC.Contains(new MailAddress(supportTicket.Implementation_Manager)) && !mail.Bcc.Contains(new MailAddress(supportTicket.Implementation_Manager)))
                                {
                                    var user = getUserPreferencesSettings(supportTicket.Implementation_Manager, emailType);
                                    if (user != null)
                                    {
                                        if (user.Notification_Type == "B")
                                        {
                                            mailToList.Add(supportTicket.Implementation_Manager);
                                            LogInboxEntry(supportTicket, Convert.ToString(user.Seq_Num), mailBody);
                                        }
                                        else if (user.Notification_Type == "I")
                                        {
                                            LogInboxEntry(supportTicket, Convert.ToString(user.Seq_Num), mailBody);
                                        }
                                        else
                                        {
                                            mailSentToImpMgr = true;
                                            mailToList.Add(supportTicket.Implementation_Manager);
                                        }

                                    }
                                    else
                                    {
                                        mailSentToImpMgr = true;
                                        mailToList.Add(supportTicket.Implementation_Manager);
                                    }
                                }
                            }
                            using (SmtpClient smtp = new SmtpClient(_configuration["EMAIL:EMAIL_SMTP"], Int32.Parse(_configuration["EMAIL:EMAIL_SMTP_PORT"])))
                            {
                                smtp.Credentials = new NetworkCredential(_configuration["EMAIL:EMAIL_USER_NAME"], _configuration["EMAIL:EMAIL_PASSWORD"]);
                                smtp.EnableSsl = _configuration["EMAIL:EMAIL_ENABLE_SSL"].ToString() != "Y" ? false : true;
                                smtp.Send(mail);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Email Service ->SendTicketEmail()" + e.ToString());
            }
        }
        public List<Groups> GetMailToGroups(string email)
        {
            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportConnection")))
            {
                var result = connection.Query<Groups>("SELECT G.EMAIL FROM GROUPS G, GROUP_EMAILS GE WHERE G.SEQ_NUM = GE.GROUP_SEQ_NUM AND GE.EMAIL = :EMAIL", new { EMAIL = email });

                return (List<Groups>)result;
            }
        }
        public void setMailPreferences(ref List<string> mailToList, string user, string emailType, string emailbody, SupportTicket supportTicket)
        {
            UserPreferences userPreference = getUserPreferencesSettings(user, emailType);
            if (!string.IsNullOrEmpty(userPreference.Notification_Type))
            {
                if (userPreference.Notification_Type == "B")
                {
                    mailToList.Add(user);
                    LogInboxEntry(supportTicket, Convert.ToString(userPreference.Seq_Num), emailbody);

                }
                else if (userPreference.Notification_Type == "I")
                {
                    LogInboxEntry(supportTicket, Convert.ToString(userPreference.Seq_Num), emailbody);
                }
                else
                {
                    mailToList.Add(user);
                }
            }
            mailToList.Add(user);
        }
        public UserPreferences getUserPreferencesSettings(string email, string type)
        {
            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportConnection")))
            {
                connection.Open();
                var response = connection.QuerySingle<UserPreferences>("SELECT * FROM SUPPORT_USER_PREFRENCES WHERE TYPE=:TYPE AND ENTERED_BY=:ENTERED_BY", new { ENTERED_BY = email, TYPE = type });

                return response;
            }
        }
        public async void LogInboxEntry(SupportTicket supportObj, string seqnum, string emailbody)
        {

            emailbody = emailbody.Replace("*Disclaimer: PLEASE DO NOT REPLY TO THIS EMAIL.This e-mail was sent from a notification-only address that cannot accept incoming mail.  <br><br>", "");
            emailbody = emailbody.Replace(">", "&gt;").Replace("<", "&lt;").Replace("\n", "<br><br>") + " <br><br><br>";
            string root = _host.ContentRootPath + "/EmailTemplate";
            DateTime now = DateTime.Now;
            byte[] blob = null;
            string path = root.Replace("\\", "/") + now.ToShortDateString().Replace("/", "") + now.Hour + now.Second + "";
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filepath = path + supportObj.Ticket_No + ".text";
                File.WriteAllText(filepath, emailbody);
                blob = File.ReadAllBytes(filepath);
                File.Delete(filepath);
            }
            catch (Exception e)
            {
                //   messages.Add(new ValidationFailure("Log Inbox Entry", "File ReadWrite Exception " + e.ToString()));
                //     throw new ValidationException(messages);
            }
            var query = "INSERT INTO WEB_AUTH.SUPPORT_INBOX(SEQ_NUM,VIEW_STATUS,USER_PREF_SEQ_NUM,TICKET_NO,EMAIL_TEXT_BLOB,ENTERED_BY,ENTERED_DATE) VALUES(PRIMARY_SEQ.NEXTVAL,:VIEW_STATUS,:USER_PREF_SEQ_NUM,:TICKET_NO,:EMAIL_TEXT_BLOB,:ENTERED_BY,SYSDATE)";
            using (var connection = new OracleConnection(_configuration.GetConnectionString("SupportConnection")))
            {
                connection.Open();
                int isInserted = await connection.ExecuteAsync(query, new { USER_PREF_SEQ_NUM = seqnum, VIEW_STATUS = 'N', TICKET_NO = supportObj.Ticket_No, EMAIL_TEXT_BLOB = blob, ENTERED_BY = supportObj.Reporter });
                if (isInserted <= 0)
                {
                    //       messages.Add(new ValidationFailure("Log Inbox Error", "QueryError"));
                }
            }

        }

        public void SentUserCreationEmail(string email, string token)
        {
            string activationLink = _configuration["EMAIL:ACTIVATION_URL"];
            if (activationLink.IndexOf('?') > 0)
                activationLink = activationLink + "&email=" + email + "&token=" + token;
            else
                activationLink = activationLink + "?email=" + email + "?token=" + token;

            string msgbody = "Hello " + email + ", <br><br>";
            msgbody += "Please follow the instructions below:.<br><br>Clink the link below to activate your account: <br>" + activationLink;
            msgbody += "<br><br>For your reference, your login is " + email + " for logging in.";
            msgbody += "<br> <br>Sincerely, <br> PracticeEHR Support";
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(_configuration[""], _configuration[""]);
                mail.To.Add(email);
                mail.Subject = "Support Account Activation";
                mail.Body = msgbody;
                mail.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient(_configuration["EMAIL:EMAIL_SMTP"], int.Parse(_configuration["EMAIL:EMAIL_SMTP_PORT"])))
                {
                    smtp.Credentials = new NetworkCredential(_configuration["EMAIL:EMAIL_USER_NAME"], _configuration["EMAIL:EMAIL_PASSWORD"]);
                    smtp.EnableSsl = _configuration["EMAIL_ENABLE_SSL"] == "Y" ? true : false;
                    smtp.Send(mail);
                }
            }
        }

        public void SendServeyEmail(string ticketReporterEmail, string ticketReporter, string customerId, string TicketNo)
        {
            var s = _configuration["SUPPORTSURVERY"];

            if (s != null && bool.Parse(s))
            {
                string strbody = string.Empty;
                strbody += "Hello " + ticketReporter + ", <br>";
                strbody += "<br>";
                strbody += "We're running a survey and would love to have your input. Please let us know what you think by filling the survey on clicking this button below. <br>";
                strbody += "<br>";
                strbody += "<a style='text-decoration:none;padding:2px;background:#4284db;color: white;border-radius: 3px 3px 3px 3px;' href='https://www.surveymonkey.com/r/69P9SQD?CustomerID=" + customerId + "&Ticket=" + TicketNo + "' target='_blank'> Go To Survey</a>";
                strbody += "<br>";
                strbody += "<br>";
                strbody += "Thanks,<br>";
                strbody += "<br>";
                strbody += "Practice EHR Team <br>";
                if (!string.IsNullOrEmpty(ticketReporterEmail))
                {
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(_configuration["EMAIL:FROM_EMAIL"], _configuration["EMAIL:FROM_NAME"]);
                        mail.To.Add(ticketReporterEmail);
                        mail.Subject = "Customer Service Feedback";
                        mail.Body = strbody;
                        mail.IsBodyHtml = true;
                        using (SmtpClient smtp = new SmtpClient(_configuration["EMAIL:EMAIL_SMTP"], Int32.Parse(_configuration["EMAIL:EMAIL_SMTP_PORT"])))
                        {
                            smtp.Credentials = new NetworkCredential(_configuration["EMAIL:EMAIL_USER_NAME"], _configuration["EMAIL:EMAIL_PASSWORD"]);
                            smtp.EnableSsl = bool.Parse(_configuration["EMAIL_ENABLE_SSL"]);
                            smtp.Send(mail);
                        }
                    }
                }
            }
        }

        public void NotifyUser(SupportTicket supportTicket, string ticketNewComment)
        {
            string strbody = string.Empty;
            if (ticketNewComment.Contains("\n"))
            {
                ticketNewComment = ticketNewComment.Replace("\n", "<br>") + "<br>";
            }

            strbody += "<br><b>Comments Added</b><hr/>";
            strbody += ticketNewComment + "<hr/>";
            strbody += "Key : " + supportTicket.Ticket_No + " <br>";
            strbody += "Practice Name : " + supportTicket.Description + " <br>";
            strbody += "Project : PEHR <br>";
            strbody += "Category :  " + supportTicket.Ticket_Type + " <br>";
            strbody += "Reported By :  " + supportTicket.Reporter_Short_Name + " <br>";
            strbody += "Priority :  " + supportTicket.Priority + " <br>";
            strbody += "Description :  " + supportTicket.Description + " <br><br><br>";
            strbody += "*Disclaimer: PLEASE DO NOT REPLY TO THIS EMAIL.This e-mail was sent from a notification-only address that cannot accept incoming mail.  <br><br>";

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(_configuration["EMAIL:FROM_EMAIL"], _configuration["EMAIL:FROM_NAME"]);
                mail.To.Add(supportTicket.Implementation_Contact);
                mail.Subject = "PracticeEHR - Ticket (" + supportTicket.Ticket_No + ")";
                mail.Body = strbody;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient(_configuration["EMAIL:EMAIL_SMTP"], Int32.Parse(_configuration["EMAIL:EMAIL_SMTP_PORT"])))
                {
                    smtp.Credentials = new NetworkCredential(_configuration["EMAIL:EMAIL_USER_NAME"], _configuration["EMAIL:EMAIL_PASSWORD"]);
                    smtp.EnableSsl = bool.Parse(_configuration["EMAIL:EMAIL_ENABLE_SSL"]);
                    smtp.Send(mail);
                }
            }
        }
    }
}
