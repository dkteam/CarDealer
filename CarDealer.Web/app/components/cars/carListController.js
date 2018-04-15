(function (app) {
    app.controller('carListController', carListController);

    carListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function carListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.carsDetail = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getCars = getCars;
        $scope.keyWord = '';
        $scope.search = search;
        $scope.deleteCar = deleteCar;
        $scope.selectAll = selectAll;

        $scope.deleteMultiple = deleteMultiple;

        function deleteMultiple() {
            $ngBootbox.confirm('Bạn có chắc chắn muốn xóa?').then(function () {
                var listId = [];
                $.each($scope.selected, function (i, item) {
                    listId.push(item.ID);
                });
                var config = {
                    params: {
                        checkedCars: JSON.stringify(listId)
                    }
                }
                apiService.del('api/car/deletemulti', config, function (result) {
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
                angular.forEach($scope.carsDetail, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.carsDetail, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }


        $scope.$watch("carsDetail", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteCar(id) {
            $ngBootbox.confirm('Bạn có chắc chắn muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/car/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        function search() {
            $scope.getCars();
        }

        function getCars(page) {
            page = page || 0;
            var config = {
                params: {
                    keyWord: $scope.keyWord,
                    page: page,
                    pageSize: 15
                }
            }

            apiService.get('api/car/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có kết quả nào được tìm thấy.')
                }
                $scope.carsDetail = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load cars failed.');
            });
        }

        function loadCategories() {
            apiService.get('api/carcategory/getallparents', null, function (result) {
                $scope.categories = result.data;
            }, function () {
                console.log('cannot get list parent')
            });
        }

        loadCategories()
        $scope.getCars();
    }
})(angular.module('cardealer.cars'));