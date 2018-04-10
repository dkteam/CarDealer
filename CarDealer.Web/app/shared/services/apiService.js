/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.service('apiService', apiService);

    apiService.$inject = ['$http', 'notificationService'];

    function apiService($http, notificationService) {
        return {
            get: get,
            post: post,
            put: put,
            del: del
        }

        function get(url, params, success, failure) {
            $http.get(url, params).then(function (result) {
                success(result);
            }, function failure(error) {
                failure(error);
            });
        }

        function post(url, data, success, failure) {
            $http.post(url, data).then(function (result) {
                success(result);
            }, function (error) {
                if (error.status === 401) {
                    notificationService.displayError('Yêu cầu đăng nhập.');
                } else if (failure != null) {
                    failure(error);
                }
            });
        }

        function put(url, data, success, failure) {
            $http.put(url, data).then(function (result) {
                success(result);
            }, function (error) {
                if (error.status === 401) {
                    notificationService.displayError('Yêu cầu đăng nhập.');
                } else if (failure != null) {
                    failure(error);
                }
            });
        }

        function del(url, data, success, failure) {
            $http.delete(url, data).then(function (result) {
                success(result);
            }, function (error) {
                if (error.status === 401) {
                    notificationService.displayError('Yêu cầu đăng nhập');
                }
                else if (failure != null) {
                    failure(error);
                }

            });
        }
    }
})(angular.module('cardealer.common'));