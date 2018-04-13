(function (app) {
    app.controller('homeController', homeController);
    homeController.$inject = ['$scope'];
    function homeController($scope) {
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