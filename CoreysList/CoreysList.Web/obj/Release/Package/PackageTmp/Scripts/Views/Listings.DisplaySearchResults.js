
$(document).ready(function ($) {
    //Disable city select list if there is only one select list item
    $(window).load(function () {
        if ($('#citiesSelectList > option').length == 1) {
            $('#citiesSelectList').attr('disabled', true);
        }
    });
    //link to selected listing
    $(".listingLink").click(function () {
        var listingId = $(this).attr("listingId");
        document.location.href = "/Listings/DisplayListing?listingId=" + listingId;
    });
    //return link to home
    $("#displaySearchResults_header_returnLink").click(function () {
        document.location.href = "/"
    });
    //jquery ui for tabs
    $(function () {
        $("#listingFilterTabs").tabs({ active: $("#TabId").val() });
    });
    //jquery ui for slider
    $(function () {
        $("#slider-range").slider({
            range: true,
            min: 0,
            max: 10000,
            values: [ $("#priceMin").val(), $("#priceMax").val()],
            slide: function (event, ui) {
                $("#amount").val("$" + ui.values[0] + " - $" + ui.values[1]);
            },
            //on change bind the selected values to the hidden fields
            change: function (event, ui) {
                $("#priceMin").val(ui.values[0]);
                $("#priceMax").val(ui.values[1]);
            },
            stop: function (event, ui) {
                $("#priceMin").val(ui.values[0]);
                $("#priceMax").val(ui.values[1]);
            }
        });
        $("#amount").val("$" + $("#slider-range").slider("values", 0) +
                            " - $" + $("#slider-range").slider("values", 1));
    });
    // when a state is selected
    $('#statesSelectList').change(function () {
        //get the state id
        var stateID = $(this).val();
        //apply a message while waiting on response
        var procemessage = "<option value='0'> Please wait...</option>";
        //show the message
        $("#citiesSelectList").html(procemessage).show();
        //get the correct url to the the controller action
        var url = "/Listings/GetCitiesByStateId/";
        //make an ajax post to call the action and send the stateID
        $.ajax({
            type: 'POST',
            url: url,
            data: { "stateID": stateID },
            success: function (json) {
                if (json.Success) {
                    //on success populate cities select list with the returned json html
                    $("#citiesSelectList").html(json.SelectOptionsHtml);
                    //enable the cities select list
                    $('#citiesSelectList').attr('disabled', false);
                } else {
                    alert(json.Error);
                }
            }
        });
    });
});