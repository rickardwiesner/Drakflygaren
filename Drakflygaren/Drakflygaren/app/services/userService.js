(function () {
    'use strict';
    angular.module('app').service('userService', function ($http) {
        this.getData = function (username) {
            return $http({
                url: "http://localhost:52647/users/getuserdetails",
                method: "GET",
                params: { username: username }
            });
        };

        this.saveProfile = function (user) {
            return $http({
                url: "http://localhost:52647/users/saveprofile",
                method: "POST",
                params: { 
                    firstName: user.firstName,
                    lastName: user.lastName,
                    imageUrl: user.imageUrl
                }
            });
        };
    });
})();