/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('cardealer.transmission_types', ['cardealer.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('transmission_types', {
            url: "/transmission_types",
            parent: 'base',
            templateUrl: "/app/components/transmission_types/transmissionTypeListView.html",
            controller: "transmissionTypeListController"
        }).state('transmission_type_add', {
            url: "/transmission_type_add",
            parent: 'base',
            templateUrl: "/app/components/transmission_types/transmissionTypeAddView.html",
            controller: "transmissionTypeAddController"
        }).state('transmission_type_edit', {
            url: "/transmission_type_edit/:id",
            parent: 'base',
            templateUrl: "/app/components/transmission_types/transmissionTypeEditView.html",
            controller: "transmissionTypeEditController"
        });
    }
})();