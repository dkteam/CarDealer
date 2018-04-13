(function (app) {
    app.controller('transmissionTypeEditController', transmissionTypeEditController)

    transmissionTypeEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function transmissionTypeEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.transmissionTypes = {
            UpdatedDate: new Date(),
            Status: true
        };

        $scope.UpdateTransmissionType = UpdateTransmissionType;
        function UpdateTransmissionType() {
            apiService.put('api/transmissiontype/update', $scope.transmissionTypes,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công.');
                    $state.go('transmission_types');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công');
                });
        };

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.transmissionTypes.Alias = commonService.getSeoTitle($scope.transmissionTypes.Name);
        }

        function loadtransmissionTypeDetail() {
            apiService.get('api/transmissiontype/getbyid/' + $stateParams.id, null, function (result) {
                $scope.transmissionTypes = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        loadtransmissionTypeDetail();
    }
})(angular.module('cardealer.transmission_types'));