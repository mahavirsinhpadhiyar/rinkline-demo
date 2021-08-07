using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RinkLine.Authorization.Accounts.Dto
{
    public class ForgotPassword
    {
        public string EmailAddress { get; set; }
        public bool SentSuccessfully { get; set; }
    }
}
