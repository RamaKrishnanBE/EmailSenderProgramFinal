using EmailSenderProgram_DomainModels.Enums;
using EmailSenderProgram_DomainModels.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderProgram_EmailServices.Helpers
{
    public class EmailHelper
    {

        public static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        public static List<ScheduledEmail> GetScheduledEmailTypes(IConfigurationSection scheduledMailsSection)
        {
            List<ScheduledEmail> scheduledEmails = new List<ScheduledEmail>();
            if (scheduledMailsSection != null)
            {
                for (int i = 0; i < scheduledMailsSection.GetChildren().Count(); i++)
                {
                    var scheduled_email_section = scheduledMailsSection.GetSection(i.ToString());
                    if (scheduled_email_section != null)
                    {
                        scheduledEmails.Add(new ScheduledEmail()
                        {
                            email_type = scheduled_email_section.GetSection("email_type").Value ?? string.Empty,
                            email_recurrence_type = scheduled_email_section.GetSection("email_recurrence_type").Value ?? string.Empty,
                            day_to_send_weekly_mail = scheduled_email_section.GetSection("day_to_send_weekly_mail").Value ?? string.Empty

                        });
                    }
                }

            }

            return scheduledEmails;
        }
        public static List<EmailTemplate> GetAllEmailTemplates(IConfigurationSection emailTemplatesSection)
        {
            List<EmailTemplate> emailTemplates = new List<EmailTemplate>();

            if (emailTemplatesSection != null)
            {
                for (int i = 0; i < emailTemplatesSection.GetChildren().Count(); i++)
                {
                    var scheduled_email_section = emailTemplatesSection.GetSection(i.ToString());
                    if (scheduled_email_section != null)
                    {
                        emailTemplates.Add(new EmailTemplate()
                        {
                            email_type = scheduled_email_section.GetSection("email_type").Value ?? string.Empty,
                            email_subject = scheduled_email_section.GetSection("email_subject").Value ?? string.Empty,
                            email_welcome_message = scheduled_email_section.GetSection("email_welcome_message").Value ?? string.Empty,
                            email_body_content = scheduled_email_section.GetSection("email_body_content").Value ?? string.Empty

                        });
                    }
                }

            }

            return emailTemplates;
        }
        public static string GetEmailBody(EmailTypes emailType, string emailWelcomeMessage, string emailBodyContent, Dictionary<string, string> valuesToReplace)
        {
            string body = emailWelcomeMessage + Environment.NewLine + emailBodyContent;
            body = ReplacePlaceholderValues(body, valuesToReplace);
            return body;
        }

        public static string ReplacePlaceholderValues(string text, Dictionary<string, string> valuesToReplace)
        {
            string outputText = text;

            foreach (var keyValue in valuesToReplace)
            {
                outputText = outputText.Replace(keyValue.Key, keyValue.Value);
            }
            return outputText;
        }
    }
}
