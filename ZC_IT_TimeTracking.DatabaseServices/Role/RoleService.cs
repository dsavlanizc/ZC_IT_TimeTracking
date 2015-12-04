using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ZC_IT_TimeTracking.Services.Role
{
    public class RoleService
    {
        public List<string> GetAvailableRoles()
        {
            return new IdentityDbContext().Roles.Select(s => s.Name).ToList();
        }

        public bool CreateUser(string UserName, string Password)
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);

            var user = new IdentityUser() { UserName = UserName };
            IdentityResult result = manager.Create(user, Password);

             return result.Succeeded;
        }
    }
}
