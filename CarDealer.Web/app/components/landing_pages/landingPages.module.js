/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('cardealer.landing_pages', ['cardealer.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('landing_pages', {
            url: "/landing_pages",
            parent: 'base',
            templateUrl: "/app/components/landing_pages/landingPageListView.html",
            controller: "landingPageListController"
        }).state('landing_page_edit', {
            url: "/landing_page_edit/:id",
            parent: 'base',
            templateUrl: "/app/components/landing_pages/landingPageEditView.html",
            controller: "landingPageEditController"
        });
    }
})();