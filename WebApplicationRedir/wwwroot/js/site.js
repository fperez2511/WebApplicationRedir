// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    
    let myTableColumnDefinitions = [
        {
            "targets": 0,
            "data": "albumId",
            "sortable": true
        },
        {
            "targets": 1,
            "data": "id",
            "sortable": true
        },
        {
            "targets": 2,
            "data": "title",
            "sortable": true
        },
        {
            "targets": 3,
            "data": "url",
            "sortable": true,
            "render": function (data, type, row, meta) {
                return '<a href="#">Details</a>'
            }
        },
        {
            "targets": 4,
            "data": "thumbnailUrl",
            "sortable": true,
            "render": function (data, type, row, meta) {
                return '<img src="' + data + '" class="small-pic">'
            }
        }
    ];
    let myTableOptions = {
        "paging": true,
        "processing": true,
        "scrollY": "350px",
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
        "serverSide": true,
        "columnDefs": myTableColumnDefinitions,
        "ajax": {
            url: "/Search/Photos",
            type: "POST",
            error: function () {
                //$('#')
                alert('DEBUG: Error!');
            }
        },
        "createdRow": function (row, data, dataIndex) {
            console.log(data);
        }
    };
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
    //
    // https://jsonplaceholder.typicode.com/photos
    //myTable.DataTable(myTableOptions);
    let myTable = $('#myTable').DataTable(myTableOptions);
});
