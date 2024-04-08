using EmailSenderProgram_DomainModels.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderProgram_EmailServices.Helpers
{
    public class EmailServerHelper
    {
        public static EmailServerSettings GetEmailServerSettings(IConfigurationSection emailServerSection)
        {
            EmailServerSettings emailServer = new EmailServerSettings();

            if (emailServerSection != null)
            {
                emailServer.email_server_name = emailServerSection.GetSection("email_server_name").Value ?? string.Empty;
                emailServer.smtp_host = emailServerSection.GetSection("smtp_host").Value ?? string.Empty;
                emailServer.smtp_port = emailServerSection.GetSection("smtp_port").Value ?? string.Empty;
                emailServer.sender_email = emailServerSection.GetSection("sender_email").Value ?? string.Empty;
                emailServer.sender_password = emailServerSection.GetSection("sender_password").Value ?? string.Empty;
                emailServer.enable_ssl = Convert.ToBoolean(emailServerSection.GetSection("enable_ssl").Value ?? "false");
                emailServer.use_default_credentials = Convert.ToBoolean(emailServerSection.GetSection("use_default_credentials").Value ?? "false");

            }
            //Get email server settings from config

            return emailServer;
        }
    }
}
