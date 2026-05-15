using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace LIFE.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // CSS bundle
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/include/css/Maker_Zakat.css",
                "~/standard/CommonStyle.css",
                "https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css",
                "https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css",
                "https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css"
            ));

            // JS bundle
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "https://code.jquery.com/jquery-3.6.0.min.js",
                "https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js",
                "https://cdn.jsdelivr.net/npm/sweetalert2@11",
                "https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"
            ));

            // Aktifkan minify meskipun Debug=true
            BundleTable.EnableOptimizations = true;
        }
    }
}