(function (app) {
    app.controller('footerListController', footerListController);

    footerListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function footerListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.footers = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getFooters = getFooters;
        $scope.keyWord = '';

        function getFooters(page) {
            page = page || 0;
            var config = {
                params: {
                    keyWord: $scope.keyWord,
                    page: page,
                    pageSize: 15
                }
            }

            apiService.get('api/footer/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có kết quả nào được tìm thấy.')
                }
                $scope.footers = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load footer failed.');
            });
        }

        $scope.getFooters();
    }
})(angular.module('cardealer.footers'));