(function (app) {
    app.controller('pageEditController', pageEditController)

    pageEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function pageEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.pages = {
            UpdatedDate: new Date(),
            Status: true
        };

        $scope.UpdatePage = UpdatePage;
        function UpdatePage() {
            apiService.put('api/page/update', $scope.pages,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công.');
                    $state.go('pages');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công');
                });
        };

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.pages.Image = fileUrl;
                })
            }
            finder.popup();
        }

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.pages.Alias = commonService.getSeoTitle($scope.pages.Name);
        }

        function loadpageDetail() {
            apiService.get('api/page/getbyid/' + $stateParams.id, null, function (result) {
                $scope.pages = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        loadpageDetail();
    }
})(angular.module('cardealer.pages'));