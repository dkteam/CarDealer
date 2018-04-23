(function (app) {
    app.controller('pageAddController', pageAddController)

    pageAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function pageAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.pages = {
            Status: true,
            CreatedDate: new Date()
        };

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.pages.Alias = commonService.getSeoTitle($scope.pages.Name);
        }

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '300px',
            $invalid: false
        }

        $scope.AddPage = AddPage;

        function AddPage() {
            apiService.post('api/page/create', $scope.pages,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm vào cơ sở dữ liệu');
                    $state.go('pages');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công');
                });
        };
    }
})(angular.module('cardealer.pages'));