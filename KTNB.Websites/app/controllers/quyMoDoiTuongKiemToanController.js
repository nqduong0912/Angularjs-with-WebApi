function quyMoDoiTuongKiemToanController($scope, $route, $routeParams, $location, $http, blockUI, toastr) {
    $scope.currentPage = null;
    $scope.data = {};
    $scope.DanhSachNam = [];
    $scope.DanhSachLoaiDoiTuongKiemToan = [];
    $scope.DanhSachQuyMo = [];
    $scope.Nam = null;
    $scope.LoaiDoiTuongKiemToan = null;



    //init Form
    $scope.initFunc = function() {

        $scope.changePageFunc($scope.currentPage);
        //get Danh sach Nam
        $http.get("/Api/QuyMoDTKT/DanhSachNam", {
            params: {

            }
        }).success(function(data) {
            $scope.DanhSachNam = data;
        }).error(function(data, status, headers, config) {
            $scope.DanhSachNam = [];
        });

        //get Danh sach Loai doi tuong kiem toan
        $http.get("/Api/QuyMoDTKT/DanhSachLoaiDoiTuongKiemToan", {
            params: {

            }
        }).success(function(data) {
            $scope.DanhSachLoaiDoiTuongKiemToan = data;
        }).error(function(data, status, headers, config) {
            $scope.DanhSachLoaiDoiTuongKiemToan = [];
        });
    };

    //Get Danh Sach Bo quy mo (phan trang)

    $scope.changePageFunc = function (page) {
        if (page == undefined || page == null || !angular.isNumber(page) || page < 1) {
            page = 1;
        }

        $http.get("/api/QuyMoDTKT/GetDanhSachBoQuyMo", {
            params: {
                page: page
            }
        }).success(function (data) {
            $scope.currentPage = data.CurrentPage;
            $scope.data = data;
        }).error(function (data, status, headers, config) {
            $scope.data = {};
        });
    }



    //Search Bo quy mo theo nam va Loai doi tuong kiem toan
    $scope.searchFunc = function () {
        $http.get("/Api/QuyMoDTKT/DanhSachBoQuyMobyNamLoaiDoiTuongKiemToan", {
            params: {
                nam: $scope.Nam,
                tenLoaiDoiTuongKiemToan: $scope.LoaiDoiTuongKiemToan
            }
        }).success(function (data) {
            $scope.currentPage = data.CurrentPage;
            $scope.data.Items = data.Items;
        }).error(function(data, status, headers, config) {
            $scope.data = {};
        });
    }


    //Sua bo quy mo
    $scope.editFunc = function(id) {
        window.location.href = "QuyMoDTKT_Edit.aspx?id=" + id;
    }


    //Copy bo quy mo

    $scope.copyFunc = function(id) {
        window.location.href = "QuyMoDTKT_Copy.aspx?id=" + id;
    }


    //On/off bo quy mo cho mot nam, mot doi tuong kiem toan

    $scope.editSatusFunc = function (item) {
        var check = confirm("Bạn có muốn thay đổi trạng thái của bộ quy mô?");
        if (check) {
            $http.post("/Api/QuyMoDTKT/UpdateStatusBoQuyMo", item).success(function (data) {
                window.location.href = "QuyMoDTKT.aspx";
            }).error(function(data, status, headers, config) {
                alert("Lỗi trong quá trình cập nhật");
            });
        }
    }

    $scope.newFormFunc = function () {
        window.location.href = "QuyMoDTKT_Input.aspx";
    }
}
