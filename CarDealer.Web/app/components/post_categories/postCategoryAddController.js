(function (app) {
    app.controller('postCategoryAddController', postCategoryAddController)

    postCategoryAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function postCategoryAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.postCategory = {
            CreatedDate: new Date(),
            Status: true
        };

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.postCategory.Alias = commonService.getSeoTitle($scope.postCategory.Name);
        }

        $scope.AddPostCategory = AddPostCategory;

        function AddPostCategory() {
            apiService.post('api/postcategory/create', $scope.postCategory,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm vào cơ sở dữ liệu');
                    $state.go('post_categories');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công');
                });
        };

        function loadParentCategory() {
            apiService.get('api/postcategory/getallparents', null, function (result) {
                $scope.parentCateogries = result.data;
            }, function () {
                console.log('cannot get list parent')
            });
        }

        loadParentCategory();
    }
})(angular.module('cardealer.post_categories'));