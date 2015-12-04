using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ZC_IT_TimeTracking.DatabaseServices.Account
{
    public class AccountService : ServiceBase
    {
        public bool CreateUser(string UserName, string Password)
        {
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);

            var user = new IdentityUser() { UserName = UserName };
            IdentityResult result = userManager.Create(user, Password);

            if (!result.Succeeded)
            {
                this.ValidationErrors.Add("UserExist", result.Errors.ElementAt(0).ToString());
            }

            return result.Succeeded;
        }

        public bool LoginUser(string UserName, string Password)
        {
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = userManager.Find(UserName, Password);

            if (user != null)
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);
                return true;
            }
            return false;
        }
    }
}
