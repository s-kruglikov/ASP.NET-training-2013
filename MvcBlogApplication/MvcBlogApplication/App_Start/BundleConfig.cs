using System.Web;
using System.Web.Optimization;

namespace MvcBlog.WebUI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery.slides")
                .Include("~/Scripts/jquery.slides*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include("~/Scripts/bootstrap*"));

            bundles.Add(new ScriptBundle("~/bundles/gridmvc")
                .Include("~/Scripts/gridmvc*"));

            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/bootstrap-theme.css")
                .Include("~/Content/Site.css"));
        }
    }
}