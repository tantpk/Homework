using System.Web;
using System.Web.Optimization;
using System.Web.UI.WebControls;

namespace Homework
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/DataTables/jquery.dataTables.min.js",
                        "~/Scripts/DataTables/jquery.unobtrusive-ajax.js",
                        "~/Scripts/DataTables/dataTables.responsive.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/bootstrap.bundle.min.js"
                      ));
            bundles.Add(new Bundle("~/bundles/bootstrap-select").Include(
                      "~/Scripts/bootstrap-select.min.js",
                      "~/Scripts/i18n/defaults-en_US.min.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap-select.min.css",
                      "~/Content/DataTables/css/dataTables.jqueryui.min.css",
                      "~/Content/DataTables/css/dataTables.bootstrap.min.css",
                      "~/Content/DataTables/css/dataTables.bootstrap4.min.css",
                      "~/Content/DataTables/css/responsive.dataTables.min.css"                      
                      ));
        }
    }
}
