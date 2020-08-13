using System.Web;
using System.Web.Optimization;

namespace GYMONE
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            
            
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            //bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css",
            //            "~/Content/jquery-ui.css"));



            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/bootstrap/js/bootstrap.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/bootstrap/css/bootstrap.css",
                "~/bootstrap/css/bootstrap.min.css",
                "~/Content/Site.css"
                ));

           

           

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));




            bundles.Add(new ScriptBundle("~/bundles/scriptsCMS").Include(
                            "~/Scripts/jquery-{version}.js",
                            "~/Scripts/bootstrap.js"
                        ));

            

            bundles.Add(new StyleBundle("~/Content/SiteCMS").Include(
                "~/Content/bootstrap.css",
                "~/Content/SiteCMS.css"
                ));

            #region template


            bundles.Add(new ScriptBundle("~/Templates/jquery").Include(
                        "~/Templates/js/jquery.min.js",
                        "~/Templates/js/popper.min.js",
                        "~/Templates/js/bootstrap.min.js",
                        "~/Templates/js/jquery.magnific-popup.min.js",
                        "~/Templates/js/jquery.pogo-slider.min.js",
                        "~/Templates/js/slider-index.js",
                        "~/Templates/js/smoothscroll.js",
                        "~/Templates/js/responsiveslides.min.js",
                        "~/Templates/js/timeLine.min.js",
                        "~/Templates/js/form-validator.min.js",
                        "~/Templates/js/contact-form-script.js",
                        "~/Templates/js/custom.js"
                        ));

            bundles.Add(new StyleBundle("~/Templates/css").Include(
                        "~/Templates/css/bootstrap.min.css",
                        "~/Templates/css/pogo-slider.min.css",
                        "~/Templates/css/style.css",
                        "~/Templates/css/responsive.css",
                        "~/Templates/css/custom.css"
                        ));

            #endregion
        }
    }
}