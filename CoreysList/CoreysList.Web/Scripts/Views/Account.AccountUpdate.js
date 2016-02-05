

$(document).ready(function ($) {

    //start page with all text boxes hidden
    $("#Email").hide();
    $("#Password").hide();
    $("#FirstName").hide();
    $("#LastName").hide();
    $("#PhoneNum").hide();

    $(".emailChange").click(function () {
        if($(this).text()== "[change]"){
            $("#Email").show();
            $(".emailText").hide();
            $(this).text("[cancel]");
        } else {
            $("#Email").hide();
            $(".emailText").show();
            $("#Email").val($(".emailText").text());
            $(this).text("[change]");
        }
    });

    $(".passwordChange").click(function () {
        if ($(this).text() == "[change]") {
            $("#Password").show();
            $(".passwordText").hide();
            $(this).text("[cancel]");
        } else {
            $("#Password").hide();
            $(".passwordText").show();
            $("#Password").val($(".passwordText").text());
            $(this).text("[change]");
        }
    });

    $(".firstNameChange").click(function () {
        if ($(this).text() == "[change]") {
            $("#FirstName").show();
            $(".firstNameText").hide();
            $(this).text("[cancel]");
        } else {
            $("#FirstName").hide();
            $(".firstNameText").show();
            $("#FirstName").val($(".firstNameText").text());
            $(this).text("[change]");
        }
    });

    $(".lastNameChange").click(function () {
        if ($(this).text() == "[change]") {
            $("#LastName").show();
            $(".lastNameText").hide();
            $(this).text("[cancel]");
        } else {
            $("#LastName").hide();
            $(".lastNameText").show();
            $("#LastName").val($(".lastNameText").text());
            $(this).text("[change]");
        }
    });

    $(".phoneNumberChange").click(function () {
        if ($(this).text() == "[change]") {
            $("#PhoneNum").show();
            $(".phoneNumberText").hide();
            $(this).text("[cancel]");
        } else {
            $("#PhoneNum").hide();
            $(".phoneNumberText").show();
            $("#PhoneNum").val($(".phoneNumberText").text());
            $(this).text("[change]");
        }
    });

    $('#updateAccount').submit(function () {
        return $(this).valid();
    });

});