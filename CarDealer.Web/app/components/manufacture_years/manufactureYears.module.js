/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('cardealer.manufacture_years', ['cardealer.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('manufacture_years', {
            url: "/manufacture_years",
            templateUrl: "/app/components/manufacture_years/manufactureYearListView.html",
            controller: "manufactureYearListController"
        }).state('manufacture_year_add', {
            url: "/manufacture_year_add",
            templateUrl: "/app/components/manufacture_years/manufactureYearAddView.html",
            controller: "manufactureYearAddController"
        }).state('manufacture_year_edit', {
            url: "/manufacture_year_edit/:id",
            templateUrl: "/app/components/manufacture_years/manufactureYearEditView.html",
            controller: "manufactureYearEditController"
        });
    }
})();