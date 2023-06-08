// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    // Search
    $('#btnSearch').unbind('click').bind('click', function (event) {
        event.preventDefault();

        let data = $('#txtCriteria').val();
        if (data === '') {
            alert('Please enter search criteria!');
            return true;
        }
        //alert('DEBUG! ' + data);
        console.log('[DEBUG] calling Search controller with payload: ' + data);

        $.ajax({
            type: "POST",
            url: "/Search",
            data: { criteria: data },
            success: function (result) {
                //$(document).load("/Search/Index", result);
                //console.log(result);
                document.open();
                document.write(result);
                document.close();
            },
            error: function (xhr) {
                alert('Request Status: ' + xhr.status + ' Status Text: ' + xhr.statusText + ' ' + xhr.responseText);
            }
        });

        //var jqxhr = $.post("/Search", { criteria: data }, function () {
        //    alert("success");
        //})
        //    .done(function () {
        //        alert("second success");
        //    })
        //    .fail(function () {
        //        alert("error");
        //    })
        //    .always(function () {
        //        alert("finished");
        //    });
    });
});
