using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZC_IT_TimeTracking.Services.Account
{
    public interface IAccountSevice : IServiceBase
    {
        bool CreateUser(string UserName, string Password);
        bool LoginUser(string UserName, string Password);
    }
}
