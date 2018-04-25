(function (app) {
    app.controller('feedbackListController', feedbackListController);

    feedbackListController.$inject = ['$scope', 'apiService', 'notificationService', '$stateParams', '$ngBootbox', '$filter'];

    function feedbackListController($scope, apiService, notificationService, $stateParams, $ngBootbox, $filter) {
        $scope.feedbacks = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getFeedbacks = getFeedbacks;
        $scope.keyWord = '';
        $scope.search = search;
        $scope.deleteFeedback = deleteFeedback;
        $scope.selectAll = selectAll;
        $scope.feedback = {};

        $scope.deleteMultiple = deleteMultiple;

        function deleteMultiple() {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var listId = [];
                $.each($scope.selected, function (i, item) {
                    listId.push(item.ID);
                });
                var config = {
                    params: {
                        checkedFeedbacks: JSON.stringify(listId)
                    }
                }
                apiService.del('api/feedback/deletemulti', config, function (result) {
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
                angular.forEach($scope.feedbacks, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.feedbacks, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }


        $scope.$watch("feedbacks", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteFeedback(id) {
            $ngBootbox.confirm('Việc xóa danh mục, sẽ xóa tất cả dòng xe thuộc danh mục đó. Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/feedback/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        function search() {
            $scope.getFeedbacks();
        }

        function getFeedbacks(page) {
            page = page || 0;
            var config = {
                params: {
                    keyWord: $scope.keyWord,
                    page: page,
                    pageSize: 15
                }
            }

            apiService.get('api/feedback/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có kết quả nào được tìm thấy.')
                }
                $scope.feedbacks = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load feedback failed.');
            });
        }

        $scope.UpdateFeedback = UpdateFeedback;
        function UpdateFeedback(id) {
            loadFeedbackDetail(id);

            apiService.put('api/feedback/update', $scope.feedback,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công.');
                    $state.go('feedbacks');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công');
                });
        };

        function loadFeedbackDetail(id) {     
            apiService.get('api/feedback/getbyid/' + id, null, function (result) {
                $scope.feedback = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        $scope.getFeedbacks();
    }
})(angular.module('cardealer.feedbacks'));