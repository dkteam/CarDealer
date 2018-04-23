(function (app) {
    app.controller('slideAddController', slideAddController)

    slideAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function slideAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.slides = {
            Status: true
        };

        $scope.AddSlide = AddSlide;

        function AddSlide() {
            apiService.post('api/slide/create', $scope.slides,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm vào cơ sở dữ liệu');
                    $state.go('slides');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công');
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
    }
})(angular.module('cardealer.slides'));