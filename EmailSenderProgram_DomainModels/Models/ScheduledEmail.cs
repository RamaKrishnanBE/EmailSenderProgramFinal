using EmailSenderProgram_DomainModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderProgram_DomainModels.Models
{
    public class ScheduledEmail
    {
        public string email_type { get; set; } = string.Empty;
        public string email_recurrence_type { get; set; } = string.Empty;
        public string day_to_send_weekly_mail { get; set; } = string.Empty;
    }
}
