/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('cardealer',
        ['cardealer.cars',
         'cardealer.car_categories',
         'cardealer.post_categories',
         'cardealer.posts',
         'cardealer.manufacture_years',
         'cardealer.common'])
         .config(config);

    config.$inject=['$stateProvider','$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('home', {
            url: "/admin",
            templateUrl: "/app/components/home/homeView.html",
            controller:"homeController"
        });

        $urlRouterProvider.otherwise('/admin');
    }
})();