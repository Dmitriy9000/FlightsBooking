(function() {

    var module = angular.module("FaresModule", ["StorageModule"]);

    module.controller("FaresController", [
        "Loading", "Error", "View", "Storage", "$http", function(Loading, Error, View, Storage, $http) {

            var self = this;

            self.data = [];
            self.rules = "";

            self.loadFares = function() {

                var requestId = Storage.GetSearchId();

                $http.post("/Terminal/SearchResult", { r: requestId }).
                    success(function(data, status, headers, config) {

                        Loading.Hide();
                        self.data = data.Data;
                        
                        if (typeof (self.data) === 'undefined' || self.data.length === 0) {
                            // if no fares -> move back
                            View.SearchForm();
                            Error.Show("Билеты не найдены", "К сожалению по Вашему запросу не найдено рейсов, попробуйте другой запрос.");
                            return;
                        }

                        console.log("Loaded fares: " + self.data.length);
                        self.selectFirstVariants();
                       
                    }).
                    error(function(data, status, headers, config) {
                        Loading.Hide();
                        alert("error");
                    });

            };

            self.selectFirstVariants = function() {
                angular.forEach(self.data, function(fare) {
                    angular.forEach(fare.Additional.Dirs, function(dir) {
                        dir.Variants[0].selected = true;
                    });
                })
            }

            // Selecting clicked variant & unselect another in this Dir
            self.selectDir = function(did, vid) {
                console.log("Direction id: " + did + ",  Variant id: " + vid);

                angular.forEach(self.data, function(fare) {
                    angular.forEach(fare.Additional.Dirs, function(dir) {
                        if (dir.Id === did) {
                            angular.forEach(dir.Variants, function(variant) {
                                if (variant.Id === vid) {
                                    variant.selected = true;
                                } else {
                                    variant.selected = false;
                                }
                            });
                        }
                    });
                })
            };

            self.confirmVariants = function(f) {

                var selectedFare = f;
                Storage.SetSelectedFare(selectedFare);

                var selectedVariants = [];
                var variants = [];

                angular.forEach(self.data, function(fare) {

                    if (fare.Id === selectedFare) {

                        Storage.SetAmount(fare.Price);
                        Storage.SetComission(fare.Comission);
                        Storage.SetPriceToPay(fare.PriceWithComission)

                        angular.forEach(fare.Additional.Dirs, function(dir) {
                            angular.forEach(dir.Variants, function(variant) {
                                if (variant.selected) {
                                    variants.push(variant);
                                    selectedVariants.push(variant.Id);
                                }
                            });
                        });
                    }
                })

                var selectedVariantsString = selectedVariants.join(";");
                Storage.SetSelectedVariants(selectedVariantsString);
                Storage.SetSelectedVariantsData(variants);

                Loading.Show();
                $http.post("/Terminal/ConfirmVariants", { r: Storage.GetSearchId(), f: f, v: selectedVariantsString })
                    .success(function(result, status, headers, config) {

                        console.log("Confirming result: " + result);

                        Loading.Hide();

                        if (result.Data.Data.Confirmed === "True") {
                            View.Passengers();

                        } else {
                            Error.Show("Ошибка", "Невозможно забронировать места на этот рейс. Пожалуйста, попробуйте другой рейс.")
                        }

                    })
                    .error(function(data, status, headers, config) {
                        Loading.Hide();
                        alert("error");
                    });
            };

            self.showFareRules = function (f) {
                Loading.Show();
                $http.post("/Terminal/Rules", { f: f, r: Storage.GetSearchId() })
                    .success(function (data, status, headers, config) {
                        self.rules = data.Data;
                        $("#myModal").modal("show")
                        Loading.Hide();
                    })
                    .error(function (data, status, headers, config) {
                        Loading.Hide();
                        alert("error");
                    });
            }

        }
    ]);

})();