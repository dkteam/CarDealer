(function (app) {
    app.controller('landingPageListController', landingPageListController);

    landingPageListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function landingPageListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.landingPages = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getLandingPages = getLandingPages;
        $scope.keyWord = '';

        function getLandingPages(page) {
            page = page || 0;
            var config = {
                params: {
                    keyWord: $scope.keyWord,
                    page: page,
                    pageSize: 5
                }
            }

            apiService.get('api/landingpage/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có kết quả nào được tìm thấy.')
                }
                $scope.landingPages = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load landingPage failed.');
            });
        }

        $scope.getLandingPages();
    }
})(angular.module('cardealer.landing_pages'));