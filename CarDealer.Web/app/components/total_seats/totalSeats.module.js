/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('cardealer.total_seats', ['cardealer.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('total_seats', {
            url: "/total_seats",
            parent: 'base',
            templateUrl: "/app/components/total_seats/totalSeatListView.html",
            controller: "totalSeatListController"
        }).state('total_seat_add', {
            url: "/total_seat_add",
            parent: 'base',
            templateUrl: "/app/components/total_seats/totalSeatAddView.html",
            controller: "totalSeatAddController"
        }).state('total_seat_edit', {
            url: "/total_seat_edit/:id",
            parent: 'base',
            templateUrl: "/app/components/total_seats/totalSeatEditView.html",
            controller: "totalSeatEditController"
        });
    }
})();