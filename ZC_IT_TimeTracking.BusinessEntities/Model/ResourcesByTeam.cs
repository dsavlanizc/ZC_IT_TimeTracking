using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZC_IT_TimeTracking.BusinessEntities.Model
{
    public class ResourcesByTeam
    {
        public int ResourceID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get { return Name; } set { Name = FirstName + " " + LastName; } }
    }
}
