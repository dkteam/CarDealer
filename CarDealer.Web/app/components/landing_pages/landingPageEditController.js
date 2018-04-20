(function (app) {
    app.controller('landingPageEditController', landingPageEditController)

    landingPageEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function landingPageEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.landingPageDetail = {
        };

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '300px',
            allowedContent: true,

        }

        $scope.UpdateLandingPage = UpdateLandingPage;
        function UpdateLandingPage() {
            apiService.put('api/landingpage/update', $scope.landingPageDetail,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công.');
                    $state.go('landing_pages');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công');
                });
        };

        function loadLandingPageDetail() {
            apiService.get('api/landingpage/getbyid/' + $stateParams.id, null, function (result) {
                $scope.landingPageDetail = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.landingPageDetail.Image = fileUrl;
                })
            }
            finder.popup();
        }

        loadLandingPageDetail();
    }
})(angular.module('cardealer.landing_pages'));