(function() {

    var module = angular.module("PassengersModule", ["StorageModule"]);

    module.controller("PassengersController", [
        "Loading", "Error", "View", "Storage", "$http", function(Loading, Error, View, Storage, $http) {

            var self = this;

            self.info = [];            
            
            self.client = Storage.GetClient;
            self.amount = Storage.GetAmount;
            self.priceToPay = Storage.GetPriceToPay;
            self.comission = Storage.GetComission;

            self.checkPassengersInfo = function () {

                // validation
                $(".passengers .form-group").removeClass("has-error");

                // client validation
                //var $phone = $("#client-phone");
                //if (/\d{10}/.test($phone.val()) === false) {
                //    $phone.parent().addClass("has-error");
                //}
                var $email = $("#client-email");
                console.log("email: ", $email.val());
                if (/([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})/.test($email.val()) === false) {
                    $email.parent().addClass("has-error");
                }

                // passengers validation
                $(".passenger-info").each(function () {

                    var $firstName = $(this).find(".first-name");
                    var $lastName = $(this).find(".last-name");
                    var $middleName = $(this).find(".middle-name");
                    var $sex = $(this).find(".sex");
                    var $dob = $(this).find(".dob");
                    var $docNumber = $(this).find(".doc-number");
                    var $docExp = $(this).find(".doc-exp");

                    if (/\w{2,}/.test($firstName.val()) === false) {
                        $firstName.parents(".form-group").addClass("has-error");
                    }
                    if (/\w{2,}/.test($lastName.val()) === false) {
                        $lastName.parents(".form-group").addClass("has-error");
                    }
                    if (/\w{2,}/.test($middleName.val()) === false) {
                        $middleName.parents(".form-group").addClass("has-error");
                    }
                    if (/[M|F]{1}/.test($sex.val()) === false) {
                        $sex.parents(".form-group").addClass("has-error");
                    }

                    if (/\d{2}\.\d{2}\.\d{4}/.test($dob.val()) === false) {
                        $dob.parents(".form-group").addClass("has-error");
                    }
                    var dobParts = $dob.val().split(".");
                    var d = new Date(dobParts[2], dobParts[1], dobParts[0]);
                    var today = new Date().getTime()
                    if ((today - d) < 0) {
                        $dob.parents(".form-group").addClass("has-error");
                    }

                    if (/\d{4}\s{1}\d{6}/.test($docNumber.val()) === false) {
                        $docNumber.parents(".form-group").addClass("has-error");
                    }

                    //if (/\d{2}\.\d{2}\.\d{4}/.test($docExp.val()) === false) {
                    //    $docExp.parents(".form-group").addClass("has-error");
                    //}
                    var expParts = $docExp.val().split(".");
                    var ex = new Date(expParts[2], expParts[1], expParts[0]);
                    if ((today - ex) > 0) {
                        $docExp.parents(".form-group").addClass("has-error");
                    }

                })

                if ($(".passengers .form-group.has-error").length > 0) {
                    return false;
                }

                Storage.SetPassengers(self.info);

                View.Money();
            };
           
            self.fill = function () {
                var passengers = [];

                var client = {
                    PhoneCode: "RU|7",
                    PhoneNumber: "9267678054",
                    Email: "d.koudinov@gmail.com"
                };
                Storage.SetClient(client);

                var total = Storage.GetTotalTravelers();
                for (var i = 0; i < total; i++) {
                    passengers.push({
                        FirstName: "Dmitriy",
                        LastName: "Kudinov",
                        MiddleName: "Serggevich",
                        Sex: "M",
                        Dob: "10.10.1987",
                        Country: "RU",
                        DocNumber: "1112 321122",
                        DocExpDate: "20.10.2020"
                    })
                }

                self.info = passengers;
                //Storage.SetPassengers(passengers);
            };
        }
    ]);

})();