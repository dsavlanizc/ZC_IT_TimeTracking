using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZC_IT_TimeTracking.BusinessEntities
{
    public class Utilities
    {
        public static int RecordPerPage = 5;
        public static int GetQuarter()
        {
            DateTime date = DateTime.Now;
            if (date.Month >= 1 && date.Month <= 3)
                return 1;
            else if (date.Month >= 4 && date.Month <= 7)
                return 2;
            else if (date.Month >= 8 && date.Month <= 10)
                return 3;
            else
                return 4;
        }
    }
}