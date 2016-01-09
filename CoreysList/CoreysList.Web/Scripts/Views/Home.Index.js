

$(document).ready(function($){

    $(".home_rightBar_usStatesList").hide();

    $(".home_rightBar_usCitiesHeader").click(function () {
        $(".home_rightBar_usStatesList").slideUp();
        $(".home_rightBar_usCitiesList").slideDown();
    });

    $(".home_rightBar_usStatesHeader").click(function () {

        $(".home_rightBar_usStatesList").slideDown();
        $(".home_rightBar_usCitiesList").slideUp();
    });

    $(".home_rightBar_usCitiesItem").click(function () {
        var cityId = $(this).attr("cityId");
        setCookie("cityId", cityId, 30);
        window.location.href = "/home/index";
    });

    $(".home_rightBar_usStatesItem").click(function () {
        var stateId = $(this).attr("stateId");
        window.location.href = "/home/StateSelector?stateId=" + stateId;
    });

    $(".home_leftBar_menu_item.faq").click(function () {
        $(".home_modal_faq").dialog({
            modal: true,
            height: 500,
            width: 700,
            autoOpen: true,
            show: {
                effect: "slide",
                duration: 700
            }
        });
    });

    $(".home_leftBar_menu_item.avoidscams").click(function () {
        $(".home_modal_avoidscams").dialog({
            modal: true,
            height: 500,
            width: 700,
            autoOpen: true,
            show: {
                effect: "slide",
                duration: 700
            }
        });
    });

    $(".home_leftBar_menu_item.personalSafetyTips").click(function () {
        $(".home_modal_personalSafetyTips").dialog({
            modal: true,
            height: 500,
            width: 700,
            autoOpen: true,
            show: {
                effect: "drop",
                duration: 700
            }
        });
    });

    $(".home_leftBar_menu_item.termsOfUse").click(function () {
        $(".home_modal_termsOfUse").dialog({
            modal: true,
            height: 500,
            width: 700,
            autoOpen: true,
            show: {
                effect: "slide",
                duration: 700
            }
        });
    });

    $(".home_leftBar_menu_item.privacyPolicy").click(function () {
        $(".home_modal_privacyPolicy").dialog({
            modal: true,
            height: 500,
            width: 700,
            autoOpen: true,
            show: {
                effect: "slide",
                duration: 700
            }
        });
    });

    $(".home_leftBar_logo").click(function () {
        window.location.href = "/home/Locator";
    });

    $(".home_leftBar_menu_item.myAccount").click(function () {
        window.location.href = "/accounts/Index";
    });

    $(".home_leftBar_menu_item.postToClassifieds").click(function () {
        window.location.href = "/accounts/Index";
    });

    $(".home_center_subcategory_list_item").click(function () {
        var subcategoryId = $(this).attr("subcategoryId");
        var cityId = $("#CityId").val();
        window.location.href = "/Listings/Index?subcategoryId=" + subcategoryId;
    });
   
});