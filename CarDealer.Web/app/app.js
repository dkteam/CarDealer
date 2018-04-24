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
         'cardealer.pages',
         'cardealer.footers',
         'cardealer.menus',
         'cardealer.total_seats',
         'cardealer.support_onlines',
         'cardealer.landing_pages',
         'cardealer.application_groups',
         'cardealer.application_roles',
         'cardealer.application_users',
         'cardealer.common'])
         .config(config)
         .config(configAuthentication);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('base', {
                url: '',
                templateUrl: '/app/shared/views/baseView.html',
                //thêm vào để fix bug blank page
                //controller: 'StoreController as spaetis',
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

    function configAuthentication($httpProvider) {
        $httpProvider.interceptors.push(function ($q, $location) {
            return {
                request: function (config) {

                    return config;
                },
                requestError: function (rejection) {

                    return $q.reject(rejection);
                },
                response: function (response) {
                    if (response.status == "401") {
                        $location.path('/login');
                    }
                    //the same response/modified/or a new one need to be returned.
                    return response;
                },
                responseError: function (rejection) {

                    if (rejection.status == "401") {
                        $location.path('/login');
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }
})();