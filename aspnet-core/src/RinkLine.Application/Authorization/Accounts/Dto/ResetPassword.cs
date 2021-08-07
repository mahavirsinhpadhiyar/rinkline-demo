using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RinkLine.Authorization.Accounts.Dto
{
    public class ResetPassword
    {
        public long UserId { get; set; }
        public string ResetCode { get; set; }
        public string NewPassword { get; set; }
    }
}
