
$(document).ready(function($){

    $(".displayListing_content_ThumbPic_Container").click(function () {
        $(".displayListing_content_ThumbPic_Container").css("border", "1px solid black");
        $(this).css("border", "1px solid red");
        var imageId = $(this).attr("imgId");
        var mainImage = $(".mainImgDisplay");
        var imgSrc = "/Images/GetImage/" + imageId;
        mainImage.attr("src", imgSrc)
        previousImgDiv = $(this);
    });

    $(".displayListing_breadCrumb_homeLink").click(function () {
        document.location.href = "/";
    });

    $(".displayListing_breadCrumb_homeLink").click(function () {
        var subCategoryId = $("#SubCatId").val();
        document.location.href = "/Listings/?subCategoryId=" + subCategoryId;
    });

});