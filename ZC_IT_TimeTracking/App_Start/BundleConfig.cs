using System.Web;
using System.Web.Optimization;

namespace MvcDemo.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //adding javascript and bootstrap js
            bundles.Add(new ScriptBundle("~/Content/js").Include("~/Scripts/jquery-1.9.1.min.js",
                                                                 "~/Scripts/bootstrap.min.js",
                                                                 "~/Scripts/GoalScript.js"));
            //adding css
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.min.css"));
        }
    }
}