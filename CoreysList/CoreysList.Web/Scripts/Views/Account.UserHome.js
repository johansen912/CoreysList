
function SuccessMessage() {
    dialog.dialog("close");
}
function FailMessage() {
    alert("Fail Post");
}


$(document).ready(function ($) {
    //open listings dialog when user wants to edit dialog
    $(".userHome_listing_edit").on("click", function () {
        loadListing($(this).attr("listingId"));
        dialog.dialog("open");
    });
    // open dialog when usere creates new listing
    $("#newPosting").on("click", function () {
        loadListing($(this).attr("listingId"));
        dialog.dialog("open");
    });
    //Dialog for edit listing
    dialog = $("#userHome_listing_editModal").dialog({
        autoOpen: false,
        height: 600,
        width: 550,
        modal: true,
        buttons: {
            "Save Listing": function () {
                $("#editListingForm").submit();
            },
            Cancel: function () {
                dialog.dialog("close");
            }
        },
        close: function () {
        }
    });

    //load the listing in the modal 
    function loadListing(listingId) {
        $.ajax({
            type: 'GET',
            url: "EditListing",
            data: { "listingId": listingId },
            success: function (result) {
                $("#userHome_listing_editModal").html(result);
            }
        });
    };

    //load the imgs in the modal 
    function loadImgs(listingId) {
        $.ajax({
            type: 'GET',
            url: "EditImages",
            data: { "listingId": listingId },
            success: function (result) {
                $("#userHome_listing_editImgsModal").html(result);
            }
        });
    };

    //open imgdialog when user clicks images
    $(".userHome_listing_editImgs").on("click", function () {
        loadImgs($(this).attr("listingId"));
        imgDialog.dialog("open");
    });

    //dialog for edit imgs
    imgDialog = $("#userHome_listing_editImgsModal").dialog({
        autoOpen: false,
        height: 450,
        width: 550,
        modal: true,
        buttons: {
            "Finished": function () {
                imgDialog.dialog("close");
            }
        },
        close: function () {
        }
    });


    //create tabs to filter data
    $("#userHome_filter_tabs").tabs(
        {
            active: $("#TabId").val(),
            heightStyle: "fill"
        });

    //function to change the active or deactive based on user 
    $(".userHome_listing_activate").click(function () {
        var listingId = $(this).attr("listingId");
        var link = $(this);
        $.ajax({
            type: 'POST',
            url: "ListingActivation",
            data: { "listingId": listingId},
            success: function (json) {
                if (json.ActivationStatus == "active") {
                        link.html("deactivate");
                    } else {
                        link.html("activate");
                    }        
            }
        });
    });

    
    // tabs to view listing by gallery, listings, thumbs
    $(".listingsTabs").click(function () {
        var tabId = $(this).attr("tabId");
        $("#TabId").val(tabId);
    });
    //refresh button to update the listings active status and new listings
    $(".userHome_listing_refreshBtn").click(function () {
        currentTab = $("#TabId").val();
        document.location.href = "/Accounts/UserHome?tabId=" + currentTab;
    });

});