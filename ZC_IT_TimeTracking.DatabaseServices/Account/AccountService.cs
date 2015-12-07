using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Web.Security;

namespace ZC_IT_TimeTracking.Services.Account
{
    public class AccountService : ServiceBase
    {
        public bool CreateUser(string UserName, string Password, string EmailId, string[] roles)
        {
            try
            {
                var userStore = new UserStore<IdentityUser>();
                var userManager = new UserManager<IdentityUser>(userStore);
                
                var user = new IdentityUser() { UserName = UserName, Email = EmailId };
                IdentityResult result = userManager.Create(user, Password);

                if (result.Succeeded)
                {
                    userManager.AddToRoles(user.Id, roles);
                    //Roles.AddUserToRoles(userManager.FindByName(UserName).Id, roles);
                }
                else
                {
                    this.ValidationErrors.Add("UserExist", result.Errors.ElementAt(0).ToString());
                }
                return result.Succeeded;
            }
            catch (Exception)
            {
                this.ValidationErrors.Add("LoginError","Error while login!");
                return false;
            }
        }

        public bool LoginUser(string UserName, string Password, bool rememberMe)
        {
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = userManager.Find(UserName, Password);

            if (user != null)
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = rememberMe }, userIdentity);
                return true;
            }
            return false;
        }

        public bool CreateRole(string roleName)
        {
            try
            {
                var rs = new RoleStore<IdentityRole>();
                var rm = new RoleManager<IdentityRole>(rs);
                if (rm.FindByName(roleName) == null)
                {
                    var asdf = new IdentityRole(roleName);
                    rm.Create(asdf);
                    return true;
                }
                else
                {
                    this.ValidationErrors.Add("RoleExist", "This role is already exist!");
                    return false;
                }
            }
            catch(Exception)
            {
                this.ValidationErrors.Add("CreateRoleError", "Error occured while creating role!");
                return false;
            }
        }

        public bool LogoutUser()
        {
            try
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                authenticationManager.SignOut();
                return true;
            }
            catch (Exception ex)
            {
                this.ValidationErrors.Add("Logout_failed","Requested logout failed!");
                return false;
            }
        }
    }
}
