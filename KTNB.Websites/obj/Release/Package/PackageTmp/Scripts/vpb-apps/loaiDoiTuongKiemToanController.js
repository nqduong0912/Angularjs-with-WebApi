function loaiDoiTuongKiemToanController($scope, $route, $routeParams, $location, $http, toastr) {
    $scope.currentPage = null;
    $scope.data = {};

    $scope.initFunc = function () {
        $scope.changePageFunc($scope.currentPage);
    };

    $scope.changePageFunc = function (page) {
        if (page == undefined || page == null || !angular.isNumber(page) || page < 1) {
            page = 1;
        }

        $http.get("/api/LoaiDoiTuongKiemToan/Get", {
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

    $scope.loadDocumentFunc = function (pkDocumentId) {
        var url = "LoaiDoiTuongKiemToan_Load.aspx?a=8b8656bb-a88d-4cdb-8a37-5dd3965c49dc&curApp=dbb1170f-fb15-4585-9c21-e21c32d4de86&an=&act=loaddoc&doc=" + pkDocumentId;
        window.location.href = url;
    }

    $scope.forwardFunc = function()
    {
        var url = "DoiTuongKiemToan.aspx?a=dbb1170f-fb15-4585-9c21-e21c32d4de86";
        window.location.href = url;
    }

    $scope.addNew = function () {
        window.location.href = "LoaiDoiTuongKiemToan_Load.aspx?a=8b8656bb-a88d-4cdb-8a37-5dd3965c49dc&curApp=dbb1170f-fb15-4585-9c21-e21c32d4de86&an=";
    }
}