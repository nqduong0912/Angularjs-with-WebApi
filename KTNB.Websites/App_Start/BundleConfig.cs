using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace VPB_KTNB
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the Development version of Modernizr to develop with and learn from. Then, when you’re
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Scripts/modernizr-*"));

            ScriptManager.ScriptResourceMapping.AddDefinition(
                "respond",
                new ScriptResourceDefinition
                {
                    Path = "~/Scripts/respond.min.js",
                    DebugPath = "~/Scripts/respond.js",
                });

            /* Custom */
            bundles.Add(new ScriptBundle("~/Scripts/angularjs").Include(
               "~/Scripts/angular.js",
               "~/Scripts/angular-animate.js",
               "~/Scripts/angular-route.js",
               "~/Scripts/angular-touch.js",
               "~/Scripts/angular-cookies.js",
               "~/Scripts/angular-messages.js",
               "~/Scripts/angular-resource.js",
               "~/Scripts/angular-loader.js",
               "~/Scripts/i18n/angular-locale_vi.js",
               //"~/Scripts/angular-ui/ui-bootstrap.js",
               "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
               "~/Scripts/angular-strap.js",
               "~/Scripts/angular-strap.tpl.js",
               "~/Scripts/angular-block-ui.js",
               "~/Scripts/angular-toastr.tpls.js",
               "~/Scripts/angular-chosen.js",
               "~/Scripts/ng-file-upload-shim.js",
               "~/Scripts/ng-file-upload.js"));

            bundles.Add(new ScriptBundle("~/Scripts/vpb-apps/constants").Include(
                "~/app/models/constants.js"));

            bundles.Add(new ScriptBundle("~/Scripts/vpb-apps/angular").Include(
                "~/app/controllers/donViKiemToanNoiBoEditController.js",
                "~/app/controllers/donViKiemToanNoiBoInputController.js",
                "~/app/controllers/donViKiemToanNoiBoController.js",
                "~/app/controllers/dotKiemToanController.js",
                "~/app/controllers/quyMoDoiTuongKiemToanController.js",
                "~/app/controllers/quyMoDoiTuongKiemToanInputController.js",
                "~/app/controllers/quyMoDoiTuongKiemToanEditController.js",
                "~/app/controllers/quyMoDoiTuongKiemToanCopyController.js",

                // Kế hoạch năm
                "~/app/controllers/ke-hoach-nam/duyetKeHoachNamController.js",
                "~/app/controllers/ke-hoach-nam/xemCacDoiTuongKiemToanDaChonController.js",
                "~/app/controllers/ke-hoach-nam/lapKeHoachNamController.js",

                "~/app/controllers/loaiDoiTuongKiemToanController.js",
                "~/app/controllers/doiTuongKiemToanController.js",
                "~/app/controllers/dotKiemToanController.js",

                // Thực hiện kiểm toán: Khoi tao Job
                "~/app/controllers/thuc-hien-kiem-toan/khoi-tao-job/chonMangNghiepVuController.js",
                "~/app/controllers/thuc-hien-kiem-toan/khoi-tao-job/chonThanhVienDotKiemToanController.js",
                "~/app/controllers/thuc-hien-kiem-toan/khoi-tao-job/phanCongThanhVienNhapRiskProfileController.js",
                "~/app/controllers/thuc-hien-kiem-toan/khoi-tao-job/pheDuyetRiskProfileController.js",
                "~/app/controllers/thuc-hien-kiem-toan/khoi-tao-job/thongTinDotKiemToanController.js",
                "~/app/controllers/thuc-hien-kiem-toan/khoi-tao-job/nhapRiskProfileController.js",

                // Common
                "~/app/controllers/fileExplorerController.js",
                "~/app/controllers/commonController.js",

                "~/Scripts/chosen.jquery.js",

                "~/Scripts/ckeditor/ckeditor.js",
                "~/app/directives/ng-ckeditor.js",
                "~/app/controllers/app-controllers.js"));
        }
    }
}





