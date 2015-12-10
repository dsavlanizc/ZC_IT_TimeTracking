using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZC_IT_TimeTracking.DataAccess.SQLParamFinder
{
    public class Sproc
    {
        public Sproc()
        {
            this.Params = new List<Param>();
        }
        public string Name { get; set; }
        public List<Param> Params { get; set; }
    }
    public class Param
    {
        public string Name { get; set; }
        public int sqlDbType { get; set; }
        public int direction { get; set; }
    }
}
