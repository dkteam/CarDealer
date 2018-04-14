(function (app) {
    app.controller('slideAddController', slideAddController)

    slideAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function slideAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.menus = {
            Status: true
        };

        $scope.AddSlide = AddSlide;

        function AddSlide() {
            apiService.post('api/menu/create', $scope.menus,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm vào cơ sở dữ liệu');
                    $state.go('menus');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công');
                });
        };
    }
})(angular.module('cardealer.menus'));