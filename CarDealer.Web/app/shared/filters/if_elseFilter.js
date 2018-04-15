
(function (app) {
    app.filter('iif', function () {
        return function (input, trueValue, falseValue) {
            return input ? trueValue : falseValue;
        };
    });
})(angular.module('cardealer.common'));