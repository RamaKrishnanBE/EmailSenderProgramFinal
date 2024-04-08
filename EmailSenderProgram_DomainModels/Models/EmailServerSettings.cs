using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderProgram_DomainModels.Models
{
    public class EmailServerSettings
    {
        public string email_server_name { get; set; } = string.Empty;
        public string smtp_host { get; set; } = string.Empty;
        public string smtp_port { get; set; } = string.Empty;
        public string sender_email { get; set; } = string.Empty;
        public string sender_password { get; set; } = string.Empty;
        public bool enable_ssl { get; set; }
        public bool use_default_credentials { get; set; }

    }
}
