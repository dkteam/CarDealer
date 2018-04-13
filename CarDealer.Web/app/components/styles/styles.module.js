/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('cardealer.styles', ['cardealer.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('styles', {
            url: "/styles",
            templateUrl: "/app/components/styles/styleListView.html",
            controller: "styleListController"
        }).state('style_add', {
            url: "/style_add",
            templateUrl: "/app/components/styles/styleAddView.html",
            controller: "styleAddController"
        }).state('style_edit', {
            url: "/style_edit/:id",
            templateUrl: "/app/components/styles/styleEditView.html",
            controller: "styleEditController"
        });
    }
})();