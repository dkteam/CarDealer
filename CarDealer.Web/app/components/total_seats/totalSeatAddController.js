(function (app) {
    app.controller('totalSeatAddController', totalSeatAddController)

    totalSeatAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function totalSeatAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.totalSeats = {};

        $scope.AddTotalSeat = AddTotalSeat;

        function AddTotalSeat() {
            apiService.post('api/totalseat/create', $scope.totalSeats,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm vào cơ sở dữ liệu');
                    $state.go('total_seats');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công');
                });
        };
    }
})(angular.module('cardealer.total_seats'));