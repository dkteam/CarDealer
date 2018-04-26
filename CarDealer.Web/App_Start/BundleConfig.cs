using System.Web;
using System.Web.Optimization;

namespace CarDealer.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/core").Include(
                "~/Assets/client/js/bootstrap.min.js",
                "~/Assets/client/js/custome/carSearch.js",
                "~/Assets/client/js/custome/carSearchForSmallScreen.js",
                "~/Assets/client/js/custome/sendFeedback.js",
                "~/Assets/client/js/jquery.js",
                "~/Assets/client/js/jquery-ui.js",
                "~/Assets/client/js/jquery-migrate-1.2.1.min.js",
                "~/Assets/client/js/jquery.easing.1.3.js",
                "~/Assets/client/js/pointer-events.js",
                "~/Assets/client/js/jquery.flexslider-min.js",
                "~/Assets/client/js/select2.js",
                "~/Assets/client/js/jquery.superslides.js",
                "~/Assets/client/js/jquery.sticky.js",
                "~/Assets/client/js/jquery.appear.js",
                "~/Assets/client/js/jquery.ui.totop.js",
                "~/Assets/client/js/jquery.caroufredsel.js",
                "~/Assets/client/js/jquery.touchSwipe.min.js",
                "~/Assets/client/js/material-parallax.js",
                "~/Assets/client/js/owl-carousel.js",
                "~/Assets/client/js/rd-mailform.js",
                "~/Assets/client/js/rd-navbar.js",
                "~/Assets/client/js/rd-instafeed.js",
                "~/Assets/client/js/light-gallery.js",
                "~/Assets/client/js/swiper.js",
                "~/Assets/client/js/waypoint.js",
                "~/Assets/client/js/scripts.js"
            ));

            BundleTable.EnableOptimizations = true;
        }
    }
}
