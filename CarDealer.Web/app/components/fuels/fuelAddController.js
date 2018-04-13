(function (app) {
    app.controller('fuelAddController', fuelAddController)

    fuelAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function fuelAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.fuels = {};

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.fuels.Alias = commonService.getSeoTitle($scope.fuels.Name);
        }

        $scope.AddFuel = AddFuel;

        function AddFuel() {
            apiService.post('api/fuel/create', $scope.fuels,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm vào cơ sở dữ liệu');
                    $state.go('fuels');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công');
                });
        };
    }
})(angular.module('cardealer.fuels'));