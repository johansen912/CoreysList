$(document).ready(function ($) {

    $("#categories").on("change", function () {
        var categoryId = $(this).val();
        $.ajax({
            type: 'GET',
            url: "GetSubCategories",
            data: { "categoryId": categoryId },
            success: function (json) {
                if (json.Success) {
                	$("#subcategories").html(json.SelectOptionsHtml);
                } else {
                    alert(json.Error);
                }
            }
        });
	});

    $("#states").on("change", function () {
		var stateId = $(this).val();
		$.ajax({
			type: 'GET',
			url: "GetCities",
			data: { "stateId": stateId },
			success: function (json) {
				if (json.Success) {
				    $("#citiesSelectList").html(json.SelectOptionsHtml);
				} else {
					alert(json.Error);
				}
			}
		});
	});
});