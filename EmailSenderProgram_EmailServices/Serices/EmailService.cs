using EmailSenderProgram_DomainModels.Enums;
using EmailSenderProgram_DomainModels.Models;
using EmailSenderProgram_EmailServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderProgram_EmailServices.Serices
{
    public class EmailService : IEmailService
    {
        public bool SendEmail(string toAddress, EmailServerSettings emailServer, string emailSubject, string emailBody)
        {
            try
            {
                using (MailMessage mm = new MailMessage(emailServer.sender_email, toAddress))
                {
                    mm.Subject = emailSubject.Trim();
                    mm.Body = emailBody.Trim();
                    mm.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = emailServer.smtp_host;
                        smtp.EnableSsl = emailServer.enable_ssl;
                        NetworkCredential networkCred = new NetworkCredential(emailServer.sender_email, emailServer.sender_password);
                        smtp.UseDefaultCredentials = emailServer.use_default_credentials;
                        smtp.Credentials = networkCred;
                        smtp.Port = Convert.ToInt32(emailServer.smtp_port);
                        smtp.Send(mm);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured while sending the mail. Message: " + ex.Message);
                return false;
            }
        }
    }

}
