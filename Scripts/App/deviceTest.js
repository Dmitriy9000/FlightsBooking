(function() {

    var myApp = angular.module("app", ["DeviceTestModule"], function () {});

    var module = angular.module("DeviceTestModule", []);
    module.controller("DeviceTestController", ["$http", "$scope", function ($http, $scope) {

            var self = this;

            self.view = "input"; // input money tickets
            self.price = 3000;
            self.moneyReceived = 0;
            self.recyclerStored = "";
            self.timer = null;

            self.toMoney = function() {
                self.view = "money";
                console.log("to money");

                PE.Devices.EnableMoneyReceiving();

                self.timer = setInterval(function () {
                    self.moneyReceived = PE.Devices.GetMoneyReceived();
                    console.log("Money detecting... ", self.moneyReceived);
                    if (self.moneyReceived >= self.price) {
                        self.complete();
                    };

                }, 1000);

            };

            self.stopMoneyDetecting = function () {
                console.log("stop money detecting");
                clearInterval(self.timer);
            };

            self.complete = function() {

                $scope.$apply(function () {
                    self.view = "tickets";
                });
                
                console.log("complete, view=" + self.view);
                self.change();

                // stop NV11 only after money received - make status / 
                self.stopMoneyDetecting();
            };

            self.return = function () {
                console.log("return");
                self.stopMoneyDetecting();
                PE.Devices.ReturnPushedMoney();
                //PE.Devices.DropNotesCount();
            };

            self.reset = function() {
                console.log("reset");
                PE.Devices.DropNotesCount();
                self.view = "input";
                self.price = 0;
                self.moneyReceived = 0;
            };

            self.change = function () {
                console.log("change");
                if (self.moneyReceived > self.price) {
                    var diff = self.moneyReceived - self.price;
                    var notesCount = diff / 1000;
                    console.log("To change: " + notesCount);
                    PE.Devices.Change(notesCount);
                }
                else if (self.moneyReceived === self.price) {
                    PE.Devices.DisableMoneyReceiving();
                }
            };

            self.refresh = function() {
                self.recyclerStored = PE.Devices.RecyclerStoredCount();
                self.paymentSession = PE.Devices.GetMoneyReceived();
            };

            self.flushPaymentSession = function() {
                PE.Devices.DropNotesCount();
                self.refresh();
            }

            self.printCheck = function() {
                PE.Devices.PrintCheck(1000, 150);
            }

        self.printUrl = function() {
            PE.Devices.PrintUrl("ya.ru");
        }

        }
    ]);

})();