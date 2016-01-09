

$(document).ready(function($){

    $(function () {
        $("#listingFilterTabs").tabs({ active: $("#TabId").val() });
    });

    $(".viewListingsTabs").click(function () {
        var tabId = $(this).attr("tabId");
        $("#TabId").val(tabId);
    });

    $("#viewListings_header_returnLink").click(function () {
        document.location.href = "/"
    });

    $(".listingLink").click(function () {
        var listingId = $(this).attr("listingId");
        document.location.href = "/Listings/DisplayListing?listingId=" + listingId;
    });

});