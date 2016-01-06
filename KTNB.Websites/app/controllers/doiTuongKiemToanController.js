function doiTuongKiemToanController($scope, $route, $routeParams, $location, $http, blockUI) {
    $scope.currentPage = "";
    $scope.loaiDTKT = "";
    $scope.tenDTKT = "";
    $scope.data = {};
    $scope.dsLoaiDTKT = [];

    $scope.initFunc = function () {
        $http.get("/api/DoiTuongKiemToan/Get", {
            params: {
                page: $scope.currentPage,
                loaiDTKT: $scope.loaiDTKT,
                tenDTKT: $scope.tenDTKT
            }
        }).success(function (data) {
            $scope.dsLoaiDTKT = data.DsLoaiDTKT;
            $scope.dsLoaiDTKT.splice(0, 0, { PK_DocumentID: "", Ten: "Tất cả" });
            $scope.currentPage = data.DoiTuongKiemToanPaged.CurrentPage;
            $scope.data = data.DoiTuongKiemToanPaged;

        }).error(function (data, status, headers, config) {
            $scope.data = {};
        });
    };

    $scope.searchFunc = function (loaiDTKT, tenDTKT) {
        $scope.filterFunc($scope.page, loaiDTKT, tenDTKT);
    }

    $scope.changePageFunc = function (page) {
        $scope.filterFunc(page, $scope.loaiDTKT, $scope.tenDTKT);
    }

    $scope.filterFunc = function (page, loaiDTKT, tenDTKT) {
        if (page == undefined || page == null || !angular.isNumber(page) || page < 1) {
            page = 1;
        }

        $http.get("/api/DoiTuongKiemToan/Filter", {
            params: {
                page: page,
                loaiDTKT: loaiDTKT,
                tenDTKT: tenDTKT
            }
        }).success(function (data) {
            $scope.currentPage = data.CurrentPage;
            $scope.data = data;
        }).error(function (data, status, headers, config) {
            $scope.data = {};
        });
    }

    $scope.loadDocumentFunc = function (pkDocumentId) {
        var url = "DoiTuongKiemToan_Load.aspx?a=8b8656bb-a88d-4cdb-8a37-5dd3965c49dc&curApp=dbb1170f-fb15-4585-9c21-e21c32d4de86&an=&act=loaddoc&doc=" + pkDocumentId;
        window.location.href = url;
    }

    $scope.addNew = function () {
        window.location.href = "DoiTuongKiemToan_Load.aspx?a=8b8656bb-a88d-4cdb-8a37-5dd3965c49dc&curApp=dbb1170f-fb15-4585-9c21-e21c32d4de86&an=";
    }
}