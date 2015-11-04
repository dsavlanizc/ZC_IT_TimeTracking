using System.Web;
using System.Web.Optimization;

namespace MvcDemo.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //adding javascript and bootstrap js
            bundles.Add(new ScriptBundle("~/Content/js").Include("~/Scripts/jquery-{version}.js",
                                                                 "~/Scripts/bootstrap.min.js",
                                                                 "~/Scripts/jquery.validate.min.js",
                                                                 "~/Scripts/GoalScript.js",
                                                                 "~/Scripts/bootbox.min.js",
                                                                 "~/Scripts/bootstrap-datepicker.min.js"));
            //adding css
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.min.css",
                                                                 "~/Content/Loading.css",
                                                                 "~/Content/bootstrap-datepicker.min.css"));
        }
    }
}