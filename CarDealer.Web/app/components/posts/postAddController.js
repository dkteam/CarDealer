(function (app) {
    app.controller('postAddController', postAddController)

    postAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function postAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.postDisplay = {
            CreatedDate: new Date(),
            Status: true
        };

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.postDisplay.Alias = commonService.getSeoTitle($scope.postDisplay.Name);
        }

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '300px'

        }
        $scope.AddPost = AddPost;

        function AddPost() {
            apiService.post('api/post/create', $scope.postDisplay,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm vào cơ sở dữ liệu');
                    $state.go('posts');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công');
                });
        };

        function loadParentCategory() {
            apiService.get('api/post/getcategories', null, function (result) {
                $scope.categories = result.data;
            }, function () {
                console.log('cannot get list parent')
            });
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.postDisplay.Image = fileUrl;
                })
            }
            finder.popup();
        }


        loadParentCategory();
    }
})(angular.module('cardealer.posts'));