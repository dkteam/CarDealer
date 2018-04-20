(function (app) {
    app.controller('postEditController', postEditController)

    postEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function postEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.postDetail = {
            UpdatedDate: new Date(),
            Status: true
        };

        $scope.UpdatePost = UpdatePost;
        function UpdatePost() {
            apiService.put('api/post/update', $scope.postDetail,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công.');
                    $state.go('posts');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công');
                });
        };

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '300px',
            resize_enabled: false

        }

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.postDetail.Alias = commonService.getSeoTitle($scope.postDetail.Name);
        }

        function loadPostDetail() {
            apiService.get('api/post/getbyid/' + $stateParams.id, null, function (result) {
                $scope.postDetail = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function loadParentCategory() {
            apiService.get('api/postcategory/getallparents', null, function (result) {
                $scope.categories = result.data;
            }, function () {
                console.log('cannot get list parent')
            });
        }
        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.postDetail.Image = fileUrl;
                })
            }
            finder.popup();
        }

        loadParentCategory();
        loadPostDetail();
    }
})(angular.module('cardealer.car_categories'));