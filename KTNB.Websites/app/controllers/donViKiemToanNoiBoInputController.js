function donViKiemToanNoiBoInputController($scope, $route, $routeParams, $location, $http, blockUI, toastr) {
    $scope.dsTrangThai = listOfStatus;
    $scope.dsTruongPhong = [];
    $scope.item = {
        MaTruongPhong: "",
        TrangThai: true
    };

    $scope.initFunc = function () {

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

    }

    //Luu phong ban
    $scope.saveFunc = function () {
        if ($scope.myForm.$valid) {
            $http.post("/api/ThongTinDonViKiemToanNoiBo/InsertPhongBan", $scope.item)
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