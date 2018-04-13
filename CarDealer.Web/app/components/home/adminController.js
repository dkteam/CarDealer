(function (app) {
    app.controller('adminController', adminController);
    adminController.$inject = ['$scope'];
    function adminController($scope) {
        $scope.uploadImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                //$scope.$apply(function () {
                //    $scope.slides.Image = fileUrl;
                //})
            }
            finder.popup();
        }
    }
})(angular.module('cardealer'));