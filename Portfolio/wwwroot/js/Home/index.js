$(document).ready(function () {
    $('.open-modal').click(function () {
        const id = $(this).data('id');
        const url = id ? `/Home/AddEditProject?id=${id}` : `/Home/AddEditProject`;

        $.get(url, function (html) {
            $('#projectContent').html(html);
            const modal = new bootstrap.Modal(document.getElementById('addEditProjectModal'));
            modal.show();

            // For adding or editing

            //$('#itemForm').on('submit', function (e) {
            //    e.preventDefault();

            //    $.ajax({
            //        type: 'POST',
            //        url: '/Items/SaveItem',
            //        data: $(this).serialize(),
            //        success: function (res) {
            //            if (res.success) {
            //                modal.hide();
            //                location.reload(); // or refresh list
            //            }
            //        },
            //        error: function (xhr) {
            //            alert("Error: " + xhr.responseText);
            //        }
            //    });
            //});
        });
    });
});
