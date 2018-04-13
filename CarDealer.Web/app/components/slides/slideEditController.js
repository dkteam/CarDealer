(function (app) {
    app.controller('slideEditController', slideEditController)

    slideEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function slideEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.slides = {
            UpdatedDate: new Date(),
            Status: true
        };

        $scope.UpdateSlide = UpdateSlide;
        function UpdateSlide() {
            apiService.put('api/slide/update', $scope.slides,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công.');
                    $state.go('slides');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công');
                });
        };

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.slides.Image = fileUrl;
                })
            }
            finder.popup();
        }

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.slides.Alias = commonService.getSeoTitle($scope.slides.Name);
        }

        function loadSlideDetail() {
            apiService.get('api/slide/getbyid/' + $stateParams.id, null, function (result) {
                $scope.slides = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        loadSlideDetail();
    }
})(angular.module('cardealer.slides'));