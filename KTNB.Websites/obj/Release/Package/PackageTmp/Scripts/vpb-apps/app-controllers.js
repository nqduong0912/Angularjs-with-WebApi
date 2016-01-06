angular
    .module('vpbApp', ['ngRoute', 'ngAnimate', 'toastr'])
    .controller('commonController', ['$scope', '$route', '$routeParams', '$location', '$http', 'toastr', commonController])
    .controller('duyetKeHoachNamController', ['$scope', '$route', '$routeParams', '$location', '$http', 'toastr', duyetKeHoachNamController])
    .controller('xemCacDoiTuongKiemToanDaChonController', ['$scope', '$route', '$routeParams', '$location', '$http', 'toastr', xemCacDoiTuongKiemToanDaChonController])
    .controller('lapKeHoachNamController', ['$scope', '$route', '$routeParams', '$location', '$http', 'toastr', lapKeHoachNamController])
    .controller('quyMoDoiTuongKiemToanController', ['$scope', '$route', '$routeParams', '$location', '$http', 'toastr', quyMoDoiTuongKiemToanController])
    .controller('quyMoDoiTuongKiemToanInputController', ['$scope', '$route', '$routeParams', '$location', '$http', 'toastr', quyMoDoiTuongKiemToanInputController])
    .controller('quyMoDoiTuongKiemToanEditController', ['$scope', '$route', '$routeParams', '$location', '$http', 'toastr', quyMoDoiTuongKiemToanEditController])
    .controller('quyMoDoiTuongKiemToanCopyController', ['$scope', '$route', '$routeParams', '$location', '$http', 'toastr', quyMoDoiTuongKiemToanCopyController])
    .controller('donViKiemToanNoiBoController', ['$scope', '$route', '$routeParams', '$location', '$http', 'toastr', donViKiemToanNoiBoController])
    .controller('loaiDoiTuongKiemToanController', ['$scope', '$route', '$routeParams', '$location', '$http', 'toastr', loaiDoiTuongKiemToanController])
    .controller('doiTuongKiemToanController', ['$scope', '$route', '$routeParams', '$location', '$http', 'toastr', doiTuongKiemToanController])
    .controller('dotKiemToanController', ['$scope', '$route', '$routeParams', '$location', '$http', 'toastr', dotKiemToanController])
    .controller('donViKiemToanNoiBoInputController', ['$scope', '$route', '$routeParams', '$location', '$http', 'toastr', donViKiemToanNoiBoInputController])
    .directive('vpbDatepickerJs', function () {
        return {
            restrict: 'C', // 'AEC' - matches either attribute or element or class name
            require: 'ngModel',
            link: function (scope, element, attrs, ctrl) {
                $(element).datepicker({
                    dateFormat: 'dd/mm/yy',
                    onSelect: function (date) {
                        ctrl.$setViewValue(date);
                        ctrl.$render();
                        scope.$apply();
                    }
                });
            }
        };
    });