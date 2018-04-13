(function (app) {
    app.controller('manufactureYearAddController', manufactureYearAddController)

    manufactureYearAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function manufactureYearAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.manufactureYear = {
            CreatedDate: new Date(),
            Status: true
        };

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.manufactureYear.Alias = commonService.getSeoTitle($scope.manufactureYear.Name);
        }

        $scope.AddManufactureYear = AddManufactureYear;

        function AddManufactureYear() {
            apiService.post('api/manufactureyear/create', $scope.manufactureYear,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm vào cơ sở dữ liệu');
                    $state.go('manufacture_years');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công');
                });
        };
    }
})(angular.module('cardealer.manufacture_years'));