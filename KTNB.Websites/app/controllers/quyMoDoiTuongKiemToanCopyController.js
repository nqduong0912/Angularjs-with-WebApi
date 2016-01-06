function quyMoDoiTuongKiemToanCopyController($scope, $route, $routeParams, $location, $http, blockUI, toastr) {

    $scope.id = null;
    $scope.DanhSachLoaiDoiTuongKiemToan = [];

    $scope.initFunc = function(id) {
        $scope.id = id;

        //get Danh sach Loai doi tuong kiem toan
        $http.get("/Api/QuyMoDTKT/DanhSachLoaiDoiTuongKiemToan", {
            params: {

            }
        }).success(function (data) {
            $scope.DanhSachLoaiDoiTuongKiemToan = data;
        }).error(function (data, status, headers, config) {
            $scope.DanhSachLoaiDoiTuongKiemToan = [];
        });

        //get Thong tin bo quy mo
        $http.get("/Api/QuyMoDTKT/GetBoQuyMobyID", {
            params: {
                id: $scope.id
            }
        }).success(function (data) {
            $scope.item = data;
        }).error(function (data, status, headers, config) {
            $scope.item = {};
        });
    }

    //Copy bo quy mo
    $scope.copyFunc = function () {
        $scope.newItem.Nam = $scope.item.Nam;
        $scope.newItem.SourceId = $scope.item.SourceId;
        $http.post("/Api/QuyMoDTKT/InsertNewBoQuyMo", $scope.newItem).success(function (data) {
            alert(data);
            window.location.href = "QuyMoDTKT.aspx";
        }).error(function (data, status, headers, config) {
            alert("Thêm mới không thành công");
        });
    }
    //Tro lai trang bo quy mo
    $scope.backFunc = function () {
        window.location.href = "QuyMoDTKT.aspx";
    }
}