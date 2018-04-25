(function (app) {
    app.controller('feedbackEditController', feedbackEditController)

    feedbackEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function feedbackEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.feedback = {
        };

        $scope.UpdateFeedback = UpdateFeedback;
        function UpdateFeedback() {
            apiService.put('api/feedback/update', $scope.feedback,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công.');
                    $state.go('feedbacks');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công');
                });
        };
 
        function loadFeedbackDetail() {
            apiService.get('api/feedback/getbyid/' + $stateParams.id, null, function (result) {
                $scope.feedback = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        loadFeedbackDetail();
    }
})(angular.module('cardealer.feedbacks'));