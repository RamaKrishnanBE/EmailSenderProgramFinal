using EmailSenderProgram_DomainModels.Enums;
using EmailSenderProgram_DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderProgram_EmailServices.Interfaces
{
    public interface IEmailService
    {
        bool SendEmail(string toAddress, EmailServerSettings emailServer, string emailSubject, string emailBody);
    }
}
