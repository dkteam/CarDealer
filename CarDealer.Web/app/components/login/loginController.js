
(function (app) {
    app.controller('loginController', ['$scope', 'loginService', '$injector', 'notificationService',
        function ($scope, loginService, $injector, notificationService) {

            $scope.loginData = {
                userName: "",
                password: ""
            };

            $scope.loginSubmit = function () {
                loginService.logIn($scope.loginData.userName, $scope.loginData.password).then(function (response) {
                    if (response != null && response.data.error != undefined) {
                        notificationService.displayError(response.data.error);
                    }
                    else {
                        var stateService = $injector.get('$state');
                        stateService.go('posts');
                    }
                });
            }
        }]);    
})(angular.module('cardealer'));