using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Profile;
using System.Web.Security;

namespace ZC_IT_TimeTracking.Services
{
    public class UserProfile : ProfileBase
    {
        [SettingsAllowAnonymous(false)]
        public string FirstName { get { return base["FirstName"] as string; } set { base["FirstName"] = value; } }

        [SettingsAllowAnonymous(false)]
        public string LastName { get { return base["LastName"] as string; } set { base["LastName"] = value; } }

        public static UserProfile GetUserProfile(string userName)
        {
            return Create(userName) as UserProfile;
        }

        public static UserProfile GetUserProfile()
        {
            return Create(Membership.GetUser().UserName) as UserProfile;
        }
    }
}
