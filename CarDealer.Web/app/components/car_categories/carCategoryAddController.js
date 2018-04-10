(function (app) {
    app.controller('carCategoryAddController', carCategoryAddController)

    carCategoryAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function carCategoryAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.carCategory = {
            CreatedDate: new Date(),
            Status: true
        };

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.carCategory.Alias = commonService.getSeoTitle($scope.carCategory.Name);
        }

        $scope.AddCarCategory = AddCarCategory;

        function AddCarCategory() {
            apiService.post('api/carcategory/create', $scope.carCategory,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm vào cơ sở dữ liệu');
                    $state.go('car_categories');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công');
                });
        };

        function loadParentCategory() {
            apiService.get('api/carcategory/getallparents', null, function (result) {
                $scope.parentCateogries = result.data;
            }, function () {
                console.log('cannot get list parent')
            });
        }

        loadParentCategory();
    }
})(angular.module('cardealer.car_categories'));