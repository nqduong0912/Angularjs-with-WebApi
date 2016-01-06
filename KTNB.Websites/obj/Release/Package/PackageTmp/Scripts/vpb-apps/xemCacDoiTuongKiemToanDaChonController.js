function xemCacDoiTuongKiemToanDaChonController($scope, $route, $routeParams, $location, $http, toastr) {
    $scope.currentPage = null;
    $scope.data = {};

    $scope.initFunc = function () {
        $scope.changePageFunc($scope.currentPage);
    };

    $scope.changePageFunc = function (page) {
        if (page == undefined || page == null || !angular.isNumber(page) || page < 1) {
            page = 1;
        }

        $http.get("/api/XemCacDoiTuongKiemToanDaChon/Get", {
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
}