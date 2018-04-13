(function (app) {
    app.controller('transmissionTypeListController', transmissionTypeListController);

    transmissionTypeListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function transmissionTypeListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.transmissionTypes = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getTransmissionTypes = getTransmissionTypes;
        $scope.keyWord = '';
        $scope.search = search;
        $scope.deleteTransmissionType = deleteTransmissionType;
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
                        checkedtransmissionTypes: JSON.stringify(listId)
                    }
                }
                apiService.del('api/transmissiontype/deletemulti', config, function (result) {
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
                angular.forEach($scope.transmissionTypes, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.transmissionTypes, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }


        $scope.$watch("transmissionTypes", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteTransmissionType(id) {
            $ngBootbox.confirm('Việc xóa danh mục, sẽ xóa tất cả dòng xe thuộc danh mục đó. Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/transmissiontype/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        function search() {
            $scope.getTransmissionTypes();
        }

        function getTransmissionTypes(page) {
            page = page || 0;
            var config = {
                params: {
                    keyWord: $scope.keyWord,
                    page: page,
                    pageSize: 15
                }
            }

            apiService.get('api/transmissiontype/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có kết quả nào được tìm thấy.')
                }
                $scope.transmissionTypes = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load transmissionType failed.');
            });
        }

        $scope.getTransmissionTypes();
    }
})(angular.module('cardealer.transmission_types'));