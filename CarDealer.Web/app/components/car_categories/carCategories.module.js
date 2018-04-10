/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('cardealer.car_categories', ['cardealer.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('car_categories', {
            url: "/car_categories",
            templateUrl: "/app/components/car_categories/carCategoryListView.html",
            controller: "carCategoryListController"
        }).state('add_car_category', {
            url: "/add_car_category",
            templateUrl: "/app/components/car_categories/carCategoryAddView.html",
            controller: "carCategoryAddController"
        }).state('edit_car_category', {
            url: "/edit_car_category/:id",
            templateUrl: "/app/components/car_categories/carCategoryEditView.html",
            controller: "carCategoryEditController"
        });
    }
})();