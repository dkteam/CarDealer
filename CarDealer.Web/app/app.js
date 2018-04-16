/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('cardealer',
        ['cardealer.cars',
         'cardealer.car_categories',
         'cardealer.post_categories',
         'cardealer.posts',
         'cardealer.manufacture_years',
         'cardealer.fuels',
         'cardealer.styles',
         'cardealer.transmission_types',
         'cardealer.slides',
         'cardealer.menus',
         'cardealer.common'])
         .config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('base', {
                url: '',
                templateUrl: 'app/shared/views/baseView.html',
                abstract: true
            })
            .state('login', {
                url: "/login",
                templateUrl: "/app/components/login/loginView.html",
                controller: "loginController"
            })
            .state('home', {
                url: "/admin",
                parent: 'base',
                templateUrl: "/app/components/home/homeView.html",
                controller: "homeController"
            });

        $urlRouterProvider.otherwise('/login');
    }
})();