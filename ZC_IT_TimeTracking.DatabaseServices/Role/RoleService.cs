using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Linq;
using ZC_IT_TimeTracking.Services;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Security;

namespace ZC_IT_TimeTracking.Services.Role
{
    public class RoleService : ServiceBase
    {
        public List<string> GetAvailableRoles()
        {
            var list = Roles.GetAllRoles();
            return list.ToList();
        }
    }
}
