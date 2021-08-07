using Abp.Configuration;
using Abp.Net.Mail.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RinkLine.Authorization.Users
{
    public class RinkLineSmtpEmailSenderConfiguration : SmtpEmailSenderConfiguration
    {
        public RinkLineSmtpEmailSenderConfiguration(ISettingManager settingManager) : base(settingManager)
        {

        }
    }
}
