using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZC_IT_TimeTracking.BusinessEntities.Model
{
    public class ResourceGoalModel
    {
        public int ResourceID { get; set; }
        public int Resource_GoalID { get; set; }
        public int Goal_MasterID { get; set; }
        public string GoalTitle { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
    }
}
