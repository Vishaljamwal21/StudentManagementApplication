var datatable;

$(document).ready(function () {
    loaddatatable();
});

function loaddatatable() {
    datatable = $("#tabledata").DataTable({
        "ajax": {
            "url": "/Enrollment/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "student.name", "width": "20%" },
            { "data": "course.title", "width": "20%" },
            { "data": "enrollmentDate", "width": "20%" },
            {
                "data": "id",
                "render": function (data, type, row) {
                    if ("@userRole" !== "Student") {
                        return `
                            <div class="text-center">
                                <a class="btn btn-danger" onclick="DeleteEnrollment(${data})"><i class="fas fa-trash"></i></a>
                                <a class="btn btn-primary" href="/Grade/SaveOrUpdate?enrollmentId=${data}">Grade</a> 
                            </div>
                        `;
                    } else {
                        return `
                            <div class="text-center">
                                <a class="btn btn-danger" onclick="DeleteEnrollment(${data})"><i class="fas fa-trash"></i></a>
                            </div>
                        `;
                    }
                }
            }
        ]
    });
}

function DeleteEnrollment(enrollmentId) {
    swal({
        text: "Delete Enrollment",
        title: "Do you want to delete the information",
        buttons: true,
        icon: "warning",
        dangermodel: true
    }).then((willdelete) => {
        if (willdelete) {
            $.ajax({
                url: `/Enrollment/Delete?id=${enrollmentId}`, // Pass the enrollment ID as a query parameter
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        datatable.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    // Handle error
                    console.error(xhr.responseText);
                }
            });
        }
    });
}
