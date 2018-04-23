(function (app) {
    app.controller('postAddController', postAddController)

    postAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function postAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.postDetail = {
            CreatedDate: new Date(),
            UpdatedDate: new Date(),
            ViewCount: 0,
            Status: true
        };

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.postDetail.Alias = commonService.getSeoTitle($scope.postDetail.Name);
        }

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '300px',
            $invalid: false
        }

        //$scope.displayCKEditor = displayCKEditor;
        //function displayCKEditor() {
        //    var editor = CKEDITOR.replace('txtDetail', {
        //        customconfig: '/Assets/admin/libs/ckeditor/article.js'

        //    })
        //}

        $scope.AddPost = AddPost;
        function AddPost() {
            apiService.post('api/post/create', $scope.postDetail,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm vào cơ sở dữ liệu');
                    $state.go('posts');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công');
                });
        };

        function loadCategory() {
            apiService.get('api/postcategory/getallparents', null, function (result) {
                $scope.categories = result.data;
            }, function () {
                console.log('cannot get list parent')
            });
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.postDetail.Image = fileUrl;
                })
            }
            finder.popup();
        }

        //displayCKEditor();
        loadCategory();
    }
})(angular.module('cardealer.posts'));