using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace ZC_IT_TimeTracking.BusinessEntities.Model
{
    public class Resources
    {
        public Resources()
            {
                this.AssignGoal = new List<AssignGoal>();                
            }

            public Int32 ResourceID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Nullable<Int32> RoleID { get; set; }
            public Nullable<Int32> TeamID { get; set; }
            public string EMailID { get; set; }
            public string UserName { get; set; }            
            
            public List<AssignGoal> AssignGoal { get; set; }
            
            public Team Team { get; set; }
    }
}
