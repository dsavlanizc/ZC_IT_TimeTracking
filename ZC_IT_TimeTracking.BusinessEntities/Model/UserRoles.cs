using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZC_IT_TimeTracking.BusinessEntities.Model
{
    public static class UserRoles
    {
        public static string UserRole(params Roles[] roles)
        {
            if (roles.Length > 1)
            {
                List<string> _roles = new List<string>();
                foreach (int i in roles)
                {
                    _roles.Add(Enum.GetName(typeof(Roles), i));
                }
                return string.Join(",", _roles);
            }
            return Enum.GetName(typeof(Roles), roles[0]);
        }
    }

    public enum Roles
    {
        Admin = 1,
        Manager = 2,
        Resource = 3
    }
}
