using System.Web;
using System.Web.Optimization;

namespace UrlShortener
{
    public class BundleConfig
    {
        // Paketleme hakkında daha fazla bilgi için lütfen https://go.microsoft.com/fwlink/?LinkId=301862 adresini ziyaret edin
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Geliştirme yapmak ve öğrenmek için Modernizr'ın geliştirme sürümünü kullanın. Daha sonra,
            // üretim için hazır. https://modernizr.com adresinde derleme aracını kullanarak yalnızca ihtiyacınız olan testleri seçin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                      "~/Content/assets/js/perfect-scrollbar.min.js",
                      "~/Content/assets/js/sweetalert.min.js",
                      "~/Content/assets/js/custom.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/assets/css/perfect-scrollbar.css",
                      "~/Content/assets/css/style.css"));

            bundles.Add(new StyleBundle("~/Content/icons").Include(
                      "~/Content/assets/fonts/LineIcons/font-css/LineIcons.min.css", new CssRewriteUrlTransform()));
        }
    }
}
