(function (app) {
    app.controller('menuEditController', menuEditController)

    menuEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function menuEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.menus = {
            Status: true
        };

        $scope.UpdateMenu = UpdateMenu;
        function UpdateMenu() {
            apiService.put('api/menu/update', $scope.menus,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công.');
                    $state.go('menus');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công');
                });
        };

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.menus.Image = fileUrl;
                })
            }
            finder.popup();
        }

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.menus.Alias = commonService.getSeoTitle($scope.menus.Name);
        }

        function loadMenuDetail() {
            apiService.get('api/menu/getbyid/' + $stateParams.id, null, function (result) {
                $scope.menus = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function loadMenuGroup() {
            apiService.get('api/menugroup/getallNonPaging', null, function (result) {
                $scope.menuGroups = result.data;
            }, function () {
                console.log('cannot get list parent')
            });
        }

        loadMenuGroup();
        loadMenuDetail();
    }
})(angular.module('cardealer.menus'));