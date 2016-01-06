function quyMoDoiTuongKiemToanInputController($scope, $route, $routeParams, $location, $http, toastr) {
    $scope.DanhSachNam = [];
    $scope.DanhSachLoaiDoiTuongKiemToan = [];
    $scope.DanhSachQuyMo = [];
    $scope.Nam = null;
    $scope.LoaiDoiTuongKiemToan = null;
    $scope.tenBoQuyMo = null;

    $scope.initFunc = function () {

        //get Danh sach Nam
        $http.get("/Api/QuyMoDTKT/DanhSachNam", {
            params: {

            }
        }).success(function (data) {
            $scope.DanhSachNam = data;
        }).error(function (data, status, headers, config) {
            $scope.DanhSachNam = [];
        });

        //get Danh sach Loai doi tuong kiem toan
        $http.get("/Api/QuyMoDTKT/DanhSachLoaiDoiTuongKiemToan", {
            params: {

            }
        }).success(function (data) {
            $scope.DanhSachLoaiDoiTuongKiemToan = data;
        }).error(function (data, status, headers, config) {
            $scope.DanhSachLoaiDoiTuongKiemToan = [];
        });

    };


    //Them moi Bo quy mo
    $scope.insertFunc = function () {
        $http.post("/Api/QuyMoDTKT/InsertNewBoQuyMo", {
            ten : $scope.tenBoQuyMo,
            nam : $scope.Nam,
            loaiDTKT : $scope.LoaiDoiTuongKiemToan,
            LstQuyMo: $scope.DanhSachQuyMo
        }).success(function (data) {
            window.location.href = "QuyMoDTKT.aspx";
        }).error(function (data, status, headers, config) {
            alert("Thêm mới không thành công");
        });
    }

    //Them moi Quy mo
    $scope.insertQuyMoFunc = function () {
        $scope.DanhSachQuyMo.push({
            Ten: $scope.tenQuyMo,
            NguonLuc: $scope.nguonLuc
        });
        $scope.tenQuyMo = null;
        $scope.nguonLuc = null;
    }

    //Xoa gia tri quymo
    $scope.deleteQuyMoFunc = function () {
        $scope.DanhSachQuyMo.pop({
            Ten: $scope.tenQuyMo,
            NguonLuc: $scope.nguonLuc
        });
    }

    //Tro lai trang bo quy mo
    $scope.backFunc = function () {
        window.location.href = "QuyMoDTKT.aspx";
    }

}