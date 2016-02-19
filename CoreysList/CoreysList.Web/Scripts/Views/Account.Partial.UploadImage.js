
$(document).ready(function ($) {
    document.getElementById('uploader').onsubmit = function () {
        var formdata = new FormData(); //FormData object
        var fileInput = document.getElementById('fileInput');
        //Iterating through each files selected in fileInput
        for (i = 0; i < fileInput.files.length; i++) {
            //Appending each file to FormData object
            formdata.append(fileInput.files[i].name, fileInput.files[i]);
        }
        //Creating an XMLHttpRequest and sending
        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/Images/Upload/'+ $("#listingId").val());
        xhr.send(formdata);
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {
                $("#userHome_listing_editImgsModal").html(xhr.responseText);
            }
        }
        
        return false;
    }

    $(".uploadImages_DeleteBtn").click(function () {
        var imageId = $(this).attr("imageId");
        $.ajax({
            type: 'POST',
            url: "/Images/Delete/"+ imageId,
            success: function (data) {
                $("#userHome_listing_editImgsModal").html(data);
            }
        });
    });

});

