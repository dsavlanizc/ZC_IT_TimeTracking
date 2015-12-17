using System.Web.Security;
using System;
using ZC_IT_TimeTracking.BusinessEntities.Model;
using System.Web.Profile;
using System.Web;

namespace ZC_IT_TimeTracking.Services.Account
{
    public class AccountService : ServiceBase
    {
        public bool CreateUser(RegisterUser user)
        {
            try
            {
                //var userStore = new UserStore<IdentityUser>();
                //var userManager = new UserManager<IdentityUser>(userStore);
                
                //var user = new IdentityUser() { UserName = UserName, Email = EmailId };
                //IdentityResult result = userManager.Create(user, Password);

                //if (result.Succeeded)
                //{
                //    userManager.AddToRoles(user.Id, roles);
                //    //Roles.AddUserToRoles(userManager.FindByName(UserName).Id, roles);
                //}
                //else
                //{
                //    this.ValidationErrors.Add("UserExist", result.Errors.ElementAt(0).ToString());
                //}
                //return result.Succeeded;
                var result = Membership.FindUsersByName(user.UserName);
                if (result.Count == 0)
                {
                    var membershipUser = Membership.CreateUser(user.UserName, user.Password, user.EmailID);
                    Roles.AddUserToRoles(user.UserName, user.RoleName);
                    UserProfile profile = UserProfile.GetUserProfile(user.UserName);
                    profile.FirstName = user.FirstName;
                    profile.LastName = user.LastName;
                    profile.Save();
                    return true;
                }
                else
                {
                    this.ValidationErrors.Add("UserExist", "User is already exist!");
                    return false;
                }

            }
            catch (Exception ex)
            {
                this.ValidationErrors.Add("LoginError", ex.Message);
                return false;
            }
        }

        public bool LoginUser(string UserName, string Password, bool rememberMe)
        {
            var user = Membership.ValidateUser(UserName, Password);
            if (user)
            {
                //var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                //var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                //authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = rememberMe }, userIdentity);
                FormsAuthentication.SetAuthCookie(UserName, rememberMe);
                UserProfile profile = UserProfile.GetUserProfile(UserName);
                HttpContext.Current.Session["userFullName"] = profile.FirstName + " " + profile.LastName;
                return true;
            }
            else if (Membership.FindUsersByName(UserName).Count > 0)
            {
                if (Membership.GetUser(UserName).IsLockedOut)
                    this.ValidationErrors.Add("LockedOut", "Account is blocked!");
            }
            this.ValidationErrors.Add("InvalidCredentials", "Invalid username or password!");
            return false;
        }

        public bool CreateRole(string roleName)
        {
            try
            {
                if (!Roles.RoleExists(roleName))
                {
                    Roles.CreateRole(roleName);
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
                FormsAuthentication.SignOut();
                return true;
            }
            catch
            {
                this.ValidationErrors.Add("Logout_failed","Requested logout failed!");
                return false;
            }
        }
    }
}
