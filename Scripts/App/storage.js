(function() {

    var module = angular.module("StorageModule", []);

    module.factory("Storage", function () {

        var self = this;
        self.totalTravelers = 0; // adult + childs
        self.searchId = "";
        self.selectedFare = "";
        self.selectedVariants = ""; // 1;43
        self.selectedVariantsData = ""; // to bind on tickets view
        self.amount = ""; // price of tickets
        self.comission = ""; // our comission
        self.priceToPay = ""; // comission + price
        self.passengers = []; // list of passengers
        self.client = {}; // client info
        self.orderId = ""; // order id
        self.supportPhone = "+7 968 451 2404";

        return {
            GetTotalTravelers: function () {
                return self.totalTravelers;
            },
            SetTotalTravelers: function (id) {
                self.totalTravelers = id;
            },

            GetSearchId: function() {
                return self.searchId;
            },
            SetSearchId: function(id) {
                self.searchId = id;
            },

            GetSelectedFare: function() {
                return self.selectedFare;
            },
            SetSelectedFare: function(value) {
                self.selectedFare = value;
            },

            GetSelectedVariants: function() {
                return self.selectedVariants;
            },
            SetSelectedVariants: function(value) {
                self.selectedVariants = value;
            },

            GetSelectedVariantsData: function() {
                return self.selectedVariantsData;
            },
            SetSelectedVariantsData: function(value) {
                self.selectedVariantsData = value;
            },

            GetAmount: function() {
                return self.amount;
            },
            SetAmount: function(value) {
                self.amount = value;
            },

            GetComission: function() {
                return self.comission;
            },
            SetComission: function(value) {
                self.comission = value;
            },

            GetPriceToPay: function() {
                return self.priceToPay;
            },
            SetPriceToPay: function(value) {
                self.priceToPay = value;
            },

            GetPassengers: function() {
                return self.passengers;
            },
            SetPassengers: function(value) {
                self.passengers = value;
            },

            GetClient: function() {
                return self.client;
            },
            SetClient: function(value) {
                self.client = value;
            },

            GetOrderId: function() {
                return self.orderId;
            },
            SetOrderId: function(value) {
                self.orderId = value;
            },

            GetSupportPhone: function() {
                return self.supportPhone;
            },

            ResetAll: function() {
                self.totalTravelers = 1;
                self.searchId = "";
                self.selectedFare = "";
                self.selectedVariants = "";
                self.selectedVariantsData = "";
                self.amount = "";
                self.comission = "";
                self.priceToPay = "";
                self.passengers = [];
                self.client = {};
                self.orderId = "";
            }

        }

    });

})();