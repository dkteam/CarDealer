(function (app) {
    app.filter('usedFilter', function () {
        return function (input) {
            return input ? 'Xe mới' : 'Đã sử dụng';
        }
    });
})(angular.module('cardealer.common'));