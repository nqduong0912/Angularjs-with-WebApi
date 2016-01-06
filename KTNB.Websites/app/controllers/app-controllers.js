angular
    .module("vpbApp", ["ngRoute", "ngAnimate", 'ui.bootstrap', "blockUI", "toastr", "ngCkeditor", "angular.chosen", "ngFileUpload"])
    .controller("commonController", commonController)
    .controller("duyetKeHoachNamController", duyetKeHoachNamController)
    .controller("xemCacDoiTuongKiemToanDaChonController", xemCacDoiTuongKiemToanDaChonController)
    .controller("lapKeHoachNamController", lapKeHoachNamController)
    .controller("quyMoDoiTuongKiemToanController", quyMoDoiTuongKiemToanController)
    .controller("quyMoDoiTuongKiemToanInputController", quyMoDoiTuongKiemToanInputController)
    .controller("quyMoDoiTuongKiemToanEditController", quyMoDoiTuongKiemToanEditController)
    .controller("quyMoDoiTuongKiemToanCopyController", quyMoDoiTuongKiemToanCopyController)
    .controller("donViKiemToanNoiBoController", donViKiemToanNoiBoController)
    .controller("loaiDoiTuongKiemToanController", loaiDoiTuongKiemToanController)
    .controller("doiTuongKiemToanController", doiTuongKiemToanController)
    .controller("dotKiemToanController", dotKiemToanController)
    .controller("donViKiemToanNoiBoInputController", donViKiemToanNoiBoInputController)
    .controller("donViKiemToanNoiBoEditController", donViKiemToanNoiBoEditController)

    //#region Thực hiện kiểm toán: Khoi tao Job
    .controller("thongTinDotKiemToanController", thongTinDotKiemToanController)
    .controller("chonThanhVienDotKiemToanController", chonThanhVienDotKiemToanController)
    .controller("chonMangNghiepVuController", chonMangNghiepVuController)
    .controller("phanCongThanhVienNhapRiskProfileController", phanCongThanhVienNhapRiskProfileController)
    .controller("pheDuyetRiskProfileController", pheDuyetRiskProfileController)
    .controller("nhapRiskProfileController", nhapRiskProfileController)
    //#endregion Thực hiện kiểm toán: Khoi tao Job

    .controller("fileExplorerController", fileExplorerController)
    .directive("vpbDatepickerJs", function () {
        return {
            restrict: "C", // 'AEC' - matches either attribute or element or class name
            require: "ngModel",
            link: function (scope, element, attrs, ctrl) {
                $(element).datepicker({
                    dateFormat: "dd/mm/yy",
                    onSelect: function (date) {
                        ctrl.$setViewValue(date);
                        ctrl.$render();
                        scope.$apply();
                    }
                });
            }
        };
    }).config(function (blockUIConfig) {
        // Change the default overlay message
        blockUIConfig.message = "Please wait...!";

        // Change the default delay to 100ms before the blocking is visible
        blockUIConfig.delay = 50;
    });