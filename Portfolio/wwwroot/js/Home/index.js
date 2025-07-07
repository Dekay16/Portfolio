function openAddEditModal(id) {
    $.get('/Home/AddEditProject', { id: id }, function (data) {
        $('#addEditModalBody').html(data);
        $('#addEditProjectModal').modal('show');
        /*$.validator.unobtrusive.parse('#addeditprojectform');*/
    });
}

function deleteProject(id) {
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
};

$(document).on('submit', '#addeditprojectform', function (e) {
    // For adding or editing
    e.preventDefault();

    $.ajax({
        type: 'POST',
        url: '/Home/AddEditProject',
        data: $(this).serialize(),
        success: function (res) {
            if (res.success) {
                $('#addEditProjectModal').modal('hide');
                location.reload(); // or refresh list
            }
            else {
                $('#addEditModalBody').html(res);
                /*$.validator.unobtrusive.parse('#addeditprojectform');*/
            }
        },
        error: function (xhr) {
            alert("Error: " + xhr.responseText);
        }
    });
});
