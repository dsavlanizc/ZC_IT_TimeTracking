using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZC_IT_TimeTracking.BusinessEntities.Model
{
    public class ResourcesByTeam : GetResourceByTeam_Result
    {
        public string Name { get { return Name; } set { Name = FirstName + " " + LastName; } }
    }
}
