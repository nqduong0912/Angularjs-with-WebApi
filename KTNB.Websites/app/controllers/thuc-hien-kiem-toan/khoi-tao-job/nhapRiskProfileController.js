function nhapRiskProfileController($scope, $route, $routeParams, $location, $http, blockUI, toastr) {
    $scope.rpData = {
        mangNghiepVu: '',
        quyTrinh: '',
        dsMangNghiepVu: [],
        dsQuyTrinh: [],
        dsRiskProfiles: []
    };

    /*
     * Hàm khởi tạo
     */
    $scope.initFunc = function () {
        toastr.warning("TODO: code func này.");
    }

    /*
     * Thêm mới row
     */
    $scope.addFunc = function () {
        toastr.warning("Func này chưa được implement.");
    }

    /*
     * Chỉnh sửa
     */
    $scope.editFunc = function () {
        toastr.warning("Func này chưa được implement.");
    }

    /*
     * Lưu lại
     */
    $scope.saveFunc = function () {
        toastr.warning("Func này chưa được implement.");
    }

    /*
     * Hủy sửa
     */
    $scope.cancelFunc = function () {
        toastr.warning("Func này chưa được implement.");
    }

    /*
     * Trình duyệt
     */
    $scope.applyFunc = function () {
        toastr.warning("Func này chưa được implement.");
    }
}