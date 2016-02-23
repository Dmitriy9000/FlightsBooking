(function() {

    var module = angular.module("TicketsModule", ["StorageModule"]);

    module.controller("TicketsController", [
        "Loading", "Error", "View", "Storage", "$http", function(Loading, Error, View, Storage, $http) {

            var self = this;
            self.supportPhone = Storage.GetSupportPhone;
            self.orderInfo = {};
            //self.receipts = {};

            // Вывод билетов
            self.show = function () {
                Loading.Show();
                $http.post("/Terminal/ShowTickets", {
                    orderId: Storage.GetOrderId(),
                    comission: Storage.GetComission()
                })
                    .success(function (resp, status, headers, config) {

                        console.log(resp.Data);
                        self.orderInfo = resp.Data;
                        //self.receipts = resp.Data;
                        Loading.Hide();

                        if (resp.Success === true) {

                        } else {
                            Error.Show("Ошибка при получении билетов", resp.Error);
                        }

                    })
                    .error(function (data, status, headers, config) {
                        Loading.Hide();
                        alert("Server error");
                    });
            }

            self.reset = function () {
                Storage.ResetAll();
                $("#from").val("");
                $("#to").val("");
                View.SearchForm();
            };

        }
    ]);

})();