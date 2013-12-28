using System.Web.Optimization;

namespace MvcBlog.WebUI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryDatepicker")
                .Include("~/Scripts/jquery.ui.1.10.3.datepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                .Include("~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquerySlides")
                .Include("~/Scripts/jquery.slides.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include("~/Scripts/bootstrap*"));

            bundles.Add(new ScriptBundle("~/bundles/gridmvc")
                .Include("~/Scripts/gridmvc.js"));

            bundles.Add(new StyleBundle("~/Content/sitecss")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/bootstrap-theme.css")
                .Include("~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/admincss")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/bootstrap-theme.css")
                .Include("~/Content/Gridmvc.css")
                .Include("~/Content/Admin.css"));

            bundles.Add(new StyleBundle("~/Content/datepickercss")
                .Include("~/Content/datepicker/jquery-ui*"));

            bundles.Add(new StyleBundle("~/Content/slidercss")
                .Include("~/Content/Slider.css"));
        }
    }
}