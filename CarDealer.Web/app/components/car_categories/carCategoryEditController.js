(function (app) {
    app.controller('carCategoryEditController', carCategoryEditController)

    carCategoryEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function carCategoryEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.carCategory = {
            CreatedDate: new Date(),
            Status: true
        };

        $scope.UpdateCarCategory = UpdateCarCategory;     
        function UpdateCarCategory() {
            apiService.put('api/carcategory/update', $scope.carCategory,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công.');
                    $state.go('car_categories');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công');
                });
        };

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.carCategory.Alias = commonService.getSeoTitle($scope.carCategory.Name);
        }

        function loadCarCategoryDetail() {
            apiService.get('api/carcategory/getbyid/' + $stateParams.id, null, function (result) {
                $scope.carCategory = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function loadParentCategory() {
            apiService.get('api/carcategory/getallparents', null, function (result) {
                $scope.parentCateogries = result.data;
            }, function () {
                console.log('cannot get list parent')
            });
        }

        loadParentCategory();
        loadCarCategoryDetail();
    }
})(angular.module('cardealer.car_categories'));