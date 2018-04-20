(function (app) {
    app.filter('targetFilter', function () {
        return function (input) {
            return input ? 'Mở tab mới' : '';
        }
    });
})(angular.module('cardealer.common'));