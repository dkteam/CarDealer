(function (app) {
    app.filter('feedbackStatusFilter', function () {
        return function (input) {
            return input ? 'Đã xử lý' : 'Chưa xử lý';
        }
    });
})(angular.module('cardealer.common'));