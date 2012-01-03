// ----------------------------------------------------------------------------
//  buttonhandlers.js
//  Copyright (c) 2012, Lucas M. Suggs
//
//  These are the jQuery event handlers for user input on the calculator.
// ----------------------------------------------------------------------------
var isDefault = true;

$(window).load(function () {
    isDefault = true;
});

$(document).ready(function () {
    $("#CurrentValue").focus();

    $("#Zero").click(function () {
        if (isDefault) {
            $("#CurrentValue").val("");
            isDefault = false;
        }
        $("#CurrentValue").val($("#CurrentValue").val() + "0");
    });

    $("#One").click(function () {
        if (isDefault) {
            $("#CurrentValue").val("");
            isDefault = false;
        }
        $("#CurrentValue").val($("#CurrentValue").val() + "1");
    });

    $("#Two").click(function () {
        if (isDefault) {
            $("#CurrentValue").val("");
            isDefault = false;
        }
        $("#CurrentValue").val($("#CurrentValue").val() + "2");
    });

    $("#Three").click(function () {
        if (isDefault) {
            $("#CurrentValue").val("");
            isDefault = false;
        }
        $("#CurrentValue").val($("#CurrentValue").val() + "3");
    });

    $("#Four").click(function () {
        if (isDefault) {
            $("#CurrentValue").val("");
            isDefault = false;
        }
        $("#CurrentValue").val($("#CurrentValue").val() + "4");
    });

    $("#Five").click(function () {
        if (isDefault) {
            $("#CurrentValue").val("");
            isDefault = false;
        }
        $("#CurrentValue").val($("#CurrentValue").val() + "5");
    });

    $("#Six").click(function () {
        if (isDefault) {
            $("#CurrentValue").val("");
            isDefault = false;
        }
        $("#CurrentValue").val($("#CurrentValue").val() + "6");
    });

    $("#Seven").click(function () {
        if (isDefault) {
            $("#CurrentValue").val("");
            isDefault = false;
        }
        $("#CurrentValue").val($("#CurrentValue").val() + "7");
    });

    $("#Eight").click(function () {
        if (isDefault) {
            $("#CurrentValue").val("");
            isDefault = false;
        }
        $("#CurrentValue").val($("#CurrentValue").val() + "8");
    });

    $("#Nine").click(function () {
        if (isDefault) {
            $("#CurrentValue").val("");
            isDefault = false;
        }
        $("#CurrentValue").val($("#CurrentValue").val() + "9");
    });

    $("#Period").click(function () {
        if (isDefault) {
            $("#CurrentValue").val("");
            isDefault = false;
        }
        $("#CurrentValue").val($("#CurrentValue").val() + ".");
    });

    $("#ClearEntry").click(function () {
        $("#CurrentValue").val("0");
    });

    $("#ChangeSign").click(function () {
        if (isDefault) {
            $("#CurrentValue").val("");
            isDefault = false;
        }
        if ($("#CurrentValue").val().substr(0, 1) == "-") {
            $("#CurrentValue").val($("#CurrentValue").val().substr(1));
        }
        else {
            $("#CurrentValue").val("-" + $("#CurrentValue").val());
        }
    });

    $("#RemoveEntry").click(function () {
        $("#CurrentValue").val($("#CurrentValue").val().slice(0, -1));

        if (0 == $("#CurrentValue").val().length)
            $("#CurrentValue").val("0");
    });

    $("#CurrentHistory").keypress(function (event) {
        if (13 == event.which) {
            event.preventDefault();
        }
    });

    $("#CurrentValue").keypress(function (event) {
        if (13 == event.which) {
            $("#Equals").click();
        }
        else if (42 == event.which) {
            $("#Multiply").click();
        }
        else if (43 == event.which) {
            $("#Plus").click();
        }
        else if (45 == event.which) {
            $("#Minus").click();
        }
        else if (46 == event.which) {
            $("#Period").click();
        }
        else if (47 == event.which) {
            $("#Divide").click();
        }
        else if (48 == event.which) {
            $("#Zero").click();
        }
        else if (49 == event.which) {
            $("#One").click();
        }
        else if (50 == event.which) {
            $("#Two").click();
        }
        else if (51 == event.which) {
            $("#Three").click();
        }
        else if (52 == event.which) {
            $("#Four").click();
        }
        else if (53 == event.which) {
            $("#Five").click();
        }
        else if (54 == event.which) {
            $("#Six").click();
        }
        else if (55 == event.which) {
            $("#Seven").click();
        }
        else if (56 == event.which) {
            $("#Eight").click();
        }
        else if (57 == event.which) {
            $("#Nine").click();
        }
    });
});
