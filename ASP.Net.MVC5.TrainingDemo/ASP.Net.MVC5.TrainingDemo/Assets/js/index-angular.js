require('angular');
require('angular-animate');
require('angular-mocks');
require('angular-sanitize');
require('angular-toastr');
require('angular-ui-router');



module.exports = function ($scope) {
    $scope.ModulesLoaded = "Angular Modules Loaded.";
    console.log("Loaded Angular Modules!");
};