

$(document).ready(function ($) {

    $(".locator_stateColumn_listItem").click(function () {
        var cityId = $(this).attr("cityId");
        setCookie("cityId", cityId, 30);
        window.location.href = "/home/index";
    });
});