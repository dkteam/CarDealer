(function (app) {
    app.controller('footerEditController', footerEditController)

    footerEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function footerEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.footers = {
        };

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '300px',
            allowedContent: true,

        }

        $scope.UpdateFooter = UpdateFooter;
        function UpdateFooter() {
            apiService.put('api/footer/update', $scope.footers,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công.');
                    $state.go('footers');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công');
                });
        };

        function loadFooterDetail() {
            apiService.get('api/footer/getbyid/' + $stateParams.id, null, function (result) {
                $scope.footers = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        loadFooterDetail();
    }
})(angular.module('cardealer.footers'));