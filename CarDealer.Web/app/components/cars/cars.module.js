/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('cardealer.cars', ['cardealer.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('cars', {
            url: "/cars",
            templateUrl: "/app/components/cars/carListView.html",
            controller: "carListController"
        }).state('car_add', {
            url: "/car_add",
            templateUrl: "/app/components/cars/carAddView.html",
            controller: "carAddController"
        });;
    }
})();