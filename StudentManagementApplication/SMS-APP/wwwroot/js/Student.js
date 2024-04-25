var datatable;

$(document).ready(function () {
    loaddatatable();
})
function loaddatatable() {
    datatable = $("#tabledata").DataTable({
        "ajax": {
            "url": "Student/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "10%" },
            { "data": "email", "width": "15%" },
            { "data": "dateOfBirth", "width": "20%" },
            { "data": "city", "width": "15%" },
            { "data": "state", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                           <a class="btn btn-primary" href="/Student/SaveorUpdate/${data}"><i class="fas fa-edit"></i></a>
                           <a class="btn btn-primary" href="/Enrollment/SaveOrUpdate?studentId=${data}">Enroll</a> <!-- Corrected URL and added text "Enroll" -->
                            <a class="btn btn-danger" onclick=Delete("Student/Delete/${data}")><i class="fas fa-trash"></i></a>
                            </div>
                            `;
                }
            }
        ]
    })
}
function Delete(url) {
    swal({
        text: "Delete Student",
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