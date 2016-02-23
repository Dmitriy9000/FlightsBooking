var myApp = angular.module("app",
    ["ngSanitize", "ui.bootstrap", "ViewsModule",
        "SearchModule", "StorageModule", "FaresModule",
    "PassengersModule", "MoneyModule", "TicketsModule"], function ($httpProvider) {
    // Use x-www-form-urlencoded Content-Type
    $httpProvider.defaults.headers.post["Content-Type"] = "application/x-www-form-urlencoded;charset=utf-8";

    /**
     * The workhorse; converts an object to x-www-form-urlencoded serialization.
     * @param {Object} obj
     * @return {String}
     */
    var param = function (obj) {
        var query = "", name, value, fullSubName, subName, subValue, innerObj, i;

        for (name in obj) {
            value = obj[name];

            if (value instanceof Array) {
                for (i = 0; i < value.length; ++i) {
                    subValue = value[i];
                    fullSubName = name + "[" + i + "]";
                    innerObj = {};
                    innerObj[fullSubName] = subValue;
                    query += param(innerObj) + "&";
                }
            }
            else if (value instanceof Object) {
                for (subName in value) {
                    subValue = value[subName];
                    fullSubName = name + "[" + subName + "]";
                    innerObj = {};
                    innerObj[fullSubName] = subValue;
                    query += param(innerObj) + "&";
                }
            }
            else if (value !== undefined && value !== null)
                query += encodeURIComponent(name) + "=" + encodeURIComponent(value) + "&";
        }

        return query.length ? query.substr(0, query.length - 1) : query;
    };

    // Override $http service's default transformRequest
    $httpProvider.defaults.transformRequest = [function (data) {
        return angular.isObject(data) && String(data) !== "[object File]" ? param(data) : data;
    }];
});

myApp.controller("MainController", [
    "$scope", "$http", "Loading", "Error", "View", "Storage", function($scope, $http, Loading, Error, View, Storage) {

        // Pass from services
        $scope.loading = Loading.IsLoading;

        // debug
        //$scope.orderInfo = { "CreatedDate": "2015-03-09T12:21:27.48+03:00", "Status": "ManualMode", "Error": null, "OrderNumber": "806802265", "Amount": "8696", "Currency": "RUB", "PresentScore": null, "ScoreForOrder": "0", "PaymentMethod": "Clearing", "TimeLimitUTCString": null, "Tickets": [{ "TicketId": "078c3de9-fe95-42fd-bc97-a1923295f931", "FName": "DMITRIY", "LName": "KUDINOV", "TicketNumber": null, "Passport": "1112321888", "Gender": "M", "Nationality": "RU", "Number": "1", "BirthDate": "1987-10-10", "FrequentlyFlyerCard": null, "OccupiesSeparateSeat": "True", "Receipt": { "Receipts": [{ "AgencyName": "Agency", "AgencyIATANum": "00000000", "PlatingAircompany": "UNKNOWN", "Passenger": "DMITRIY KUDINOV", "IdentifierNumber": "806802265", "TicketNumber": "", "IssueDate": "0001-01-01T00:00:00", "Endorsment": "ENDORSMENT TEXT", "BaseAmount": "RUB 999", "Taxes": "RUB 3518", "TotalAmount": "RUB 4347,54", "TotalAmountWithMargins": "4470.54", "MarginsAmount": "-47.46", "FareCalc": "", "Currency": "RUB", "PNR": "UKWN", "VendorPNR": "UNKNOWN", "PrintDate": "2015-03-09T12:21:33.5705102+03:00", "Trip": { "Airline": "Aeroflot Russian Airlines", "FlightNumber": "SU6122", "Class": "R(Economy)", "FareBasis": null, "Bag": null, "ST": null, "Start": { "AirpCode": "DME", "AirpName": "Domodedovo", "City": "Moscow", "Terminal": null }, "End": { "AirpCode": "LED", "AirpName": "Pulkovo", "City": "St Petersburg", "Terminal": "1" }, "StartDate": "2015-03-23T09:20:00", "EndDate": "2015-03-23T10:50:00", "NVB": "0001-01-01T00:00:00", "NVA": "0001-01-01T00:00:00" } }] } }, { "TicketId": "ba2c09df-7bdf-4443-81b2-31c9823480fd", "FName": "PETR", "LName": "KUDINOV", "TicketNumber": null, "Passport": "1112321122", "Gender": "M", "Nationality": "RU", "Number": "2", "BirthDate": "1987-10-10", "FrequentlyFlyerCard": null, "OccupiesSeparateSeat": "True", "Receipt": { "Receipts": [{ "AgencyName": "Agency", "AgencyIATANum": "00000000", "PlatingAircompany": "UNKNOWN", "Passenger": "PETR KUDINOV", "IdentifierNumber": "806802265", "TicketNumber": "", "IssueDate": "0001-01-01T00:00:00", "Endorsment": "ENDORSMENT TEXT", "BaseAmount": "RUB 999", "Taxes": "RUB 3518", "TotalAmount": "RUB 4347,54", "TotalAmountWithMargins": "4470.54", "MarginsAmount": "-47.46", "FareCalc": "", "Currency": "RUB", "PNR": "UKWN", "VendorPNR": "UNKNOWN", "PrintDate": "2015-03-09T12:21:33.8158019+03:00", "Trip": { "Airline": "Aeroflot Russian Airlines", "FlightNumber": "SU6122", "Class": "R(Economy)", "FareBasis": null, "Bag": null, "ST": null, "Start": { "AirpCode": "DME", "AirpName": "Domodedovo", "City": "Moscow", "Terminal": null }, "End": { "AirpCode": "LED", "AirpName": "Pulkovo", "City": "St Petersburg", "Terminal": "1" }, "StartDate": "2015-03-23T09:20:00", "EndDate": "2015-03-23T10:50:00", "NVB": "0001-01-01T00:00:00", "NVA": "0001-01-01T00:00:00" } }] } }], "Trips": [{ "TripId": "75e737f2-1e0f-480e-af8e-026254b1da18", "TripNumber": 0, "StartTerminal": null, "EndTerminal": "1", "Carrier": "Rossiya- Russian Airlines", "FlightNumber": "6122", "Conx": false, "Direction": "0", "StartTime": "2015-03-23T09:20:00", "EndTime": "2015-03-23T10:50:00", "TimeOnEarth": null, "Arrive": "0", "Departure": "0", "JourneyTime": "01:30", "FlightTime": "01:30", "StartCity": { "Name": "Москва", "Code": "MOW" }, "EndCity": { "Name": "Санкт-Петербург", "Code": "LED" }, "AirCompany": { "Name": "Aeroflot Russian Airlines", "Code": "SU" }, "StartAirp": { "Name": "Домодедово", "Code": "DME" }, "EndAirp": { "Name": "Пулково", "Code": "LED" }, "Airplane": { "Name": "Airbus A319", "Code": "319" }, "Class": { "ClassName": "Economy", "Class": "R", "ServiceClass": "E" } }, { "TripId": "ca41b834-93b4-468b-82b1-1697bcf5e8cd", "TripNumber": 1, "StartTerminal": "1", "EndTerminal": null, "Carrier": "Rossiya- Russian Airlines", "FlightNumber": "6121", "Conx": false, "Direction": "1", "StartTime": "2015-03-27T06:55:00", "EndTime": "2015-03-27T08:25:00", "TimeOnEarth": null, "Arrive": "0", "Departure": "0", "JourneyTime": "01:30", "FlightTime": "01:30", "StartCity": { "Name": "Санкт-Петербург", "Code": "LED" }, "EndCity": { "Name": "Москва", "Code": "MOW" }, "AirCompany": { "Name": "Aeroflot Russian Airlines", "Code": "SU" }, "StartAirp": { "Name": "Пулково", "Code": "LED" }, "EndAirp": { "Name": "Домодедово", "Code": "DME" }, "Airplane": { "Name": "Airbus A319", "Code": "319" }, "Class": { "ClassName": "Economy", "Class": "R", "ServiceClass": "E" } }] };

        View.SearchForm();
    }
]);