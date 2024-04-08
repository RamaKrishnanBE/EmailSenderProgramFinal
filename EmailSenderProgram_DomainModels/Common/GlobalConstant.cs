using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderProgram_DomainModels.Common
{
    public static class GlobalConstant
    {
        public const string CONST_ERROR_LOG_MESSAGE_NULL = "Log message is empty. So unable to send the mail.";
        public const string CONST_ERROR_EMAIL_SERVER_SETTINGS = "Email server configurations are empty or invalid. Please check the email server settings in the config try again.";
        public const string CONST_ERROR_SCHEDULED_MAIL_NULL = "There is no scheduled emails. Please schedule mails in the config try again.";
        public const string CONST_ERROR_MAIL_TEMPLATE_NULL = "There is no email template available. Please add atleast one email template in the config try again.";
        public const string CONST_ERROR_UNKNOWN_EMAIL_SCHEDULED = "Unknown email type scheduled. Please check the scheduled email types in the config and try again.";
        public const string CONST_ERROR_EMAIL_TEMPLATE_NOT_FOUND = "There is no email template found for the email {0} in config. Please add the template for the mail type {0} and try again.";
        public const string CONST_EMAIL_SUBJECT_PLACEHOLDER = "Test mail subject";
        public const string CONST_EMAIL_GREETINGS_MESSAGE_PLACEHOLDER = "Hi ##email##";
        public const string CONST_EMAIL_BODY_CONTENT_PLACEHOLDER = "Welcome to Energy Opticon AB";
    }
}
