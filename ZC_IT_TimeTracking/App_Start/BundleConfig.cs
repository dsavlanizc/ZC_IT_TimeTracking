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
                                                                 "~/Scripts/bootbox.min.js"));

            bundles.Add(new ScriptBundle("~/kendo/js").Include("~/Content/kendo/kendo.all.min.js",
                                                               "~/Content/kendo/kendo.aspnetmvc.min.js"));

            //adding css
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.min.css",
                                                                 "~/Content/Loading.css"));

            bundles.Add(new StyleBundle("~/kendo/css").Include("~/Content/kendo/css/kendo.common-bootstrap.min.css",
                                                               "~/Content/kendo/css/kendo.bootstrap.min.css"));
        }
    }
}