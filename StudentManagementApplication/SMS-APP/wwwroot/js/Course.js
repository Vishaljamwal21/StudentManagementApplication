var datatable;
$(document).ready(function () {
    loaddatatable();
})
function loaddatatable() {
    datatable = $("#tabledata").DataTable({
        "ajax": {
            "url": "Course/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "title", "width": "20%" },
            { "data": "description", "width": "20%" },
            { "data": "duration", "width": "20%" },           
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                           <a class="btn btn-primary" href="/Course/SaveorUpdate/${data}"><i class="fas fa-edit"></i></a>
                            <a class="btn btn-danger" onclick=Delete("Course/Delete/${data}")><i class="fas fa-trash"></i></a>
                            </div>
                            `;
                }
            }
        ]
    })
}
function Delete(url) {
    swal({
        text: "Delete Course",
        title: "Do you want to delete the information",
        buttons: true,
        icon: "warning",
        dangermodel: true
    }).then((willdelete) => {
        if (willdelete) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        datatable.ajax.reload();
                        toastr.success(data.messege);
                    }
                    else {
                        toastr(data.messege);
                    }
                }
            })
        }
    })
}