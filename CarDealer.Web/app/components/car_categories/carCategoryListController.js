(function (app) {
    app.controller('carCategoryListController', carCategoryListController);

    carCategoryListController.$inject = ['$scope', 'apiService', 'notificationService'];

    function carCategoryListController($scope, apiService, notificationService) {
        $scope.carCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getCarCategories = getCarCategories;
        $scope.keyWord = '';

        $scope.search = search;

        function search() {
            $scope.getCarCategories();
        }

        function getCarCategories(page) {
            page = page || 0;
            var config = {
                params: {
                    keyWord: $scope.keyWord,
                    page: page,
                    pageSize: 15
                }
            }

            apiService.get('/api/carcategory/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có kết quả nào được tìm thấy.')
                } else {
                    notificationService.displaySuccess('Có ' + result.data.TotalCount + ' kết quả được tìm thấy.')
                }
                $scope.carCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load carcategory failed.');
            });
        }

        $scope.getCarCategories();
    }
})(angular.module('cardealer.car_categories'));