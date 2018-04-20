/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('cardealer.support_onlines', ['cardealer.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('supportonline_edit', {
            url: "/supportonline_edit",
            parent: 'base',
            templateUrl: "/app/components/support_onlines/supportOnlineEditView.html",
            controller: "supportOnlineEditController"
        });
    }
})();