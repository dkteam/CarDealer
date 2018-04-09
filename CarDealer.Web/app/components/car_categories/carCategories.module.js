/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('cardealer.car_categories', ['cardealer.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('car_categories', {
            url: "/car_categories",
            templateUrl: "/app/components/car_categories/carCategoryListView.html",
            controller: "carCategoryListController"
        });
    }
})();