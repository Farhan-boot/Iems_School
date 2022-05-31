using System.Web;
using System.Web.Optimization;

using BundleTransformer.Core.Builders;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Resolvers;
using BundleTransformer.Core.Transformers;

namespace EMS.Web.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            var nullBuilder = new NullBuilder();
            var styleTransformer = new StyleTransformer();
            var scriptTransformer = new ScriptTransformer();
            var nullOrderer = new NullOrderer();

            // Replace a default bundle resolver in order to the debugging HTTP-handler
            // can use transformations of the corresponding bundle
            BundleResolver.Current = new CustomBundleResolver();
            
            #if DEBUG
                BundleTable.EnableOptimizations = false;
            #else
                BundleTable.EnableOptimizations = true; //for compress  bundles on publish
            #endif


            #region Style Css
            //1
            var bootstrapStylesBundle = new Bundle("~/css/bootstrap");
            bootstrapStylesBundle.Include(
                "~/assets/css/bootstrap.min.css");
            bootstrapStylesBundle.Builder = nullBuilder;
            bootstrapStylesBundle.Transforms.Add(styleTransformer);
            bootstrapStylesBundle.Orderer = nullOrderer;
            bundles.Add(bootstrapStylesBundle);
            //1.1
            var bootstrapRtlStylesBundle = new Bundle("~/css/bootstrap-rtl");
            bootstrapRtlStylesBundle.Include(
                "~/assets/css/bootstrap-rtl.min.css");
            bootstrapRtlStylesBundle.Builder = nullBuilder;
            bootstrapRtlStylesBundle.Transforms.Add(styleTransformer);
            bootstrapRtlStylesBundle.Orderer = nullOrderer;
            bundles.Add(bootstrapRtlStylesBundle);
            //2
            var themeStylesBundle = new Bundle("~/css/theme");
            themeStylesBundle.Include(
                "~/assets/css/theme.min.css",
                "~/assets/css/demo.min.css",
                "~/assets/css/font-awesome.min.css",
                "~/assets/css/typicons.min.css",
                "~/assets/css/weather-icons.min.css",
                "~/assets/custom/lib/toastr/toastr.min.css", //for toastr notification updated from vendor
                "~/assets/css/animate.min.css");
            themeStylesBundle.Builder = nullBuilder;
            themeStylesBundle.Transforms.Add(styleTransformer);
            themeStylesBundle.Orderer = nullOrderer;
            bundles.Add(themeStylesBundle);
            //2.1
            var themeRtlStylesBundle = new Bundle("~/css/theme-rtl");
            themeRtlStylesBundle.Include(
                "~/assets/css/theme-rtl.min.css",
                "~/assets/css/demo.min.css",
                "~/assets/css/font-awesome.min.css",
                "~/assets/css/typicons.min.css",
                "~/assets/css/weather-icons.min.css",
                "~/assets/css/animate.min.css");
            themeRtlStylesBundle.Builder = nullBuilder;
            themeRtlStylesBundle.Transforms.Add(styleTransformer);
            themeRtlStylesBundle.Orderer = nullOrderer;
            bundles.Add(themeRtlStylesBundle);

            //3
            var angularStylesBundle = new Bundle("~/css/angular");
            angularStylesBundle.Include(
                //"~/assets/custom/lib/angular/ng-grid/ng-grid.css"
                //,"~/assets/custom/lib/angular/ng-img-crop/ng-img-crop.min.css"
                //,"~/assets/custom/lib/angular/angular-bootstrap-datetimepicker/css/datetimepicker.css"//don't add any other  
               "~/assets/custom/lib/angular/angular-loading-bar/loading-bar.min.css"
               ,"~/assets/js/angular/angular-ui-select/select.min.css" //depends on sanitize, has js
                );
            angularStylesBundle.Builder = nullBuilder;
            angularStylesBundle.Transforms.Add(styleTransformer);
            angularStylesBundle.Orderer = nullOrderer;
            bundles.Add(angularStylesBundle);

            //4 //for custom extentions
            var libStylesBundle = new Bundle("~/css/lib");
            libStylesBundle.Include(
                 "~/assets/custom/lib/xdsoftDateTimePicker/jquery.datetimepicker.min.css" //need for xdsoft datetime picker
                );
            libStylesBundle.Builder = nullBuilder;
            libStylesBundle.Transforms.Add(styleTransformer);
            libStylesBundle.Orderer = nullOrderer;
            bundles.Add(libStylesBundle);
            //5
            var customStylesBundle = new Bundle("~/css/custom");
            customStylesBundle.Include(
                "~/assets/custom/css/site.css",
                "~/assets/custom/css/print.css");
            customStylesBundle.Builder = nullBuilder;
            customStylesBundle.Transforms.Add(styleTransformer);
            customStylesBundle.Orderer = nullOrderer;
            bundles.Add(customStylesBundle);
            #endregion

            #region Script   Bunldes
            //1
            var jQueryBundle = new Bundle("~/js/jquery");
            jQueryBundle.Include("~/assets/js/jquery.min.js");
            jQueryBundle.Builder = nullBuilder;
            jQueryBundle.Transforms.Add(scriptTransformer);
            jQueryBundle.Orderer = nullOrderer;
            bundles.Add(jQueryBundle);
            //2
            var bootstrapBundle = new Bundle("~/js/bootstrap");
            bootstrapBundle.Include(
                  "~/assets/js/bootstrap.min.js");
            bootstrapBundle.Builder = nullBuilder;
            bootstrapBundle.Transforms.Add(scriptTransformer);
            bootstrapBundle.Orderer = nullOrderer;
            bundles.Add(bootstrapBundle);

            //3
            var jsLibBundle = new Bundle("~/js/lib");
            jsLibBundle.Include(
                 "~/assets/js/jqueryval/jquery.validate*"
                 //"~/assets/custom/lib/moment/moment.min.js", //dependancy for ng/datetimepicker.js || js Error to get
                 );
            jsLibBundle.Builder = nullBuilder;
            jsLibBundle.Transforms.Add(scriptTransformer);
            jsLibBundle.Orderer = nullOrderer;
            bundles.Add(jsLibBundle);
            //4
            var themeBundle = new Bundle("~/js/theme");
            themeBundle.Include( 
                "~/assets/js/slimscroll/jquery.slimscroll.min.js"//dependancy for theme.min.js
                ,"~/assets/js/skins.min.js" //dependancy  for theme.min.js
                ,"~/assets/js/theme.min.js" // depends on  slimscroll.js,skins.js
                ,"~/assets/custom/lib/toastr/toastr.min.js" // for Toastr Notifications updated from vendor
                ,"~/assets/js/bootbox/bootbox.min.js" // for Modal
                //,"~/assets/js/fuelux/wizard/wizard-custom*"
                ,"~/assets/js/inputmask/jasny-bootstrap.min.js" //data-input-mask
                ,"~/assets/js/datetime/moment.js" //dependancy for ng/datetimepicker.js
                //,"~/assets/js/datetime/bootstrap-datepicker.min.js"
                //,"~/assets/js/datetime/bootstrap-timepicker.min.js"
                //,"~/assets/js/datetime/daterangepicker.js" //depends on moment.js,
                ,"~/assets/js/charts/morris/raphael-2.0.2.min.js"
                ,"~/assets/js/charts/morris/morris.js"
                //, "~/assets/js/select2/select2.min.js"//todo test later,
            //,"~/assets/js/fullcalendar/moment.min.js"
            //,"~/assets/js/fullcalendar/jquery-ui.custom.min.js"
            //,"~/assets/js/fullcalendar/fullcalendar.min.js"
            );
            themeBundle.Builder = nullBuilder;
            themeBundle.Transforms.Add(scriptTransformer);
            themeBundle.Orderer = nullOrderer;
            bundles.Add(themeBundle);

            //5
            var angularBundle = new Bundle("~/js/angular");
            angularBundle.Include(
                "~/assets/js/angular/angular.js",
                //"~/assets/custom/lib/angular/ng-grid/ng-grid.min.js",
                //"~/assets/custom/lib/angular/angular-bootstrap-datetimepicker/js/datetimepicker.js",//depends on moment.js
                //"~/assets/custom/lib/angular/smart-table/smart-table.min.js",
                "~/assets/custom/lib/angular/ng-file-upload/ng-file-upload-shim.min.js",
                "~/assets/custom/lib/angular/ng-file-upload/ng-file-upload.min.js",
                //"~/assets/custom/lib/angular/ng-img-crop/ng-img-crop.min.js",
                "~/assets/custom/lib/angular/angular-loading-bar/loading-bar.min.js", //has css
                "~/assets/js/angular/angular-sanitize/angular-sanitize.js", //depends for select2 Angular
                "~/assets/js/angular/angular-ui-select/select.min.js", //depends on sanitize, has css
                 //js\angular\angular-ui-select\select.min.js
                "~/assets/custom/lib/xdsoftDateTimePicker/datetime.js",//depends on moment.js; works for ngdate for directive https://github.com/eight04/angular-datetime
                "~/Content/Controllers/MainController.js"
            );
            angularBundle.Builder = nullBuilder;
            angularBundle.Transforms.Add(scriptTransformer);
            angularBundle.Orderer = nullOrderer;
            bundles.Add(angularBundle);

            //6
            var customBundle = new Bundle("~/js/custom");
            customBundle.Include(
                "~/assets/custom/lib/xdsoftDateTimePicker/jquery.datetimepicker.full.min.js" //has css, see details http://xdsoft.net/jqplugins/datetimepicker/
                , "~/assets/custom/lib/xdsoftDateTimePicker/date.format.js"//to format date,eg:dateFormat(Date, "dd/mm/yyyy")  http://blog.stevenlevithan.com/archives/date-time-format
                //, "~/assets/js/tabdrop/bootstrap-tabdrop.js"//to get in dropdown in tab https://www.eyecon.ro/bootstrap-tabdrop/
                , "~/assets/custom/js/site.js"
               );
            customBundle.Builder = nullBuilder;
            customBundle.Transforms.Add(scriptTransformer);
            customBundle.Orderer = nullOrderer;
            bundles.Add(customBundle);
            #endregion


        }
    }
}
