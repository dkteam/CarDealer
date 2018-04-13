(function (app) {
    app.controller('fuelListController', fuelListController);

    fuelListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function fuelListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.fuels = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getFuels = getFuels;
        $scope.keyWord = '';
        $scope.search = search;
        $scope.deleteFuel = deleteFuel;
        $scope.selectAll = selectAll;

        $scope.deleteMultiple = deleteMultiple;

        function deleteMultiple() {
            $ngBootbox.confirm('Việc xóa danh mục, sẽ xóa tất cả dòng xe thuộc danh mục đó. Bạn có chắc muốn xóa?').then(function () {
                var listId = [];
                $.each($scope.selected, function (i, item) {
                    listId.push(item.ID);
                });
                var config = {
                    params: {
                        checkedfuels: JSON.stringify(listId)
                    }
                }
                apiService.del('api/fuel/deletemulti', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                    search();
                }, function (error) {
                    notificationService.displayError('Xóa không thành công');
                });
            });
        }

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.fuels, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.fuels, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }


        $scope.$watch("fuels", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteFuel(id) {
            $ngBootbox.confirm('Việc xóa danh mục, sẽ xóa tất cả dòng xe thuộc danh mục đó. Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/fuel/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        function search() {
            $scope.getFuels();
        }

        function getFuels(page) {
            page = page || 0;
            var config = {
                params: {
                    keyWord: $scope.keyWord,
                    page: page,
                    pageSize: 15
                }
            }

            apiService.get('api/fuel/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có kết quả nào được tìm thấy.')
                }
                $scope.fuels = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load fuel failed.');
            });
        }

        $scope.getFuels();
    }
})(angular.module('cardealer.fuels'));