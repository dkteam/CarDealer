(function (app) {
    app.controller('totalSeatEditController', totalSeatEditController)

    totalSeatEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function totalSeatEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.totalSeats = {};

        $scope.UpdateTotalSeat = UpdateTotalSeat;
        function UpdateTotalSeat() {
            apiService.put('api/totalseat/update', $scope.totalSeats,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công.');
                    $state.go('total_seats');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công');
                });
        };

        function loadTotalSeatDetail() {
            apiService.get('api/totalseat/getbyid/' + $stateParams.id, null, function (result) {
                $scope.totalSeats = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        loadTotalSeatDetail();
    }
})(angular.module('cardealer.total_seats'));