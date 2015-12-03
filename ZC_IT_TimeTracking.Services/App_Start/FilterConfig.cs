using System.Web;
using System.Web.Mvc;

namespace ZC_IT_TimeTracking.Services
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
