using Application.Exceptions;
using Application.Interfaces.Services;
using FluentValidation.Results;
using Google.Authenticator;
using Microsoft.Extensions.Configuration;
using PasswordGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Infrastructure.Common.Services
{
    public class PasswordService : IPasswordService
    {
        private IConfiguration configuration;

        public PasswordService(IConfiguration configuration)
        {
            this.configuration = configuration;

        }
        public string Base64Decode(string password)
        {
            password = password.Replace('-', '+');
            password = password.Replace('_', '/');
            password = password.PadRight(password.Length + (4 - password.Length % 4) % 4, '=');

            var data = Convert.FromBase64String(password);
            return Encoding.UTF8.GetString(data);
        }

        public bool IsBase64String(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return false;
            }

            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }

        public void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            IList<ValidationFailure> messages = new List<ValidationFailure>();

            if (password == null) messages.Add(new ValidationFailure("Password", "Password is null"));
            if (string.IsNullOrWhiteSpace(password)) messages.Add(new ValidationFailure("password", "Value cannot be empty or whitespace only string."));

            if (messages.Count > 0) throw new ValidationException(messages);

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = Convert.ToBase64String(hmac.Key);
                passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            IList<ValidationFailure> messages = new List<ValidationFailure>();

            if (password == null) messages.Add(new ValidationFailure("password", "Password is null"));
            if (string.IsNullOrWhiteSpace(password)) messages.Add(new ValidationFailure("password", "Value cannot be empty or whitespace only string."));
            if (storedHash.Length != 64) messages.Add(new ValidationFailure("passwordHash", "Invalid length of password hash (64 bytes expected)."));
            if (storedSalt.Length != 128) throw new ArgumentException("passwordHash", "Invalid length of password salt (128 bytes expected).");

            if (messages.Count > 0) throw new ValidationException(messages);

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }

        public string GeneratePassword(int passwordlength)
        {
            var pwd = new Password(passwordlength).IncludeLowercase().IncludeUppercase().IncludeNumeric().IncludeSpecial("[]{}^_=");
            return pwd.Next();
        }

        public bool VerifyPassword(string inputPassword, string dbPassword)
        {
            IList<ValidationFailure> messages = new List<ValidationFailure>();

            if (inputPassword == null) messages.Add(new ValidationFailure("password", "Password is null"));
            if (string.IsNullOrWhiteSpace(inputPassword)) messages.Add(new ValidationFailure("password", "Value cannot be empty or whitespace only string."));
           
            if (messages.Count > 0) throw new ValidationException(messages);

            return String.Equals(inputPassword, dbPassword);
            
        }
        public bool GenerateTwoFactorAuthentication(bool AuthkeyNotExist, string key, string email)
        {
            Guid guid = new Guid();
            String uniqueUserKey = "";
            string QRAuthenticationImage = "";
            string autoAuthenticationKey = "";
            string manualAuthenticationKey = "";
            configuration.GetSection("EMAIL");
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            if (AuthkeyNotExist)
            {
                guid = Guid.NewGuid();
                uniqueUserKey = Convert.ToString(guid).Replace("-", "").Substring(0, 10);
                var setupInfo = tfa.GenerateSetupCode("AdminSupportPortal", email, uniqueUserKey, false, 300);
                if (setupInfo != null)
                {
                    QRAuthenticationImage = setupInfo.QrCodeSetupImageUrl;
                    manualAuthenticationKey = setupInfo.ManualEntryKey;
                    autoAuthenticationKey = uniqueUserKey;


                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(configuration["EMAIL:FROM_EMAIL"], configuration["EMAIL:FROM_NAME"]);
                        mail.To.Add(email);

                        mail.Subject = "Support Portal Login QR";
                        mail.Body = "<div id='authenticateDiv'><h3> Google Authenticator </ h3 ><div><p >Step 1: Please download and install Google Authenticator on your IPhone/IPad/Android device, if already not installed. </p><p>Step 2: Link your device to your account. </p><p >Step 3: You have two options to link your device to your account. </p><p>1: Using QR Code</p><p>2: Using Secret Key</p></div>" +
                       "<div><img id ='imgQrCode' src=" + setupInfo.QrCodeSetupImageUrl + " alt='QR' />" +
                       "<div><span style = 'font-weight: bold; font - size: 14px; '> Account Name :" + email + "</ span ></div><span style = 'font-weight:bold; font - size: 14px; '> Secret Key:" + setupInfo.ManualEntryKey + "</span>";


                        mail.IsBodyHtml = true;

                        using (SmtpClient smtp = new SmtpClient(configuration["EMAIL:EMAIL_SMTP"].ToString(), Int32.Parse(configuration["EMAIL:EMAIL_SMTP_PORT"])))
                        {
                            smtp.Credentials = new NetworkCredential(userName: configuration["EMAIL:EMAIL_USER_NAME"], password: configuration["EMAIL:EMAIL_PASSWORD"]);
                            smtp.EnableSsl = Convert.ToBoolean(configuration["EMAIL:EMAIL_ENABLE_SSL"] == "Y" ? true : false);
                            smtp.Send(mail);
                        }
                    }

                    return true;
                }
            }
            else
            {
                QRAuthenticationImage = "";
                manualAuthenticationKey = "";
                autoAuthenticationKey = key;
                //cf5f7596ef
                TwoFactorAuthenticator tf = new TwoFactorAuthenticator();
                var b = tf.GetCurrentPIN(key);
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(configuration["EMAIL:FROM_EMAIL"], configuration["EMAIL:FROM_NAME"]);
                    mail.To.Add(email);

                    mail.Subject = "Support Portal Login Code";
                    mail.Body = "Login Authentication Code:<b>" + b + "</b> don't share it to any other one";
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient(configuration["EMAIL:EMAIL_SMTP"].ToString(), Int32.Parse(configuration["EMAIL:EMAIL_SMTP_PORT"])))
                    {
                        smtp.Credentials = new NetworkCredential(userName: configuration["EMAIL:EMAIL_USER_NAME"], password: configuration["EMAIL:EMAIL_PASSWORD"]);
                        smtp.EnableSsl = Convert.ToBoolean(configuration["EMAIL:EMAIL_ENABLE_SSL"] == "Y" ? true : false);
                        smtp.Send(mail);
                    }
                }


                return true;
            }



            return false;
        }
    }
}