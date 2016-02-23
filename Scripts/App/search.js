(function() {

    var module = angular.module("SearchModule", ["StorageModule"]);

    module.controller("SearchController", [
        "Loading", "Error", "View", "Storage", "$http", function(Loading, Error, View, Storage, $http) {

            var self = this;

            self.from = "";
            self.to = "";
            self.wayFrontDate = "";
            self.wayBackDate = "";
            self.adults = 1;
            self.childs = 0;
            self.infants = 0;
            self.serviceClass = "E";
            self.supportPhone = Storage.GetSupportPhone;

            $("#from").autocomplete({
                source: "/Typeahead/TravelPoints",
                minLength: 2,
                select: function(event, ui) {
                    self.from = ui.item.id;
                    $("#from").val(ui.item.value);
                }
            });

            $("#to").autocomplete({
                source: "/Typeahead/TravelPoints",
                minLength: 2,
                select: function(event, ui) {
                    self.to = ui.item.id;
                    $("#to").val(ui.item.value);
                }
            });

            $("#fromDate").datepicker({ dateFormat: "dd.mm.yy" });
            $("#toDate").datepicker({ dateFormat: "dd.mm.yy" });
            $(".date").datepicker({ dateFormat: "dd.mm.yy" });

            self.validate = function()
            {
                // validation
                $(".my-form .form-group").removeClass("has-error");
                if (self.from === "") {
                    $("#from").parent().addClass("has-error");
                }
                if (self.to === "") {
                    $("#to").parent().addClass("has-error");
                }
                if (/\d{2}.\d{2}.\d{2}/.test(self.wayFrontDate) === false) {
                    $("#fromDate").parent().addClass("has-error");
                }

                var d = self.wayFrontDate;
                var now = new Date();

                //console.log(d.substring(6, 10));
                //console.log(d.substring(3, 5));
                //console.log(d.substring(0, 2));

                //var wayFront = new Date(d.substring(6, 10), d.substring(3, 5), d.substring(0, 2), 0, 0, 0, 0);
                //console.log("wayFron: " + wayFront);
                //if (wayFront < now) {
                //    $("#fromDate").parent().addClass("has-error");
                //}

                if ($(".my-form .form-group.has-error").length > 0) {
                    return false;
                }

                return true;
            }

            // Search for fares
            self.start = function() {

                Loading.Show();

                $http.post("/Terminal/InitRequest", {
                        from: self.from,
                        to: self.to,
                        d1: self.wayFrontDate,
                        d2: self.wayBackDate,
                        cl: self.serviceClass,
                        amateurs: self.adults,
                        childs: self.childs,
                        infants: self.infants
                    }).
                    success(function(data, status, headers, config) {

                        if (data.Success === true) {

                            Storage.SetTotalTravelers(parseInt(self.adults) + parseInt(self.childs));
                            Storage.SetSearchId(data.Data);
                            var requestId = Storage.GetSearchId();
                            var percents = "0";

                            for (var i = 0; i < 10; i++) {

                                $.ajax({
                                    url: "/Terminal/SearchStatus",
                                    type: "POST",
                                    data: { r: requestId },
                                    async: false,
                                    success: function(d) {

                                        if (d.Success === true) {
                                            percents = d.Data;
                                            console.log("searching: " + percents);

                                            if (percents == 100) {
                                                i = 10;

                                                View.Fares();
                                            }

                                        } else {
                                            alert("requets error");
                                        }
                                    },
                                    error: function() {
                                        console.log("failed: " + percents);
                                    }
                                });

                                self.sleep(2000);
                                console.log("SearchStatus attempt: " + i);

                                if (i == 9) {
                                    Error.Show("Ошибка при поиске рейсов", "Сервис недоступен. Пожалуйста попробуйте позже.")
                                }
                            }

                        } else {

                            Error.Show("Ошибка при поиске рейсов", "Некорректные данные, пожалуйста проверьте введенные города и даты. Обратите внимание - количество младенцев не может привышать одного на одного возрослого пассажира.")
                        }
                    }).
                    error(function(data, status, headers, config) {
                        Error.Show("Ошибка при поиске рейсов", "Сервер вернул ошибку. Пожалуйста обратитесь в службу поддержки пользователей " + $scope.supportPhone);
                    });
                // } else {
                //     alert('form valiation eror');
                //}
            };

            self.testFill = function () {
                self.from = "DME";
                self.to = "LED";
                self.wayFrontDate = "23.07.2015";
                self.wayBackDate = "27.07.2015";
                self.adults = 2;
            };

            self.sleep = function (milliseconds) {
                var start = new Date().getTime();
                for (var i = 0; i < 1e7; i++) {
                    if ((new Date().getTime() - start) > milliseconds) {
                        break;
                    }
                }
            };
        }
    ]);

})();