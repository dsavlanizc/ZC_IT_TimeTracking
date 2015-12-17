using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZC_IT_TimeTracking.BusinessEntities.Model
{
    public class RegisterUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string[] RoleName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
