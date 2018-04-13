﻿/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('cardealer.fuels', ['cardealer.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('fuels', {
            url: "/fuels",
            templateUrl: "/app/components/fuels/fuelListView.html",
            controller: "fuelListController"
        }).state('fuel_add', {
            url: "/fuel_add",
            templateUrl: "/app/components/fuels/fuelAddView.html",
            controller: "fuelAddController"
        }).state('fuel_edit', {
            url: "/fuel_edit/:id",
            templateUrl: "/app/components/fuels/fuelEditView.html",
            controller: "fuelEditController"
        });
    }
})();