function commonController($scope, $route, $routeParams, $location, $http, blockUI, toastr, Upload) {
    $scope.editorOptions = {
        language: "vi",
        uiColor: "#48B061",
        height: "90px"
    };

    $scope.initFunc = function () {
        toastr.warning("Func này chưa được implement.");
    }
}