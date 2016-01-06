function lapKeHoachNamController($scope, $route, $routeParams, $location, $http, blockUI, toastr) {
    $scope.currentYear = configs.currentYear;
    $scope.currentPage = "";
    $scope.currentPage2 = "";
    $scope.loaiDTKT1 = "";
    $scope.loaiDTKT2 = "";
    $scope.tenDTKT1 = "";
    $scope.tenDTKT2 = "";

    $scope.listMonthOfYear = angular.copy(listMonthOfYear);
    $scope.listMonthOfYear.splice(0, 0, { value: "", name: "- Chọn Tháng -" });

    $scope.dsNam = [];
    $scope.rooms = [];
    $scope.users = [];
    $scope.tanSuat = [];
    $scope.boQuyMo = {};
    $scope.dsQuyMo = [];
    $scope.nguonLucNam = 0;
    $scope.nguonLucCanThiet = 0;
    $scope.trangThaiNam = {};

    Object.defineProperty($scope, "nguonLucConLai", {
        get: function () {
            return $scope.nguonLucNam - $scope.nguonLucCanThiet;
        }
    });

    // Danh sach items theo du lieu goc
    $scope.pagedItems1 = [];
    $scope.pagedItems2 = [];

    $scope.initFunc = function () {
        $scope.changeYearFunc($scope.currentYear);
    };

    $scope.changeYearFunc = function (year) {
        $scope.currentPage = "";
        $scope.currentPage2 = "";
        $scope.loaiDTKT1 = "";
        $scope.loaiDTKT2 = "";
        $scope.tenDTKT1 = "";
        $scope.tenDTKT2 = "";
        $http.get("/api/LapKeHoachNam/Get", {
            params: {
                page1: $scope.currentPage,
                page2: $scope.currentPage2,
                year: $scope.currentYear
            }
        }).success(function (data) {
            $scope.dsNam = data.DsNam;
            $scope.rooms = data.Rooms;
            $scope.users = data.Users;
            $scope.dsTanSuat = data.DsTanSuat;
            $scope.boQuyMo = data.BoQuyMo;
            $scope.dsQuyMo = data.DsQuyMo;
            $scope.dsQuyMo.splice(0, 0, { Id: "", Ten: "" });
            $scope.dsLoaiDTKT = data.DsLoaiDTKT;
            $scope.dsLoaiDTKT.splice(0, 0, { Id: "", Ten: "Tất cả" });

            $scope.currentPage = data.KeHoachNam1Paged.CurrentPage;
            $scope.data = data.KeHoachNam1Paged;
            $scope.pagedItems1[$scope.currentPage] = angular.copy(data.KeHoachNam1Paged.Items);

            $scope.currentPage2 = data.KeHoachNam2Paged.CurrentPage;
            $scope.data2 = data.KeHoachNam2Paged;
            $scope.pagedItems2[$scope.currentPage] = angular.copy(data.KeHoachNam2Paged.Items);

            $scope.nguonLucNam = data.NguonLucNam;
            $scope.nguonLucCanThiet = data.NguonLucCanThiet;
            $scope.trangThaiNam = data.TrangThaiNam;
        }).error(function (data, status, headers, config) {
            toastr.error("Có lỗi xảy ra, vui lòng làm mới lại trang.");
        });
    }

    $scope.search1Func = function (loaiDTKT1, tenDTKT1) {
        $scope.filter1Func($scope.currentPage, loaiDTKT1, tenDTKT1);
    }

    $scope.search2Func = function (loaiDTKT2, tenDTKT2) {
        $scope.filter2Func($scope.currentPage, loaiDTKT2, tenDTKT2);
    }

    $scope.changePageFunc = function (page) {
        $scope.filter1Func(page, $scope.loaiDTKT1, $scope.tenDTKT1);
    }

    $scope.changePage2Func = function (page) {
        $scope.filter2Func(page, $scope.loaiDTKT2, $scope.tenDTKT2);
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
        angular.copy($scope.pagedItems1[$scope.currentPage][index], item);
    }

    $scope.cancelEdit2Func = function (item, index) {
        item.EditMode = false;
        angular.copy($scope.pagedItems2[$scope.currentPage2][index], item);
    }

    $scope.saveFunc = function (item, index) {
        $http.post("/api/LapKeHoachNam/Edit", item).success(function (data) {
            angular.copy(item, $scope.pagedItems1[$scope.currentPage][index]);
            $scope.nguonLucCanThiet = data.NguonLucCanThiet;
            $scope.trangThaiNam = data.TrangThaiNam;
            item.EditMode = false;

            toastr.success("Cập nhật dữ liệu thành công.");
        }).error(function (data, status, headers, config) {
            toastr.error("Cập nhật dữ liệu không thành công, vui lòng kiểm tra dữ liệu và thử lại.");
        });
    }

    $scope.save2Func = function (item, index) {
        $http.post("/api/LapKeHoachNam/Edit", item).success(function (data) {
            angular.copy(item, $scope.pagedItems2[$scope.currentPage2][index]);
            $scope.nguonLucCanThiet = data.NguonLucCanThiet;
            $scope.trangThaiNam = data.TrangThaiNam;
            item.EditMode = false;

            toastr.success("Cập nhật dữ liệu thành công.");
        }).error(function (data, status, headers, config) {
            toastr.error("Cập nhật dữ liệu không thành công, vui lòng kiểm tra dữ liệu và thử lại.");
        });
    }

    $scope.xinDuyetFunc = function () {
        // toastr.warning("Func này chưa được inplement.");
        $http.post("/api/LapKeHoachNam/XinDuyet/" + $scope.currentYear, {
            // year: $scope.currentYear
        }).success(function (data) {
            $scope.trangThaiNam = data.TrangThaiNam;

            toastr.success("Đã chuyển trạng thái thành: Chưa duyệt.");
        }).error(function (data, status, headers, config) {
            toastr.error("Xin duyệt không thành công, vui lòng kiểm tra dữ liệu và thử lại.");
        });
    }

    $scope.xuatExcelFunc = function () {
        toastr.warning("Func này chưa được inplement.");
        $http.post("/api/LapKeHoachNam/XuatExcel", {
            param1: "data"
        }).success(function (data) {
            // location.href = "/";
        }).error(function (data, status, headers, config) {
            // display error message
            toastr.error("Cập nhật dữ liệu không thành công, vui lòng kiểm tra dữ liệu và thử lại.");
        });
    }

    $scope.filter1Func = function (page, loaiDTKT, tenDKKT) {
        $http.get("/api/LapKeHoachNam/Filter1", {
            params: {
                year: $scope.currentYear,
                page: page,
                loaiDTKT: loaiDTKT,
                tenDTKT: tenDKKT
            }
        }).success(function (data) {
            $scope.currentPage = data.KeHoachNam1Paged.CurrentPage;
            $scope.data = data.KeHoachNam1Paged;

            $scope.pagedItems1[$scope.currentPage] = angular.copy(data.KeHoachNam1Paged.Items);
        }).error(function (data, status, headers, config) {
            $scope.data = {};
        });
    }

    $scope.filter2Func = function (page, loaiDTKT, tenDKKT) {
        $http.get("/api/LapKeHoachNam/Filter2", {
            params: {
                year: $scope.currentYear,
                page: page,
                loaiDTKT: loaiDTKT,
                tenDTKT: tenDKKT
            }
        }).success(function (data) {
            $scope.currentPage2 = data.KeHoachNam2Paged.CurrentPage;
            $scope.data2 = data.KeHoachNam2Paged;

            $scope.pagedItems2[$scope.currentPage2] = angular.copy(data.KeHoachNam2Paged.Items);
        }).error(function (data, status, headers, config) {
            $scope.data2 = {};
        });
    }
}