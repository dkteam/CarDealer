(function (app) {
    app.controller('carEditController', carEditController)

    carEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function carEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.moreImages = [];
        $scope.carDetail = {
            UpdatedDate: new Date(),
        };        

        $scope.UpdateCar = UpdateCar;
        function UpdateCar() {
            $scope.carDetail.MoreImages = JSON.stringify($scope.moreImages); //chuyển sang dạng chuỗi truyền vào database

            apiService.put('api/car/update', $scope.carDetail,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công.');
                    $state.go('cars');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công');
                });
        };

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '300px',
            resize_enabled: false

        }

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.carDetail.Alias = commonService.getSeoTitle($scope.carDetail.Name);
        }

        function loadCarDetail() {
            apiService.get('api/car/getbyid/' + $stateParams.id, null, function (result) {
                $scope.carDetail = result.data;
                $scope.moreImages = JSON.parse($scope.carDetail.MoreImages); //parse ra dạng mảng
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

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



        loadCategory();
        loadCarDetail();
        loadManufactureYear();
        loadStyle();
        loadTransmissionType();
        loadFuel();



    }
})(angular.module('cardealer.cars'));