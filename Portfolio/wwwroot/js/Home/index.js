// Improved modal handling for Bootstrap 5 (no jQuery modal plugin).
// Ensures partial HTML initialization, unobtrusive validation parsing,
// image preview init and correct show/hide behavior.
(function ($) {
    var MOBILE_BREAKPOINT = 991.98;

    function getBootstrapModal() {
        var el = document.getElementById('addEditProjectModal');
        if (!el) return null;
        return bootstrap.Modal.getInstance(el) || new bootstrap.Modal(el, { backdrop: 'static', keyboard: false });
    }

    window.openAddEditModal = function (id) {
        $.get('/Home/AddEditProject', { id: id }, function (data) {
            $('#addEditModalBody').html(data);

            // Re-parse unobtrusive validation for newly injected form
            if ($.validator && $.validator.unobtrusive) {
                $.validator.unobtrusive.parse('#addeditprojectform');
            }

            // Initialize behaviors inside partial (image preview, focus, etc.)
            if (window.initAddEditModal) window.initAddEditModal();

            // Show the modal via Bootstrap 5 API
            var modal = getBootstrapModal();
            if (modal) {
                modal.show();
                // ensure body overflow is handled if any custom logic toggles it
                $('body').css('overflow', '');
            }
        });
    };

    window.deleteProject = function (id) {
        var isDelete = confirm("Are you sure you would like to delete this project?");
        if (!isDelete) return;

        $.ajax({
            type: "GET",
            url: "/Home/DeleteProject",
            data: { id: id },
            dataType: 'json',
            success: function (response) {
                if (response && response.success) {
                    alert("Successfully removed project.");
                    location.reload();
                } else {
                    alert("A server error occurred when deleting project.");
                }
            },
            error: function () {
                alert("A client error occurred when deleting project.");
            }
        });
    };

    // Submit handler for the injected form
    $(document).on('submit', '#addeditprojectform', function (e) {
        e.preventDefault();

        var formData = new FormData(this);

        $.ajax({
            type: 'POST',
            url: '/Home/AddEditProject',
            data: formData,
            processData: false,
            contentType: false,
            success: function (res, status, xhr) {
                // If server returned JSON success object, close modal and reload.
                if (res && typeof res === 'object' && res.success) {
                    var modal = getBootstrapModal();
                    if (modal) modal.hide();
                    location.reload();
                    return;
                }

                // Otherwise assume HTML partial returned (validation errors).
                // Inject returned HTML into modal body, re-parse validation and init behaviors.
                $('#addEditModalBody').html(res);
                if ($.validator && $.validator.unobtrusive) {
                    $.validator.unobtrusive.parse('#addeditprojectform');
                }
                if (window.initAddEditModal) window.initAddEditModal();

                // Ensure modal is visible (in case it was not shown)
                var modalEnsure = getBootstrapModal();
                if (modalEnsure) modalEnsure.show();
            },
            error: function (xhr) {
                // Try to show server error in modal if HTML returned, otherwise alert.
                var ct = xhr.getResponseHeader('content-type') || '';
                if (ct.indexOf('text/html') !== -1 && xhr.responseText) {
                    $('#addEditModalBody').html(xhr.responseText);
                    if ($.validator && $.validator.unobtrusive) {
                        $.validator.unobtrusive.parse('#addeditprojectform');
                    }
                    if (window.initAddEditModal) window.initAddEditModal();
                    var modalEnsure = getBootstrapModal();
                    if (modalEnsure) modalEnsure.show();
                } else {
                    alert("Error: " + (xhr.responseText || xhr.statusText));
                }
            }
        });
    });

    // Exposed initializer used by partial to wire file preview and focus.
    window.initAddEditModal = function () {
        // Bind change on file input (delegated in case form is replaced)
        $(document).off('change', '#ImageFile').on('change', '#ImageFile', function () {
            var file = this.files && this.files[0];
            var $preview = $('#imagePreview');

            if (!file) {
                $preview.hide().attr('src', '');
                return;
            }

            var reader = new FileReader();
            reader.onload = function (ev) {
                $preview.attr('src', ev.target.result).show();
            };
            reader.readAsDataURL(file);
        });

        // Show existing preview if present
        var $previewInit = $('#imagePreview');
        if ($previewInit.length && $previewInit.attr('src')) {
            $previewInit.show();
        }

        // Focus first visible input for accessibility
        setTimeout(function () {
            $('#addeditprojectform').find('input, textarea').filter(':visible').first().focus();
        }, 50);
    };

    // Clean up modal state on hide to avoid stale markup/validation
    $(document).on('hidden.bs.modal', '#addEditProjectModal', function () {
        // Remove injected content to ensure a fresh partial on next open
        $('#addEditModalBody').empty();
        // Remove any unobtrusive validation metadata left behind
        if ($.validator && $.validator.unobtrusive) {
            // No direct API to remove, but re-parsing on next load suffices.
        }
        // Restore body overflow if changed
        $('body').css('overflow', '');
    });

})(jQuery);
