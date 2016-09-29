(function () {
    'use strict';
    angular.module('app')
    .controller('userController', ['userService', '$routeParams', '$scope', userController]);

    function userController(userService, $routeParams, $scope) {

        var model = this;
        var backup = "";

        userService.getData($scope.username).then(function (reponse) {
            model.user = reponse.data;
            console.log(model.user);
            model.date = new Date();
        });

        model.startEditing = function () {
            model.editing = true;
            backup = angular.copy(model.user);
        };

        model.cancelEditing = function () {
            model.user = backup;
            model.editing = false;
        };

        model.saveProfile = function () {
            userService.saveProfile(model.user).then(function () {
                model.successMessage = "Din profil är nu uppdaterad :)";
                model.editing = false;
            }, function (error) {
                model.errorMessage = "Misslyckades att uppdatera din profil :(";
            });
        };
    }
}());
