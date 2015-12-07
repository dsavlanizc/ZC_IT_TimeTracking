using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZC_IT_TimeTracking.Models
{
    public partial class AssignGoal
    {
        public int[] ResourceID { get; set; }
        public int Goal_MasterID { get; set; }
        public int weight { get; set; }
    }
}