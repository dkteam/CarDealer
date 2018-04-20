(function (app) {
    app.controller('supportOnlineEditController', supportOnlineEditController)

    supportOnlineEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function supportOnlineEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.supportOnlines = {};

        $scope.UpdateSupportOnline = UpdateSupportOnline;
        function UpdateSupportOnline() {
            apiService.put('api/supportonline/update', $scope.supportOnlines,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công.');
                    $state.go('home');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công');
                });
        };

        function loadsupportOnlineDetail() {
            apiService.get('api/supportonline/getbyid', null, function (result) {
                $scope.supportOnlines = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        loadsupportOnlineDetail();
    }
})(angular.module('cardealer.support_onlines'));