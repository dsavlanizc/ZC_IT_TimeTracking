using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Linq;
using ZC_IT_TimeTracking.Services;
using Microsoft.AspNet.Identity.EntityFramework;
using ZC_IT_TimeTracking.Services;

namespace ZC_IT_TimeTracking.Services.Role
{
    public class RoleService : ServiceBase
    {
        public List<string> GetAvailableRoles()
        {
            return new IdentityDbContext().Roles.Select(s => s.Name).ToList();
        }
    }
}
