using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderProgram_DomainModels.Models
{
    public class ApplicationLogMessages
    {
        public string application_start_message { get; set; } = string.Empty;
        public string application_end_message { get; set; } = string.Empty;
        public string prior_to_send_first_mail { get; set; } = string.Empty;
        public string after_send_every_mail { get; set; } = string.Empty;
        public string after_send_all_mails { get; set; } = string.Empty;
        public string exception_message { get; set; } = string.Empty;

    }
}
