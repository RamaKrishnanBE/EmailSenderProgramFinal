// See https://aka.ms/new-console-template for more information
using EmailSenderProgram_DataAccessLayer;
using EmailSenderProgram_DomainModels.Common;
using EmailSenderProgram_DomainModels.Enums;
using EmailSenderProgram_DomainModels.Models;
using EmailSenderProgram_EmailServices.Helpers;
using EmailSenderProgram_EmailServices.Serices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

/// <summary>
/// This application is run everyday
/// </summary>
/// <param name="args"></param>


try
{
    using IHost host = Host.CreateDefaultBuilder(args).Build();

    IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

    var sec_log_messages = config.GetSection("log_messages");

    var logMessages = LogMessageHelper.GetApplicationLogMessages(sec_log_messages);
    if (logMessages.application_start_message != null)
    {
        //Call the method that do the work for me, I.E. sending the mails
        Console.WriteLine(logMessages.application_start_message);

        var sec_email_server = config.GetSection("email_server");

        var emailSettings = EmailServerHelper.GetEmailServerSettings(sec_email_server);
        if (emailSettings != null && EmailHelper.IsValidEmail(emailSettings.sender_email) && string.IsNullOrWhiteSpace(emailSettings.sender_password) == false)
        {
            var sec_scheduled_emails = config.GetSection("scheduled_emails");

            var scheduledEmails = EmailHelper.GetScheduledEmailTypes(sec_scheduled_emails);

            if (scheduledEmails != null && scheduledEmails.Any())
            {
                var sec_email_templates = config.GetSection("email_templates");
                var emailTemplates = EmailHelper.GetAllEmailTemplates(sec_email_templates);
                if (emailTemplates != null && emailTemplates.Any())
                {
                    EmailTypes emailType = EmailTypes.none;
                    EmailRecurrenceTypes emailRecurrenceType = EmailRecurrenceTypes.none;
                    DayOfWeek weeklyEmailDay = DayOfWeek.Sunday;

                    EmailService emailService = new EmailService();
                    bool isAllSentEmail = false;
                    foreach (var scheduledEmail in scheduledEmails)
                    {
                        if (scheduledEmail != null)
                        {
                            if (string.IsNullOrWhiteSpace(scheduledEmail.email_type) == false && (Enum.TryParse(scheduledEmail.email_type, out emailType)))
                            {
                                if (emailType != EmailTypes.none)
                                {
                                    var mailTemplate = emailTemplates.FirstOrDefault(x => x.email_type == emailType.ToString());
                                    if (mailTemplate != null && string.IsNullOrWhiteSpace(mailTemplate.email_type) == false)
                                    {

                                        if (string.IsNullOrWhiteSpace(scheduledEmail.email_recurrence_type) == false && (Enum.TryParse(scheduledEmail.email_recurrence_type, out emailRecurrenceType)))
                                        {
                                            if (emailRecurrenceType != EmailRecurrenceTypes.none)
                                            {
                                                if (emailRecurrenceType == EmailRecurrenceTypes.weekly && string.IsNullOrWhiteSpace(scheduledEmail.day_to_send_weekly_mail) == false)
                                                {
                                                    Enum.TryParse(scheduledEmail.day_to_send_weekly_mail, out weeklyEmailDay);
                                                }

                                                mailTemplate.email_subject = string.IsNullOrWhiteSpace(mailTemplate.email_subject) == false ? mailTemplate.email_subject : GlobalConstant.CONST_EMAIL_SUBJECT_PLACEHOLDER;
                                                mailTemplate.email_welcome_message = string.IsNullOrWhiteSpace(mailTemplate.email_welcome_message) == false ? mailTemplate.email_welcome_message : GlobalConstant.CONST_EMAIL_GREETINGS_MESSAGE_PLACEHOLDER;
                                                mailTemplate.email_body_content = string.IsNullOrWhiteSpace(mailTemplate.email_body_content) == false ? mailTemplate.email_body_content : GlobalConstant.CONST_EMAIL_BODY_CONTENT_PLACEHOLDER;
                                                switch (emailRecurrenceType)
                                                {
                                                    case EmailRecurrenceTypes.daily:
                                                        isAllSentEmail = SendAllEmail(emailService, emailType, emailSettings, mailTemplate.email_subject, mailTemplate.email_welcome_message, mailTemplate.email_body_content, logMessages.after_send_every_mail);
                                                        break;
                                                    case EmailRecurrenceTypes.weekly:
                                                        if (DateTime.Now.DayOfWeek.Equals(weeklyEmailDay))
                                                        {
                                                            isAllSentEmail = SendAllEmail(emailService, emailType, emailSettings, mailTemplate.email_subject, mailTemplate.email_welcome_message, mailTemplate.email_body_content, logMessages.after_send_every_mail);
                                                        }
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(string.Format(GlobalConstant.CONST_ERROR_EMAIL_TEMPLATE_NOT_FOUND, emailType.ToString()));
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine(GlobalConstant.CONST_ERROR_UNKNOWN_EMAIL_SCHEDULED);
                                break;
                            }
                        }
                    }

                    if (isAllSentEmail)
                    {
                        Console.WriteLine(logMessages.after_send_all_mails);
                    }
                }
                else
                {
                    Console.WriteLine(GlobalConstant.CONST_ERROR_MAIL_TEMPLATE_NULL);
                }
            }
            else
            {
                Console.WriteLine(GlobalConstant.CONST_ERROR_SCHEDULED_MAIL_NULL);
            }
        }
        else
        {
            Console.WriteLine(GlobalConstant.CONST_ERROR_EMAIL_SERVER_SETTINGS);
        }

    }
    else
    {
        Console.WriteLine(GlobalConstant.CONST_ERROR_LOG_MESSAGE_NULL);
    }
}
catch (Exception ex)
{
    Console.WriteLine("Program --> Exception message: " + ex.Message);
}


bool SendAllEmail(EmailService emailService, EmailTypes emailType, EmailServerSettings emailServer, string emailSubject, string welcomeMessage, string emailBody, string logMessageForEachMail)
{
    bool isAllEmailSent = false;

    try
    {
        Dictionary<string, string> valuesToReplace = new Dictionary<string, string>();
        List<Customer> customer = new List<Customer>();
        //Get newly registered users list, one day back in time
        if (emailType == EmailTypes.welcome_mail)
        {
            customer = DataLayer.ListCustomers().Where(x => x.CreatedDateTime > DateTime.Now.AddDays(-1)).ToList();
        }
        else if (emailType == EmailTypes.comeback_mail)
        {
            //List all customers 
            customer = DataLayer.ListCustomers();
            //List all orders
            List<Order> orders = DataLayer.ListOrders();

            if (customer != null && customer.Any() && orders != null && orders.Any())
            {
                var customer_2 = from c in customer
                                 join o in orders on c.Email equals o.CustomerEmail
                                 select new Customer()
                                 {
                                     Email = c.Email,
                                     CreatedDateTime = c.CreatedDateTime
                                 };

                if (customer_2 != null && customer_2.Count() > 0)
                {
                    customer = customer_2.ToList();
                }
            }

            valuesToReplace.Add("##voucher##", "EOComebackToUs");

        }
        string toEmail = string.Empty;

        //loop through list of new customers
        for (int i = 0; i < customer.Count; i++)
        {
            valuesToReplace.Add("##email##", customer[i].Email);
            emailBody = EmailHelper.GetEmailBody(emailType, welcomeMessage, emailBody, valuesToReplace);

            //Send email through email service
            isAllEmailSent = emailService.SendEmail(customer[i].Email, emailServer, emailSubject, emailBody);
            Console.WriteLine(emailType.ToString() + logMessageForEachMail + customer[i].Email);
        }
    }
    catch (Exception)
    {
        //Something went wrong :(
        isAllEmailSent = false;
    }

    return isAllEmailSent;
}