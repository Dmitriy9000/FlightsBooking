(function() {

    var module = angular.module("MoneyModule", ["StorageModule"]);

    module.controller("MoneyController", [
        "Loading", "Error", "View", "Storage", "$http", function(Loading, Error, View, Storage, $http) {

            var self = this;

            self.priceToPay = Storage.GetPriceToPay;
            self.moneyReceived = 0;
            self.timer = null;
            self.inRecycler = 0;

            self.startMoneyDetecting = function () {
                PE.Devices.EnableMoneyReceiving();
                self.inRecycler = PE.Devices.RecyclerStoredCount();
                self.timer = setInterval(function () {
                    self.moneyReceived = PE.Devices.GetMoneyReceived();
                    console.log("Money detecting... ", self.moneyReceived);
                    if (self.moneyReceived >= Storage.GetPriceToPay()) {
                        self.buy();
                    };

                }, 1000);
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

            self.stopMoneyDetecting = function () {
                console.log("stop money detecting");
                clearInterval(self.timer);
            };

            self.buy = function () {

                self.stopMoneyDetecting();

                var pass = JSON.stringify(Storage.GetPassengers());
                var client = Storage.GetClient();
                console.log("passengers: ", pass);

                Loading.Show();

                $http.post("/Terminal/BuyTickets", {
                    passengers: pass,
                    r: Storage.GetSearchId(),
                    f: Storage.GetSelectedFare(),
                    v: Storage.GetSelectedVariants(),
                    email: client.Email,
                    phoneCode: client.PhoneCode,
                    phoneNumber: client.PhoneNumber,
                    amount: Storage.GetAmount(),
                    comission: Storage.GetComission()
                })
                    .success(function (resp, status, headers, config) {
                        if (resp.Success === true) {
                            Storage.SetOrderId(resp.Data);
                            console.log("OrderId: " + Storage.GetOrderId());
                            Loading.Hide();
                            self.change();
                            View.Tickets();
                        } else {
                            Error.Show("Ошибка при покупке билетов", resp.Error);
                        }
                    })
                    .error(function (data, status, headers, config) {
                        Loading.Hide();
                        alert("error");
                    });
            };

            self.reset = function () {
                console.log("reset");
                PE.Devices.DropNotesCount();
                Storage.SetPriceToPay(0);
                self.moneyReceived = 0;

                // flush payment session?
            };

            self.return = function () {
                console.log("return");
                self.stopMoneyDetecting();
                PE.Devices.ReturnPushedMoney();
            };

        }
    ]);

})();