(function () {

    //const
    var APP_TITLE = "Weather Application";
    var API_PARTIAL = "/api/values/";
    var API_LOCATION_PARTIAL = "?location=";
    var API_SPEED_PARTIAL = "&speed=";
    var API_TEMP_PATIAL = "&temp=";

    //init module
    var app = angular.module('weatherApp', []);

    //init controller
    app.controller('weatherController', function ($scope, $http, $sce) {
        $scope.Title = APP_TITLE;
        $scope.HasResults = false;
        $scope.HasError = false;

        //get data
        $scope.GetWeather = function () {
            if ($scope.locationInput !== null && $scope.locationInput.length > 0) {
                var url = API_PARTIAL + API_LOCATION_PARTIAL + $scope.locationInput;
                url = url + API_SPEED_PARTIAL + $scope.selectedSpeed;
                url = url + API_TEMP_PATIAL + $scope.selectedTemp;
                $http.get(url).then(function (response) {
                    if (response.data.success === false) {
                        $scope.HasError = true; return;
                    }
                    $scope.WindSpeed = response.data.windSpeed;
                    $scope.Temperature = response.data.temperature;
                    $scope.HasResults = true;
                });
            }
        }

        //temp select options
        $scope.temps = [{
            value: 1,
            label: $sce.trustAsHtml('&deg;C')
        }, {
            value: 2,
            label: $sce.trustAsHtml('&deg;F')
        }];

        //speed select options
        $scope.speeds = [{
            value: 1,
            label: 'MPH'
        }, {
            value: 2,
            label: 'KPH'
        }];
    });
})();