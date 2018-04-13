(function (app) {
    app.controller('styleAddController', styleAddController)

    styleAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function styleAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.styles = {
            CreatedDate: new Date(),
            Status: true
        };

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.styles.Alias = commonService.getSeoTitle($scope.styles.Name);
        }

        $scope.AddStyle = AddStyle;

        function AddStyle() {
            apiService.post('api/style/create', $scope.styles,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm vào cơ sở dữ liệu');
                    $state.go('styles');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công');
                });
        };
    }
})(angular.module('cardealer.styles'));