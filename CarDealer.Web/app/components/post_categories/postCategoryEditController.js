(function (app) {
    app.controller('postCategoryEditController', postCategoryEditController)

    postCategoryEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function postCategoryEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.postCategory = {
            UpdatedDate: new Date(),
            Status: true
        };

        $scope.UpdatePostCategory = UpdatePostCategory;
        function UpdatePostCategory() {
            apiService.put('api/postcategory/update', $scope.postCategory,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công.');
                    $state.go('post_categories');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công');
                });
        };

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.postCategory.Alias = commonService.getSeoTitle($scope.postCategory.Name);
        }

        function loadPostCategoryDetail() {
            apiService.get('api/postcategory/getbyid/' + $stateParams.id, null, function (result) {
                $scope.postCategory = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function loadParentCategory() {
            apiService.get('api/postcategory/getallparents', null, function (result) {
                $scope.parentCateogries = result.data;
            }, function () {
                console.log('cannot get list parent')
            });
        }

        loadParentCategory();
        loadPostCategoryDetail();
    }
})(angular.module('cardealer.post_categories'));