(function (app) {
    app.controller('transmissionTypeAddController', transmissionTypeAddController)

    transmissionTypeAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function transmissionTypeAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.transmissionTypes = {
            CreatedDate: new Date(),
            Status: true
        };

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.transmissionTypes.Alias = commonService.getSeoTitle($scope.transmissionTypes.Name);
        }

        $scope.AddTransmissionType = AddTransmissionType;

        function AddTransmissionType() {
            apiService.post('api/transmissiontype/create', $scope.transmissionTypes,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm vào cơ sở dữ liệu');
                    $state.go('transmission_types');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công');
                });
        };
    }
})(angular.module('cardealer.transmission_types'));