using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZC_IT_TimeTracking.BusinessEntities
{
    public class Teams
    {
        public Teams()
        {
        this.Resource=new List<Resources>();
        }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int DepartmentID { get; set; }

        public List<Resources> Resource { get; set; }
    }
}
