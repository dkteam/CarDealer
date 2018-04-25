(function (app) {
    app.controller('feedbackEditController', feedbackEditController)

    feedbackEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function feedbackEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.feedbacks = {
            Status: false
        };

        $scope.UpdateFeedback = UpdateFeedback;
        function UpdateFeedback() {
            apiService.put('api/feedback/update', $scope.feedbacks,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công.');
                    $state.go('feedbacks');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công');
                });
        };

        $scope.ChangeStatus = ChangeStatus;
        function ChangeStatus() {
            apiService.put('api/feedback/changestatus', $stateParams.id,
                function (result) {
                    $scope.feedbacks = result.data;
                    //notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công.');
                    //$state.go('feedbacks');
                }, function (error) {
                    //notificationService.displayError('Cập nhật không thành công');
                    notificationService.displayError(error.data);
                });
        };
 
        function loadFeedbackDetail() {
            apiService.get('api/feedback/getbyid/' + $stateParams.id, null, function (result) {
                $scope.feedbacks = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        loadFeedbackDetail();
    }
})(angular.module('cardealer.feedbacks'));