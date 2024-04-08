using EmailSenderProgram_DomainModels.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderProgram_EmailServices.Helpers
{
    public class LogMessageHelper
    {
        public static ApplicationLogMessages GetApplicationLogMessages(IConfigurationSection logMessageSection)
        {
            ApplicationLogMessages logMessages = new ApplicationLogMessages();

            if (logMessageSection != null)
            {
                logMessages.application_start_message = logMessageSection.GetSection("application_start_message").Value ?? string.Empty;
                logMessages.application_end_message = logMessageSection.GetSection("application_end_message").Value ?? string.Empty;
                logMessages.prior_to_send_first_mail = logMessageSection.GetSection("prior_to_send_first_mail").Value ?? string.Empty;
                logMessages.after_send_every_mail = logMessageSection.GetSection("after_send_every_mail").Value ?? string.Empty;
                logMessages.after_send_all_mails = logMessageSection.GetSection("after_send_all_mails").Value ?? string.Empty;
                logMessages.exception_message = logMessageSection.GetSection("exception_message").Value ?? string.Empty;
            }
            return logMessages;
        }
    }
}
