// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function callAjaxPost(pTitle, pText, pConfirmButtonText, pCancelButtonText,
    pContentType, pDataType, pUrl, pData) {
    swal.fire({
        title: pTitle,
        text: pText,
        icon: 'info',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: pConfirmButtonText,
        cancelButtonText: pCancelButtonText
    }).then((result) => {
        if (result.value) {
            $.ajax({
                contentType: pContentType,
                dataType: pDataType,
                type: "POST",
                url: pUrl,
                data: pData,
                success: function (data) {
                    if (data.isSuccess == true) {
                        swal.fire('با تشکر از شما', data.message, 'success')
                            .then(function (isConfirm) {
                                location.reload();
                            });
                    }
                    else {
                        swal.fire('هشدار!', data.message, 'warning');
                    }
                },
                error: function (request, status, error) {
                    alert(request.responseText);
                }
            });
        }
    })
}

function showModal(pMethodType,pUrl,pData,pModalId) {
    $.ajax({
        contentType: "application/x-www-form-urlencoded",
        dataType: "html",
        type: pMethodType,
        url: pUrl,
        data: pData,
        success: function (response) {
            $(pModalId).find(".modal-body").html(response);
            $(pModalId).modal('show');
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}