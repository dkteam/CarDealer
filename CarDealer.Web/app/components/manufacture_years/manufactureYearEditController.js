(function (app) {
    app.controller('manufactureYearEditController', manufactureYearEditController)

    manufactureYearEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function manufactureYearEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.manufactureYear = {
            UpdatedDate: new Date(),
            Status: true
        };

        $scope.UpdateManufactureYear = UpdateManufactureYear;
        function UpdateManufactureYear() {
            apiService.put('api/manufactureyear/update', $scope.manufactureYear,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công.');
                    $state.go('manufacture_years');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công');
                });
        };

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.manufactureYear.Alias = commonService.getSeoTitle($scope.manufactureYear.Name);
        }

        function loadmanufactureYearDetail() {
            apiService.get('api/manufactureyear/getbyid/' + $stateParams.id, null, function (result) {
                $scope.manufactureYear = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        loadmanufactureYearDetail();
    }
})(angular.module('cardealer.manufacture_years'));