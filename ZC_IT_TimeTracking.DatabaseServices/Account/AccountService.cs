using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZC_IT_TimeTracking.Services.Account
{
    public class AccountService : ServiceBase
    {
        public bool CreateUser(string UserName, string Password)
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);

            var user = new IdentityUser() { UserName = UserName };
            IdentityResult result = manager.Create(user, Password);

            if (!result.Succeeded)
            {
                this.ValidationErrors.Add("UserExist", result.Errors.ToString());
            }

            return result.Succeeded;
        }
    }
}
