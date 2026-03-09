function openAddEditModal(id) {
    $.get('/Home/AddEditProject', { id: id }, function (data) {
        $('#addEditModalBody').html(data);
        $('#addEditProjectModal').modal('show');
        $.validator.unobtrusive.parse('#addeditprojectform');
    });
}

function deleteProject(id) {
    let isDelete = confirm("Are you sure you would like to delete this project?");
    if (isDelete) {
        $.ajax({
            type: "GET",
            url: "/Home/DeleteProject",
            data: { id: id },
            datatype: 'json',
            success: function (response) {
                if (response.success) {
                    alert("Successfully removed project.")
                    location.reload();
                }
                else {
                    alert("A server error occured when deleting project.")
                }

            },
            error: function (response) {
                alert("A client error occured when deleting project.")
            }
        })
    }
};

$(document).on('submit', '#addeditprojectform', function (e) {
    e.preventDefault();

    var formData = new FormData(this); 

    $.ajax({
        type: 'POST',
        url: '/Home/AddEditProject',
        data: formData,
        processData: false,  
        contentType: false,  
        success: function (res) {
            if (res.success) {
                $('#addEditProjectModal').modal('hide');
                location.reload();
            } else {
                $('#addEditModalBody').html(res);
            }
        },
        error: function (xhr) {
            alert("Error: " + xhr.responseText);
        }
    });
});
