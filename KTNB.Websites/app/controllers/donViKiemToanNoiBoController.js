function donViKiemToanNoiBoController($scope, $route, $routeParams, $location, $http, blockUI, toastr) {


    
    $scope.currentPage = null;
    $scope.data = {};

    $scope.initFunc = function () {

        //get Danh sach don vi kiem toan noi bo

        $scope.changePageFunc($scope.currentPage);
        var myStatus = document.getElementsByClassName("status-pb");

    };

    //Thong tin Danh sach Phong ban

    $scope.changePageFunc = function (page) {
        if (page == undefined || page == null || !angular.isNumber(page) || page < 1) {
            page = 1;
        }

        $http.get("/api/ThongTinDonViKiemToanNoiBo/GetDanhSachPhongBans", {
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

    $scope.newFormFunc = function() {
        location.href = "QL_DonViKTNB_Input.aspx";
    }

    $scope.editFunc = function(id) {
        location.href = "QL_DonViKTNB_Edit.aspx?id=" + id;
    }
}