(function (app) {
    app.controller('carCategoryListController', carCategoryListController);

    carCategoryListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function carCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.carCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getCarCategories = getCarCategories;
        $scope.keyWord = '';
        $scope.search = search;
        $scope.deleteCarCategory = deleteCarCategory;
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
                        checkedCarCategories: JSON.stringify(listId)
                    }
                }
                apiService.del('api/carcategory/deletemulti', config, function (result) {
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
                angular.forEach($scope.carCategories, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.carCategories, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }


        $scope.$watch("carCategories", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deletePost(id) {
            $ngBootbox.confirm('Việc xóa danh mục, sẽ xóa tất cả dòng xe thuộc danh mục đó. Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/carcategory/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

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