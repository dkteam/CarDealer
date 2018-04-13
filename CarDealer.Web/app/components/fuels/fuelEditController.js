(function (app) {
    app.controller('fuelEditController', fuelEditController)

    fuelEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function fuelEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.fuels = {};

        $scope.UpdateFuel = UpdateFuel;
        function UpdateFuel() {
            apiService.put('api/fuel/update', $scope.fuels,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công.');
                    $state.go('fuels');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công');
                });
        };

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.fuels.Alias = commonService.getSeoTitle($scope.fuels.Name);
        }

        function loadFuelDetail() {
            apiService.get('api/fuel/getbyid/' + $stateParams.id, null, function (result) {
                $scope.fuels = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        loadFuelDetail();
    }
})(angular.module('cardealer.fuels'));