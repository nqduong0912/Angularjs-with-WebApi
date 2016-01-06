function donViKiemToanNoiBoEditController($scope, $route, $routeParams, $location, $http, blockUI, toastr) {
    $scope.dsTrangThai = listOfStatus;
    $scope.id = null;
    $scope.dsTruongPhong = [];
    $scope.item = {};

    $scope.initFunc = function(id) {
        $scope.id = id;
        $scope.item.Id = id;


        //Danh sach Nguoi dung
        $http.get("/api/ThongTinDonViKiemToanNoiBo/LoadCustomUser", {
            params: {

            }
        }).success(function (data) {
            $scope.dsTruongPhong = data;
            $scope.dsTruongPhong.splice(0, 0, { PK_UserID: "", Name: "- Trưởng phòng -" });
        }).error(function (data, status, headers, config) {
            $scope.dsTruongPhong = [];
        });

        //Thong tin Phong Ban
        $http.get("/api/ThongTinDonViKiemToanNoiBo/GetPhongBan", {
            params: {
                id:$scope.id
            }
        }).success(function (data) {
            $scope.item = data;
        }).error(function (data, status, headers, config) {
            $scope.item = {};
        });
    }


    //Cap nhat Thong tin Phong Ban
    $scope.saveFunc = function () {
        if ($scope.myForm.$valid) {
            $http.post("/api/ThongTinDonViKiemToanNoiBo/UpdatePhongBan", $scope.item)
                .success(function (data) {
                    alert(data);
                    location.href = "QL_DonViKTNB.aspx";
                }).error(function (data, status, headers, config) {
                    alert("Không thể Lưu mới Phòng ban!");
                });
        } else {
            // $scope.myForm.$dirty = true;
            angular.forEach($scope.myForm.$error.required, function (field) {
                field.$setDirty();
            });
            toastr.error("Vui lòng điền các thông tin sau đó chọn Lưu.");
        }
    }
}