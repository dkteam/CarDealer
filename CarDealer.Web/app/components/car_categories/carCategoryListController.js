(function (app) {
    app.controller('carCategoryListController', carCategoryListController);

    carCategoryListController.$inject = ['$scope', 'apiService'];

    function carCategoryListController($scope, apiService) {
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