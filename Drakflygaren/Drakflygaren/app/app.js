(function () {
    "use strict";
    var app = angular.module("app", ['ngRoute', 'commonService'])
        .config(function ($routeProvider) {
            $routeProvider.when("/", {
                controller: "userController",
                controllerAs: "model",
                templateUrl: "/app/views/userView.html"
            });
            //$routeProvider.when("/items/:id", {
            //    controller: "itemDetailsController",
            //    controllerAs: "model",
            //    templateUrl: "app/components/items/itemDetails/itemDetailsView.html"
            //})
            $routeProvider.otherwise({ redirectTo: "/" });
        });
    app.filter("dateFilter", function () {
        return function (item) {
            if (item != null) {
                return new Date(parseInt(item.substr(6)));
            }
            return "";
        };
    });
}());