function dotKiemToanController($scope, $route, $routeParams, $location, $http, blockUI) {
    $scope.currentPage = "";
    $scope.trangThai = "";
    $scope.data = {};
    $scope.dsTrangThai = [];

    $scope.initFunc = function () {
        $http.get("/api/DotKiemToan/Get", {
            params: {
                page: $scope.currentPage,
                trangThai: $scope.trangThai
            }
        }).success(function (data) {
            $scope.dsTrangThai.splice(0, 0, { value: "", name: "Tất cả" });
            $scope.dsTrangThai.splice(1, 0, { value: "0", name: "Chưa thực hiện" });
            $scope.dsTrangThai.splice(2, 0, { value: "1", name: "Đã duyệt" });
            $scope.currentPage = data.DotKiemToanPaged.CurrentPage;
            $scope.data = data.DotKiemToanPaged;

        }).error(function (data, status, headers, config) {
            $scope.data = {};
        });
    };

    $scope.searchFunc = function (trangThai) {
        $scope.filterFunc($scope.page, trangThai);
    }

    $scope.changePageFunc = function (page) {
        $scope.filterFunc(page, $scope.trangThai);
    }

    $scope.filterFunc = function (page, trangThai) {
        if (page == undefined || page == null || !angular.isNumber(page) || page < 1) {
            page = 1;
        }

        $http.get("/api/DotKiemToan/Filter", {
            params: {
                page: page,
                trangThai: trangThai
            }
        }).success(function (data) {
            $scope.currentPage = data.CurrentPage;
            $scope.data = data;
        }).error(function (data, status, headers, config) {
            $scope.data = {};
        });
    }
}