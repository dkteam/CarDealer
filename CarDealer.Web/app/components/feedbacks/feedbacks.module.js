/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('cardealer.feedbacks', ['cardealer.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('feedbacks', {
            url: "/feedbacks",
            parent: 'base',
            templateUrl: "/app/components/feedbacks/feedbackListView.html",
            controller: "feedbackListController"
        }).state('feedback_edit', {
            url: "/feedback_edit/:id",
            parent: 'base',
            templateUrl: "/app/components/feedbacks/feedbackEditView.html",
            controller: "feedbackEditController"
        });
    }
})();