function thongTinDotKiemToanController($scope, $route, $routeParams, $location, $http, blockUI, toastr, Upload) {
    $scope.editorOptions = {
        language: "vi",
        uiColor: "#48B061",
        height: "90px"
    };
    var viewStates = { quanLy: 0, suaTenMangNghiepVu: 1 };
    $scope.currentView = viewStates.quanLy;

    $scope.id = "";
    $scope.data = {};
    $scope.dsThang = listMonthOfYear;
    $scope.dsThang.splice(0, 0, { value: "", name: "- Chọn Tháng -" });
    $scope.loaiDTKT = "";
    $scope.dsLoaiDTKT = [];
    $scope.DTKT = "";
    $scope.dsDTKT = [];
    $scope.donViLienQuan = [];
    $scope.uploadFiles = [];
    // khai bao bien trong tab Chon thanh vien doan kiem toan
    $scope.tab2data = {};
    $scope.tab2LstUser = [];
    $scope.tab2LoaiDoiTuongKiemToanId = null;
    $scope.dsUserInDotKT = [];
    $scope.tab2User = {
        PK_UserID: ""
    };
    $scope.tab2User.PK_UserID = "";
    $scope.tab2LstUploadFile = [];
    $scope.baseUrl = configs.baseUrl;
    $scope.lstMangNghiepVu = [];
    $scope.dsThanhVienQuyTrinh = [];
    $scope.tab3lstRiskProfile = {};
    $scope.pagedItems3 = [];
    $scope.currentPage3 = "";
    $scope.mangNghiepVuDocumentId = null;
    $scope.mangNghiepVuChosen = {
        PK_DocumentID : "",
        Status: null,
        Ten:"",
        DienGiai:""
    };


    $scope.initFunc = function (dotKiemToanId, loaiDoiTuongKiemToanId) {
        // Block the user interface
        blockUI.start();
        $scope.id = dotKiemToanId;
        $scope.tab2LoaiDoiTuongKiemToanId = loaiDoiTuongKiemToanId;
        $http.get("/api/ThongTinDotKiemToan/Get", {
            params: {
                id: $scope.id,
                loaiDoiTuongKiemToanId: $scope.tab2LoaiDoiTuongKiemToanId
            }
        }).success(function (data) {
            $scope.data = data;
            $scope.dsLoaiDTKT = data.DsLoaiDTKT;
            if (data.DsDonViLienQuan != null) {
                $scope.donViLienQuan = data.DsDonViLienQuan;
            }

            if (data.DsFileThongTinChung != null) {
                $scope.uploadFiles = data.DsFileThongTinChung;
            }

            // tab 2
            $scope.tab2LstUser = data.DsUsers;
            $scope.tab2LstUser.splice(0, 0, { PK_UserID: "", Name: "- Chọn Thành viên -", IsThanhVienDotKiemToan: false });
            if (data.DsUploadFileByThanhVien != undefined || data.DsUploadFileByThanhVien != null) {
                $scope.tab2LstUploadFile = data.DsUploadFileByThanhVien;
            }

            if (data.DsMangNghiepVu != undefined || data.DsMangNghiepVu != null) {
                $scope.lstMangNghiepVu = data.DsMangNghiepVu;
            }

            //tab 4
            $scope.dsUserInDotKT = data.DsUsers.filter(function (el) {
                return el.IsThanhVienDotKiemToan;
            });
            $scope.dsThanhVienQuyTrinh = data.DsThanhVienTrongDotKT.filter(function (el) {
                return el.QuyTrinh;
            });
            // Unblock the user interface
            blockUI.stop();
        }).error(function (data, status, headers, config) {
            $scope.data = {};
            $scope.tab2LstUser = [];

            // Unblock the user interface
            blockUI.stop();
        });
    };

    $scope.changeLoaiDTKT = function (idLoaiDTKT) {
        $http.get("/api/ThongTinDotKiemToan/Filter", {
            params: {
                loaiDTKT: idLoaiDTKT
            }
        }).success(function (data) {
            $scope.dsDTKT = data.DsDTKT;
        }).error(function (data, status, headers, config) {
            $scope.data = {};
        });
    }

    $scope.changeMangNV = function (mangNghiepVuId) {
        $http.get("/api/ThongTinDotKiemToan/GetDsQuyTrinh", {
            params: {
                idMangNv: mangNghiepVuId
            }
        }).success(function (data) {
            $scope.dsQuyTrinh = data.DsQuyTrinh;
        }).error(function (data, status, headers, config) {
            $scope.data = {};
        });
    }

    $scope.chonDonVi = function (DTKT, LoaiDTKT) {
        if (DTKT == null || DTKT == "" || LoaiDTKT == null || LoaiDTKT == "") {
            toastr.error("Chọn đối tượng kiểm toán.");
            return;
        }
        for (var i = 0; i < $scope.donViLienQuan.length; i++) {
            if ($scope.donViLienQuan[i].IDDTKT == DTKT) {
                toastr.error("Đối tượng kiểm toán đã tồn tại trong danh sách.");
                return;
            }
        }
        var DTKTText = $.grep($scope.dsDTKT, function (item, index) {
            return item.PK_DocumentID == DTKT;
        })[0].Ten;

        var LoaiDTKTText = $.grep($scope.dsLoaiDTKT, function (item, index) {
            return item.PK_DocumentID == LoaiDTKT;
        })[0].Ten;
        var donvi = {
            IDDTKT: DTKT,
            TenDTKT: DTKTText,
            IDLoaiDTKT: LoaiDTKT,
            TenLoaiDTKT: LoaiDTKTText
        };
        $scope.donViLienQuan.push(donvi);
        //$scope.donViLienQuan.splice(0, 0, { value: '', name: '- Chọn Tháng -' })
    }

    $scope.ganTV = function (thanhVienId, quyTrinhId, mangNghiepVuId) {
        if (!mangNghiepVuId) {
            toastr.error("Chọn mảng nghiệp vụ.");
            return;
        }
        if (!quyTrinhId) {
            toastr.error("Chọn quy trình.");
            return;
        }
        if (!thanhVienId) {
            toastr.error("Chọn thành viên.");
            return;
        }
        for (var i = 0; i < $scope.dsThanhVienQuyTrinh.length; i++) {
            if ($scope.dsThanhVienQuyTrinh[i].PK_UserID == thanhVienId) {
                toastr.error("Thành viên đã tồn tại trong danh sách.");
                return;
            }
        }
        var quyTrinhText = $.grep($scope.dsQuyTrinh, function (item, index) {
            return item.PK_DocumentID == quyTrinhId;
        })[0].Ten;

        var UserName = $.grep($scope.dsUserInDotKT, function (item, index) {
            return item.PK_UserID == thanhVienId;
        })[0].FullName;
        var thanhvien = {
            PK_UserID: thanhVienId,
            UserName: UserName,
            QuyTrinh: quyTrinhId,
            QuyTrinhText: quyTrinhText
        };
        $scope.dsThanhVienQuyTrinh.push(thanhvien);
    }

    $scope.deleteDV = function (idDTKT) {
        if (confirm("Bạn chắc chắn muốn xóa ?")) {
            for (var i = 0; i < $scope.donViLienQuan.length; i++) {
                var obj = $scope.donViLienQuan[i];

                if (obj.IDDTKT == idDTKT) {
                    $scope.donViLienQuan.splice(i, 1);
                    return;
                }
            }
        }
    }

    $scope.deleteTV = function (PK_UserID) {
        if (confirm("Bạn chắc chắn muốn xóa ?")) {
            for (var i = 0; i < $scope.dsThanhVienQuyTrinh.length; i++) {
                var obj = $scope.dsThanhVienQuyTrinh[i];

                if (obj.PK_UserID == PK_UserID) {
                    $scope.dsThanhVienQuyTrinh.splice(i, 1);
                    return;
                }
            }
        }
    }

    $scope.xoaFile = function (name) {
        if (confirm("Bạn chắc chắn muốn xóa ?")) {
            for (var i = 0; i < $scope.uploadFiles.length; i++) {
                var obj = $scope.uploadFiles[i];

                if (obj.Name == name) {
                    $scope.uploadFiles.splice(i, 1);
                    //TODO: Xóa file trên server
                    return;
                }
            }
        }
    }

    $scope.luuThongTin = function (item) {
        if (!item.DotKT) {
            toastr.error("Nhập tên đợt kiểm toán.");
            return;
        }

        if (!item.MucTieu) {
            toastr.error("Nhập mục tiêu đợt kiểm toán.");
            return;
        }

        if (!item.PhamVi) {
            toastr.error("Nhập phạm vi đợt kiểm toán.");
            return;
        }

        item.DonViLienQuan = JSON.stringify($scope.donViLienQuan);
        item.FileThongTinChung = JSON.stringify($scope.uploadFiles);
        $http.post("/api/ThongTinDotKiemToan/Edit", item).success(function (data) {
            toastr.success("Cập nhật dữ liệu thành công.");
        }).error(function (data, status, headers, config) {
            var errors = data.ModelState;
            var errorMsg = "";
            angular.forEach(errors, function (value, key) {
                errorMsg += value + "<br />";
            }, errorMsg);
            // toastr.error("Cập nhật dữ liệu không thành công, vui lòng kiểm tra dữ liệu và thử lại.");
            toastr.error(errorMsg, {
                allowHtml: true
            });
        });

        $scope.loadDocumentFunc = function (pkDocumentId) {
            var url = "KhoiTaoJob.aspx?a=2f17115e-4dcd-4691-bf4f-712fe3cffa79&curApp=0c2a2496-b6d9-492e-9fb2-a29537f7d32f&an=&c=f58fa6b2-b974-481c-9acf-739198ddd983&cn=&nw=0&id=" + pkDocumentId;
            window.location.href = url;
        }
    }

    $scope.luuDsThanhVienQuyTrinh = function (dotkiemtoanID, item) {
        $http.post("/api/ThongTinDotKiemToan/EditListUser/" + dotkiemtoanID, item).success(function (data) {
            toastr.success("Cập nhật dữ liệu thành công.");
        }).error(function (data, status, headers, config) {
            toastr.error("Cập nhật không thành công, vui lòng kiểm tra dữ liệu và thử lại.");
        });
    }

    //Tab Chon Thanh vien doan kiem toan

    //Them moi thanh vien vao dot kiem toan
    $scope.insertUserFunc = function (userId) {
        if (($scope.myForm.newUser.$valid)) {
            var user = $.grep($scope.tab2LstUser, function (item, index) {
                return item.PK_UserID === userId;
            })[0];
            user.IdDotKiemToan = $scope.id;
            user.IsThanhVienDotKiemToan = true;
            $scope.tab2User.PK_UserID = "";
            $scope.myForm.newUser.$setPristine();
        } else {
            angular.forEach($scope.myForm.$error.required, function (field) {
                field.$setDirty();
            });
        }
    }

    //delete nhan vien dot kiem toan
    $scope.deleteUserFunc = function (item) {
        var isDelete = confirm("Bạn có muốn xóa thành viên khỏi đoàn kiểm toán?");
        if (isDelete) {
            item.IsThanhVienDotKiemToan = false;
        }
    }

    $scope.saveThanhVienFunc = function () {
        var dsThanhVien = $.grep($scope.tab2LstUser, function (item, index) {
            return item.IsThanhVienDotKiemToan === true;
        });

        $scope.tab2data = { DsThanhVienDotKiemToan: dsThanhVien, DsUpLoadFileDotKiemToan: $scope.tab2LstUploadFile };

        $http.post("/api/ThanhVienDotKiemToan/InsertDanhSachDoanKiemToan", $scope.tab2data)
            .success(function (data) {
                $scope.dsUserInDotKT = $scope.tab2LstUser.filter(function (el) {
                    return el.IsThanhVienDotKiemToan;
                });
                toastr.success(data);
            }).error(function (data, status, headers, config) {
                toastr.error("Thêm mới không thành công. Vui lòng kiểm tra lại!");
            });
    }

    $scope.deleteFilebyThanhVienFunc = function (file) {
        var isDelelte = confirm("Bạn có chắc chắn xóa file đã chọn?");
        if (isDelelte) {
            $scope.tab2LstUploadFile.pop(file);
        }
    }

    $scope.editMangNghiepVuFunc = function (item) {
        if (($scope.myForm.mangNghiepVuChoice.$valid)) {
            $scope.mangNghiepVuChosen = angular.copy(item);
            $scope.myForm.mangNghiepVuChoice.$setPristine();
            $scope.setView(viewStates.suaTenMangNghiepVu);
        } else {
            $scope.myForm.mangNghiepVuChoice.$setDirty();
        }
        
    }

    $scope.luuSuaMangNghiepVu = function () {
        toastr.warning("Func này chưa được implement.");
    }

    $scope.huySuaMangNghiepVuFunc = function () {
        $scope.setView(viewStates.quanLy);
    }

    $scope.submitKhoiTaoJobFunc = function () {
        blockUI.start();
        $http.post("/api/ThongTinDotKiemToan/SubmitJob/" + $scope.id, {})
           .success(function (data) {
               blockUI.stop();
               toastr.success("Submit thành công!");
           }).error(function (data, status, headers, config) {
               blockUI.stop();
               toastr.error("Submit không thành công;!");
           });
        // TODO: check xem khi nao thi moi duoc submit
    }

    //#region Thông tin chung Tab



    //#endregion Thông tin chung Tab

    //#region Chọn thành viên đợt kiểm toán



    //#endregion Chọn thành viên đợt kiểm toán

    //#region Chọn mảng nghiệp vụ Tab

    $scope.saveMangNghiepVuFunc = function () {
        toastr.warning("Func này chưa được implement.");
    }

    $scope.editMangNghiepVu_QuyTrinhFunc = function () {
        toastr.warning("Func này chưa được implement.");
    }

    $scope.downloadRiskProfileFunc = function () {
        toastr.warning("Func này chưa được implement.");
    }

    $scope.uploadRiskProfileFunc = function () {
        toastr.warning("Func này chưa được implement.");
    }

    //#endregion Chọn mảng nghiệp vụ Tab

    //#region Phan cong thanh vien nhap Risk profile Tab



    //#endregion Phan cong thanh vien nhap Risk profile Tab

    //#region Phe duyet Risk profile Tab

    $scope.approveTab = {
        currentQuyTrinhId: 1,
        currentThanhVienId: 1,
        dsQuyTrinh: [
            { value: 1, text: "Quy trinh 1" },
            { value: 2, text: "Quy trinh 2" },
            { value: 3, text: "Quy trinh 3" }
        ],
        dsThanhVien: [
            { value: 1, text: "Thanh vien 1" },
            { value: 2, text: "Thanh vien 2" },
            { value: 3, text: "Thanh vien 3" }
        ],
        dsRiskProfile: [
            { MangNghiepVu: "Mảng nghiệp vụ 1", QuyTrinh: "Quy trình 1", ThanhVien: "Damnv", Buoc: "Bước 1", MucTieu: "Mục tiêu 1", RuiRo: "Rủi ro 1", KiemSoat: "Kiểm soát 1", ThuTuc: "Thủ tục 1" },
            { MangNghiepVu: "Mảng nghiệp vụ 1", QuyTrinh: "Quy trình 1", ThanhVien: "Tieplh", Buoc: "Bước 2", MucTieu: "Mục tiêu 1", RuiRo: "Rủi ro 2", KiemSoat: "Kiểm soát 2", ThuTuc: "Thủ tục 2" },
            { MangNghiepVu: "Mảng nghiệp vụ 1", QuyTrinh: "Quy trình 1", ThanhVien: "Huynq8", Buoc: "Bước 1", MucTieu: "Mục tiêu 1", RuiRo: "Rủi ro 3", KiemSoat: "Kiểm soát 3", ThuTuc: "Thủ tục 3" }
        ]
    };

    $scope.tuChoiDuyetApproveTabFunc = function () {
        toastr.warning("Func này chưa được implement.");
    }

    $scope.suaRiskProfileApproveTabFunc = function (item) {
        item.IsEdit = true;
    }

    $scope.luuRiskProfileApproveTabFunc = function (item) {
        item.IsEdit = false;
    }

    $scope.huyRiskProfileApproveTabFunc = function (item) {
        item.IsEdit = false;
    }

    $scope.submitApproveTabFunc = function () {
        toastr.warning("Func này chưa được implement.");
    }

    //#endregion Phe duyet Risk profile Tab

    //#region Upload file
    $scope.uploadPic = function (files, environment) {
        $scope.formUpload = true;
        if (files == null) {
            toastr.error("Chọn File để thêm.");
            return;
        }
        upload(files[0], environment);
    };

    function upload(file, environment) {
        $scope.errorMsg = null;
        uploadUsingUpload(file, environment);
    }

    function uploadUsingUpload(file, environment) {
        file.upload = Upload.upload({
            url: "/api/FileExplorer/Post",
            method: "POST",
            headers: {
                'my-header': "my-header-value"
            },
            //fields: { Environment: environment },
            data: { Environment: environment },
            file: file//,
            //fileFormDataName: 'myFile'
        });

        file.upload.then(function (response) {
            // file: response.data.Files[0].Name
            if (response.data.Successful) {
                if (response.data.Environment === "tab1") {
                    for (var i = 0; i < $scope.uploadFiles.length; i++) {
                        var obj = $scope.uploadFiles[i];

                        if (obj.Name == response.data.Files[0].Name) {
                            toastr.error("File đã tồn tại trong danh sách.");
                            return;
                        }
                    }
                    //$scope.uploadFiles.push(response.data.Files[0]);
                    $scope.uploadFiles.push({
                        Name: response.data.Files[0].Name,
                        Path: response.data.Files[0].Path,
                        CreateDate: response.data.ServerTime,
                        Id: response.data.Files[0].Id
                    });
                } else if (response.data.Environment === "tab2") {
                    $scope.tab2LstUploadFile.push({
                        Name: response.data.Files[0].Name,
                        Path: response.data.Files[0].Path,
                        CreateDate: response.data.ServerTime,
                        Id: response.data.Files[0].Id
                    });

                    $scope.picFile2 = null;
                }
            } else {
                toastr.error("Thêm file không thành công. Vui lòng thử lại.");
            }
        }, function (response) {
            toastr.error("Thêm file không thành công. Vui lòng thử lại.");
        });

        file.upload.progress(function (evt) {
            // Math.min is to fix IE which reports 200% sometimes
            file.progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
        });

        file.upload.xhr(function (xhr) {
            // xhr.upload.addEventListener('abort', function(){console.log('abort complete')}, false);
        });
    }
    //#endregion Upload file

    $scope.setView = function (viewState) {
        $scope.currentView = viewState;
        switch (viewState) {
            case viewStates.suaTenMangNghiepVu:
                $("#ContentPlaceHolder1_lblFormCaption").text("SỬA TÊN MẢNG NGHIỆP VỤ");
                break;
            case viewStates.quanLy:
                $("#ContentPlaceHolder1_lblFormCaption").text("ĐỢT KIỂM TOÁN");
                break;
            default:
                $("#ContentPlaceHolder1_lblFormCaption").text("ĐỢT KIỂM TOÁN");
                $scope.currentView = viewStates.quanLy;
                break;
        }
    }

    // Xem RiskProfile
    $scope.viewRiskProfileFunc = function (id) {
        if (($scope.myForm.mangNghiepVu.$valid)) {
            blockUI.start();
            $scope.changePageRiskProfileFunc($scope.currentPage3, id);
            blockUI.stop();
            $scope.myForm.mangNghiepVu.$setPristine();
        } else {
            angular.forEach($scope.myForm.$error.required, function (field) {
                field.$setDirty();
            });
        }
        
    }

    $scope.changePageRiskProfileFunc = function (page, documentId) {
        $http.get("/api/ThongTinDotKiemToan/GetRiskProfileByMangNghiepVu", {
            params: {
                documentID: documentId,
                page: page

            }
        }).success(function (data) {
            $scope.currentPage3 = data.DsRiskProfile.CurrentPage;
            $scope.tab3lstRiskProfile = data.DsRiskProfile;
            $scope.pagedItems3[$scope.currentPage3] = angular.copy(data.DsRiskProfile.Items);
        }).error(function (data, status, headers, config) {
            $scope.tab3lstRiskProfile = {};
        });
    }

    $scope.changePage2Func = function (page) {
        $scope.changePageRiskProfileFunc(page, $scope.mangNghiepVuDocumentId);
    }
}