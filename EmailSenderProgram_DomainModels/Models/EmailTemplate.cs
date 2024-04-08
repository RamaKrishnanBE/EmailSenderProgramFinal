using EmailSenderProgram_DomainModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderProgram_DomainModels.Models
{
    public class EmailTemplate
    {
        public string email_type { get; set; } = string.Empty;
        public string email_subject { get; set; } = string.Empty;
        public string email_welcome_message { get; set; } = string.Empty;
        public string email_body_content { get; set; } = string.Empty;
    }
}
