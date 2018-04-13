(function (app) {
    app.controller('styleEditController', styleEditController)

    styleEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function styleEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.styles = {
            UpdatedDate: new Date(),
            Status: true
        };

        $scope.UpdateStyle = UpdateStyle;
        function UpdateStyle() {
            apiService.put('api/style/update', $scope.styles,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công.');
                    $state.go('styles');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công');
                });
        };

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.styles.Alias = commonService.getSeoTitle($scope.styles.Name);
        }

        function loadStyleDetail() {
            apiService.get('api/style/getbyid/' + $stateParams.id, null, function (result) {
                $scope.styles = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        loadStyleDetail();
    }
})(angular.module('cardealer.styles'));