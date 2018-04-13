/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('cardealer.slides', ['cardealer.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('slides', {
            url: "/slides",
            templateUrl: "/app/components/slides/slideListView.html",
            controller: "slideListController"
        }).state('slide_add', {
            url: "/slide_add",
            templateUrl: "/app/components/slides/slideAddView.html",
            controller: "slideAddController"
        }).state('slide_edit', {
            url: "/slide_edit/:id",
            templateUrl: "/app/components/slides/slideEditView.html",
            controller: "slideEditController"
        });
    }
})();