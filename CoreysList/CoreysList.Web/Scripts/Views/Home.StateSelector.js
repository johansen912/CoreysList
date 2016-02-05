

$(document).ready(function ($) {
    $(".stateSelector_cityItem").click(function () {
      var cityId = $(this).attr("cityId");
       setCookie("cityId", cityId, 30);
       window.location.href = "/home/index";
    });
});