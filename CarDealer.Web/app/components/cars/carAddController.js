(function (app) {
    app.controller('carAddController', carAddController)

    carAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function carAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.carDetail = {
            CreatedDate: new Date(),
            ViewCount: 0,
            Price: 0,
            CarStatus: true,
            Status: true
        };

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.carDetail.Alias = commonService.getSeoTitle($scope.carDetail.Name);
        }

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '300px',
            $invalid: false

        }

        $scope.AddCar = AddCar;
        function AddCar() {
            $scope.carDetail.MoreImages = JSON.stringify($scope.moreImages); //chuyển sang dạng chuỗi truyền vào database

            apiService.post('api/car/create', $scope.carDetail,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm vào cơ sở dữ liệu');
                    $state.go('cars');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công');
                });
        };

        function loadCategory() {
            apiService.get('api/carcategory/getallparents', null, function (result) {
                $scope.categories = result.data;
            }, function () {
                console.log('cannot get list parent')
            });
        }

        function loadManufactureYear() {
            apiService.get('api/manufactureyear/getallnonpaging', null, function (result) {
                $scope.manufactureyears = result.data;
            }, function () {
                console.log('cannot get list parent')
            });
        }

        function loadStyle() {
            apiService.get('api/style/getallnonpaging', null, function (result) {
                $scope.styles = result.data;
            }, function () {
                console.log('cannot get list parent')
            });
        }

        function loadTransmissionType() {
            apiService.get('api/transmissiontype/getallnonpaging', null, function (result) {
                $scope.transmissiontypes = result.data;
            }, function () {
                console.log('cannot get list parent')
            });
        }

        function loadFuel() {
            apiService.get('api/fuel/getallnonpaging', null, function (result) {
                $scope.fuels = result.data;
            }, function () {
                console.log('cannot get list parent')
            });
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.carDetail.Image = fileUrl;
                })
            }
            finder.popup();
        }

        $scope.moreImages = [];
        $scope.ChooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                });
            }
            finder.popup();
        }

        $scope.removeImage = function (index) {
            $scope.moreImages.splice(index, 1);
        }

        //displayCKEditor();
        loadCategory();
        loadManufactureYear();
        loadStyle();
        loadTransmissionType();
        loadFuel();
    }
})(angular.module('cardealer.cars'));