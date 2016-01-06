function duyetKeHoachNamController($scope, $route, $routeParams, $location, $http, toastr) {
    $scope.working = false;

    $scope.roles = { banGDKhoiId: '', isBanGDKhoi: false, banKSId: '', isBanKS: false };
    $scope.currentYear = configs.currentYear;
    $scope.currentPage = '';
    $scope.currentLoaiDTKT = '';

    $scope.listMonthOfYear = angular.copy(listMonthOfYear);
    $scope.listMonthOfYear.splice(0, 0, { value: '', name: '- Chọn Tháng -' });

    $scope.dsNam = [];
    $scope.rooms = [];
    $scope.users = [];
    $scope.tanSuat = [];
    $scope.boQuyMo = {};
    $scope.dsQuyMo = [];
    $scope.nguonLucNam = 0;
    $scope.nguonLucCanThiet = 0;
    $scope.trangThaiNam = {};

    Object.defineProperty($scope, 'nguonLucConLai', {
        get: function () {
            return $scope.nguonLucNam - $scope.nguonLucCanThiet;
        }
    });

    // Danh sach items theo du lieu cho phep edit
    // $scope.items = [];
    // Danh sach items theo du lieu goc
    $scope.pagedItems = [];

    $scope.initFunc = function (banGDKhoiId, banKSId) {
        $scope.roles.isBanGDKhoi = userInfo.isInGroup(banGDKhoiId);
        $scope.roles.isBanKS = userInfo.isInGroup(banKSId);

        $http.get("/api/DuyetKeHoachNam/Get", {
            params: {
                page: '',
                year: '',
                loaiDTKT: ''
            }
        }).success(function (data) {
            $scope.dsNam = data.DsNam;
            $scope.rooms = data.Rooms;
            $scope.users = data.Users;
            $scope.dsTanSuat = data.DsTanSuat;
            $scope.boQuyMo = data.BoQuyMo;
            $scope.dsLoaiDTKT = data.DsLoaiDTKT;
            $scope.dsLoaiDTKT.splice(0, 0, { Id: '', Ten: 'Tất cả' });

            $scope.currentPage = data.KeHoachNamPaged.CurrentPage;
            $scope.data = data.KeHoachNamPaged;
            $scope.nguonLucNam = data.NguonLucNam;
            $scope.nguonLucCanThiet = data.NguonLucCanThiet;
            $scope.trangThaiNam = data.TrangThaiNam;

            $scope.pagedItems[$scope.currentPage] = angular.copy(data.KeHoachNamPaged.Items);
        }).error(function (data, status, headers, config) {
            toastr.error("Có lỗi xảy ra, vui lòng làm mới lại trang.");
        });
    };

    $scope.changeYear = function (year) {
        $scope.filterFunc(null, year, $scope.currentLoaiDTKT);
    }

    $scope.changeLoaiDTKT = function (loaiDTKT) {
        $scope.filterFunc(null, $scope.currentYear, loaiDTKT);
    }

    $scope.changePageFunc = function (page) {
        $scope.filterFunc(page, $scope.currentYear, $scope.currentLoaiDTKT);
    }

    $scope.filterFunc = function (page, year, loaiDTKT) {
        if (page == undefined || page == null || !angular.isNumber(page) || page < 1) {
            page = 1;
        }

        $http.get("/api/DuyetKeHoachNam/Filter", {
            params: {
                page: page,
                year: year,
                loaiDTKT: loaiDTKT
            }
        }).success(function (data) {
            $scope.currentPage = data.KeHoachNamPaged.CurrentPage;
            $scope.data = data.KeHoachNamPaged;

            $scope.pagedItems[$scope.currentPage] = angular.copy(data.KeHoachNamPaged.Items);

            $scope.nguonLucNam = data.NguonLucNam;
            $scope.nguonLucCanThiet = data.NguonLucCanThiet;
        }).error(function (data, status, headers, config) {
            $scope.data = {};
        });
    }

    $scope.changeQuyMoFunc = function (item) {
        item.QuyMo = $.grep($scope.dsQuyMo, function (qm, index) {
            return qm.Id == item.QuyMoId;
        })[0].Ten;
    }

    $scope.changeTanSuatFunc = function (item) {
        item.TanSuatText = $.grep($scope.dsTanSuat, function (ts, index) {
            return ts.SourceId == item.TanSuat;
        })[0].Name;
    }

    $scope.changePhongFunc = function (item) {
        item.PhongText = $.grep($scope.rooms, function (room, index) {
            return room.SourceId == item.Phong;
        })[0].Ten;
    }

    $scope.changeLeaderFunc = function (item) {
        item.TruongDoanText = $.grep($scope.users, function (user, index) {
            return user.PK_UserID == item.TruongDoan;
        })[0].FullName;
    }

    $scope.changeManagerFunc = function (item) {
        item.ManagerText = $.grep($scope.users, function (user, index) {
            return user.PK_UserID == item.Manager;
        })[0].FullName;
    }

    $scope.editFunc = function (item, index) {
        item.EditMode = true;
    }

    $scope.cancelEditFunc = function (item, index) {
        item.EditMode = false;
        angular.copy($scope.pagedItems[$scope.currentPage][index], item);
    }

    $scope.saveFunc = function (item, index) {
        $http.post("/api/DuyetKeHoachNam/Edit", item).success(function (data) {
            angular.copy(item, $scope.pagedItems[$scope.currentPage][index]);
            $scope.nguonLucCanThiet = data.NguonLucCanThiet;
            item.EditMode = false;

            toastr.success("Cập nhật dữ liệu thành công.");
        }).error(function (data, status, headers, config) {
            toastr.error("Cập nhật dữ liệu không thành công, vui lòng kiểm tra dữ liệu và thử lại.");
        });
    }

    $scope.duyetFunc = function () {
        $http.post("/api/DuyetKeHoachNam/Duyet/" + $scope.currentYear, {
            // param1: 'data'
        }).success(function (data) {
            $scope.trangThaiNam = data.TrangThaiNam;
            toastr.success("Trạng thái hiện tại: " + data.TrangThaiNam.TrangThaiKeHoachNamText);
        }).error(function (data, status, headers, config) {
            // display error message
            toastr.error("Cập nhật dữ liệu không thành công, vui lòng kiểm tra dữ liệu và thử lại.");
        });
    }

    $scope.tuChoiFunc = function () {
        $http.post("/api/DuyetKeHoachNam/TuChoi/" + $scope.currentYear, {
            // param1: 'data'
        }).success(function (data) {
            $scope.trangThaiNam = data.TrangThaiNam;
            toastr.success("Trạng thái hiện tại: " + data.TrangThaiNam.TrangThaiKeHoachNamText);
        }).error(function (data, status, headers, config) {
            // display error message
            toastr.error("Cập nhật dữ liệu không thành công, vui lòng kiểm tra dữ liệu và thử lại.");
        });
    }

    $scope.xuatExcelFunc = function () {
        toastr.warning("Func này chưa được inplement.");
        $http.post("/api/DuyetKeHoachNam/XuatExcel", {
            param1: 'data'
        }).success(function (data) {
            // location.href = "/";
        }).error(function (data, status, headers, config) {
            // display error message
            toastr.error("Cập nhật dữ liệu không thành công, vui lòng kiểm tra dữ liệu và thử lại.");
        });
    }
}