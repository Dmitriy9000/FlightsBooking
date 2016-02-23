(function () {

    var app = angular.module("ViewsModule", []);

    app.factory("Loading", function() {

        var loading = false;

        return {
            Show: function() {
                loading = true;
            },
            Hide: function() {
                loading = false;
            },
            IsLoading: function() {
                return loading;
            }
        }

    });

    app.factory("Error", ["Loading", function(Loading) {

        var self = this;
        self.errorTitle = "";
        self.errorText = "";

        return {
            Show: function(title, text) {
                self.errorTitle = title;
                self.errorText = text;
                Loading.Hide();
                $("#errorModal").modal("show");
            },

            GetLastErrorTitle: function() {
                return self.errorTitle;
            },

            GetLastErrorText: function() {
                return self.errorText;
            }
        }
    }]);

    app.factory("View", ["Storage", function(Storage) {

        var self = this;
        self.active = ""; // search-form | fares | passengers | money | tickets 

        return {
            SearchForm: function() {
                self.active = "search-form";

            },

            Fares: function() {
                self.active = "fares";

                angular.element(document.getElementById("fares-block")).controller().loadFares();

                new FTScroller(document.getElementById("fares-block"), {
                    scrollbars: false,
                    scrollingX: false
                });
            },

            Passengers: function () {

                var fromMoney = self.active === "money";
                self.active = "passengers";
                console.log("From money view: " + fromMoney)

                if (fromMoney) {

                    // passengers are the same

                } else {
                    // first time fill passengers

                    var total = Storage.GetTotalTravelers();
                    console.log("Total passengers: " + total);
                    var passengers = [];
                    for (var i = 0; i < total; i++) {
                        passengers.push({
                            FirstName: "",
                            LastName: "",
                            MiddleName: "",
                            Sex: "M",
                            Dob: "",
                            Country: "RU",
                            DocNumber: "",
                            DocExpDate: ""
                        });
                    }

                    angular.element(document.getElementById("passengers")).controller().info = passengers;
                    
                }


                //var passengersCount = Storage.GetTotalTravelers();
                //self.data = new Array(passengersCount);
                //for (var j = 0; j < passengersCount; j++) {
                //    self.data.push({});
                //}

                $(".doc-number").mask("0000 000000");
                $("#client-phone").mask("(000) 000-00-00");

                setInterval(function () {
                    new FTScroller(document.getElementById("passengers"), {
                        scrollbars: false,
                        scrollingX: false
                    });
                }, 1000);
            },

            Money: function() {
                self.active = "money";
                angular.element(document.getElementById("money-block")).controller().startMoneyDetecting();
            },

            Tickets: function() {
                self.active = "tickets";
                angular.element(document.getElementById("tickets-block")).controller().show();
                new FTScroller(document.getElementById("tickets-block"), {
                    scrollbars: false,
                    scrollingX: false
                });
            },

            GetActive: function() {
                return self.active;
            }

        }

    }]);

    app.controller("ViewsController", ["Loading", "Error", "View", function(Loading, Error, View) {

        var self = this;
        self.loading = Loading.IsLoading;
        self.activeView = View.GetActive;

        self.errorTitle = Error.GetLastErrorTitle;
        self.errorText = Error.GetLastErrorText;

        self.activate = function() {
            Loading.Show();
        }

        self.searchForm = function() {
            View.SearchForm();
        }

        self.fares = function() {
            View.Fares();
        }

        self.passengers = function() {
            View.Passengers();
        }

        self.money = function() {
            View.Money();
        }

        self.tickets = function() {
            View.Tickets();
        }

    }]);

})();