function quyMoDoiTuongKiemToanEditController($scope, $route, $routeParams, $location, $http, blockUI, toastr) {

    $scope.DanhSachNam = [];
    $scope.DanhSachLoaiDoiTuongKiemToan = [];
    $scope.DanhSachQuyMo = [];
    $scope.id = null;

    $scope.initFunc = function (id) {
        $scope.id = id;


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

    $scope.updateFunc = function () {
        $scope.item.id = $scope.id;
        $http.post("/Api/QuyMoDTKT/UpdateBoQuyMo", $scope.item)
         .success(function (data) {
             window.location.href = "QuyMoDTKT.aspx";
         }).error(function (data, status, headers, config) {

         });
    }

    //insert quy mo
    $scope.insertQuyMoFunc = function () {
        if ($scope.item.LstQuyMo == undefined || $scope.item.LstQuyMo == null) {
            $scope.item.LstQuyMo = [];
        }
        $http.post("/Api/QuyMoDTKT/AddQuyMo", {
            Ten: $scope.tenQuyMo,
            NguonLuc: $scope.nguonLuc,
            BoQuyMoId: $scope.id
        }).success(function(data) {
            $scope.item.LstQuyMo.push({
                Ten: data.Ten,
                NguonLuc: data.NguonLuc
            });
            $scope.tenQuyMo = null;
            $scope.nguonLuc = null;
        }).error(function(data, status, headers, config) {
            alert("Không thể thêm mới");
        });

    }

    //Xoa gia tri quymo
    $scope.deleteQuyMoFunc = function (quyMo) {
        var del = confirm("Bạn có muốn xóa quy mô này không?");
        if (del) {
            $scope.item.LstQuyMo.pop({
                Ten: $scope.tenQuyMo,
                NguonLuc: $scope.nguonLuc
            });
            $http.post("/Api/QuyMoDTKT/DeleteQuyMo", quyMo)
                .success(function (data) {
                alert(data);
            }).error(function (data, status, headers, config) {
                    alert("Không thể xóa quy mô này");
                    $scope.insertQuyMoFunc();
                });
        }

    }

    //Tro lai trang bo quy mo
    $scope.backFunc = function () {
        window.location.href = "QuyMoDTKT.aspx";
    }

    //search bo quy mo theo nam
    $scope.namChangeFunc = function () {
        $http.get("/Api/QuyMoDTKT/SearchBoQuyMobyNam", {
            params: {
                nam: $scope.Nam
            }
        }).success(function (data) {
            $scope.item = data;
            location.href = "QuyMoDTKT.aspx";
        }).error(function (data, status, headers, config) {
            $scope.item = {};
        });
    }
    //search bo quy mo theo loai doi tuong kiem toan
    $scope.loaiDoiTuongKiemToanChangeFunc = function () {
        $http.get("/Api/QuyMoDTKT/SearchBoQuyMobyLoaiDoiTuongKiemToan", {
            params: {
                loaiDoiTuongKiemToan: $scope.LoaiDoiTuongKiemToan
            }
        }).success(function (data) {
            $scope.item = data;
            location.href = "QuyMoDTKT.aspx";
        }).error(function (data, status, headers, config) {
            $scope.item = {};
        });
    }
}
