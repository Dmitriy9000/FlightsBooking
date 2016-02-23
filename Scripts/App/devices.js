var PE = PE || {};

PE.Devices = PE.Devices || (function () {

    var enableMoneyReceiving = function() {
        $.ajax({
            url: "/Devices/EnableMoneyReceiving",
            type: "POST",
            async: false,
            success: function (d) {

                if (d.Success === true) {

                } else {
                    console.log("Service call failed");
                }

            },
            error: function () {
                console.log("Failed");
            }
        });
    };

    var disableMoneyReceiving = function () {
        $.ajax({
            url: "/Devices/DisableMoneyReceiving",
            type: "POST",
            async: false,
            success: function (d) {
                if (d.Success === true) {

                } else {
                    console.log("Service call failed");
                }
            },
            error: function () {
                console.log("Failed");
            }
        });
    };

    var recyclerStoredCount = function () {
        var result = "";
        $.ajax({
            url: "/Devices/RecyclerStoredCount",
            type: "POST",
            async: false,
            success: function (d) {
                
                if (d.Success === true) {
                    result = d.Message;
                } else {
                    console.log("Service call failed");
                }

            },
            error: function () {
                console.log("Failed");
            }
        });
        return result;
    };

    var getMoneyReceived = function () {
        var result = "-1";
        $.ajax({
            url: "/Devices/GetMoneyReceived",
            type: "POST",
            async: false,
            success: function (d) {

                if (d.Success === true) {
                    result = d.Message;
                } else {
                    console.log("Service call failed");
                }

            },
            error: function () {
                console.log("Failed");
            }
        });
        return result;
    };

    var returnPushedMoney = function () {
        $.ajax({
            url: "/Devices/ReturnPushedMoney",
            type: "POST",
            async: false,
            success: function (d) {
                if (d.Success === true) {
                    
                } else {
                    console.log("Service call failed");
                }
            },
            error: function () {
                console.log("Failed");
            }
        });
    };

    var sendNotes = function (cnt) {
        $.ajax({
            url: "/Devices/SendNotes",
            type: "POST",
            data: { count: cnt },
            async: false,
            success: function (d) {
                if (d.Success === true) {

                } else {
                    console.log("Service call failed");
                }
            },
            error: function () {
                console.log("Failed");
            }
        });
    };

    var change = function (cnt) {
        $.ajax({
            url: "/Devices/Change",
            type: "POST",
            data: { count: cnt },
            async: false,
            success: function (d) {
                if (d.Success === true) {

                } else {
                    console.log("Service call failed");
                }
            },
            error: function () {
                console.log("Failed");
            }
        });
    };

    var sendOneNote = function () {
        $.ajax({
            url: "/Devices/SendOneNote",
            type: "POST",
            async: false,
            success: function (d) {
                if (d.Success === true) {

                } else {
                    console.log("Service call failed");
                }
            },
            error: function () {
                console.log("Failed");
            }
        });
    };

    var dropNotesCount = function () {
        $.ajax({
            url: "/Devices/DropNotesCount",
            type: "POST",
            async: false,
            success: function (d) {
                if (d.Success === true) {

                } else {
                    console.log("Service call failed");
                }
            },
            error: function () {
                console.log("Failed");
            }
        });
    };

    var clearRecycler = function () {
        $.ajax({
            url: "/Devices/ClearRecycler",
            type: "POST",
            async: false,
            success: function (d) {
                if (d.Success === true) {

                } else {
                    console.log("Service call failed");
                }
            },
            error: function () {
                console.log("Failed");
            }
        });
    };

    var printUrl = function(url) {
        $.ajax({
            url: "/Devices/PrintUrl",
            type: "POST",
            data: { url: url },
            async: false,
            success: function (d) {
                if (d.Success === true) {

                } else {
                    console.log("Service call failed");
                }
            },
            error: function () {
                console.log("Failed");
            }
        });
    }

    var printCheck = function (amount, comission) {
        $.ajax({
            url: "/Devices/PrintCheck",
            type: "POST",
            data: { amount: amount, comission: comission },
            async: false,
            success: function (d) {
                if (d.Success === true) {

                } else {
                    console.log("Service call failed");
                }
            },
            error: function () {
                console.log("Failed");
            }
        });
    }

    return {
        EnableMoneyReceiving: enableMoneyReceiving,
        DisableMoneyReceiving: disableMoneyReceiving,
        RecyclerStoredCount: recyclerStoredCount,
        GetMoneyReceived: getMoneyReceived,
        ReturnPushedMoney: returnPushedMoney,
        SendNotes: sendNotes,
        Change: change,
        SendOneNote: sendOneNote,
        ClearRecycler: clearRecycler,
        DropNotesCount: dropNotesCount,
        PrintUrl: printUrl,
        PrintCheck: printCheck
    };

})();