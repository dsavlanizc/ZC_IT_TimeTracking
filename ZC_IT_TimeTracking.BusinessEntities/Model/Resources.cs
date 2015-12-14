using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace ZC_IT_TimeTracking.BusinessEntities
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
            public string Name { get { return Name; } set { Name = FirstName + " " + LastName; } }
          
            public List<AssignGoal> AssignGoal { get; set; }
            public Team Team { get; set; }

            public Int32 Goal_MasterID { get; set; }
            public string  GoalTitle { get; set; }
            public DateTime GoalAssignDate { get; set; }
            public Int32 weight { get; set; }
            public Int32 Resource_GoalID { get; set; }
    }
}
