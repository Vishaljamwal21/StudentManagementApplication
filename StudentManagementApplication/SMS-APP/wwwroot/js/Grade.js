var datatable;

$(document).ready(function () {
    loaddatatable();
});

function loaddatatable() {
    datatable = $("#tabledata").DataTable({
        "ajax": {
            "url": "/Grade/GetAll", // Corrected URL for retrieving enrollment data
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "enrollment.student.name", "width": "20%" }, // Assuming "name" is the field containing the student's name
            { "data": "enrollment.course.title", "width": "20%" }, // Assuming "title" is the field containing the course title
            { "data": "gradeValue", "width": "20%" }, // Assuming "enrollmentDate" is the field containing the enrollment date
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a class="btn btn-danger" onclick="DeleteGrade(${data})"><i class="fas fa-trash"></i></a>
                            </div>
                            `;
                }
            }
        ]
    });
}

function DeleteGrade(gradeId) {
    swal({
        text: "Delete Grade",
        title: "Do you want to delete the information",
        buttons: true,
        icon: "warning",
        dangermodel: true
    }).then((willdelete) => {
        if (willdelete) {
            $.ajax({
                url: `/Grade/Delete?id=${gradeId}`, 
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
