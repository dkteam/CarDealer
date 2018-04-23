(function (app) {
    app.controller('rootController', rootController);

    rootController.$inject = ['$state', 'authData', 'loginService', '$scope', 'authenticationService'];

    function rootController($state, authData, loginService, $scope, authenticationService) {
        $scope.logOut = function () {
            loginService.logOut();
            $state.go('login');
        }
        $scope.authentication = authData.authenticationData;
        $scope.sideBar = "/app/shared/views/sideBar.html";

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.popup();
        }
        //authenticationService.validateRequest();
    }
})(angular.module('cardealer'));