(function (app) {
    app.controller('menuAddController', menuAddController)

    menuAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function menuAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.menus = {
            Status: true,
            Target: false
        };

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.menus.URL = commonService.getSeoTitle($scope.menus.Name);
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.menus.Image = fileUrl;
                })
            }
            finder.popup();
        }

        function loadMenuGroup() {
            apiService.get('api/menugroup/getallNonPaging', null, function (result) {
                $scope.menuGroups = result.data;
            }, function () {
                console.log('cannot get list parent')
            });
        }

        function loadMenuParent() {
            apiService.get('api/menu/getallNonPaging', null, function (result) {
                $scope.menuParents = result.data;
            }, function () {
                console.log('cannot get list parent')
            });
        }

        $scope.AddMenu = AddMenu;
        function AddMenu() {
            apiService.post('api/menu/create', $scope.menus,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm vào cơ sở dữ liệu');
                    $state.go('menus');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công');
                });
        };

        loadMenuParent();
        loadMenuGroup();
    }
})(angular.module('cardealer.menus'));