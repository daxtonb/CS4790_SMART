let doRefresh = false;

// Retrieves form and places in form modal
function getForm(modalTitle, getHandlerUrl, postHandlerUrl, successCallback, failureCallback, validator) {

    const modalForm = $('#modal-form');

    // Add form action
    if (postHandlerUrl) {
        $(modalForm).attr('action', postHandlerUrl);
    }
    // Add unobtrusive AJAX
    if (successCallback || failureCallback) {
        $(modalForm).attr('data-ajax', true);
    }
    // Add success callback
    if (successCallback) {
        $(modalForm).attr('data-ajax-success', successCallback.name);
    }
    // Add failure callback
    if (failureCallback) {
        $(modalForm).attr('data-ajax-failure', failureCallback.name);
    }
    // Add validation
    if (validator) {
        $(modalForm).on('submit', validator);
    }

    const modalBody = $('#form-modal-body');

    // Clear modal body
    $(modalBody).text('Loading...');

    // Add modal title
    $('#form-modal-title').text(modalTitle);

    // Get form HTML and intialize client-side validation
    $(modalBody).load(getHandlerUrl, null, () => { $.validator.unobtrusive.parse('form'); });
}

// Retrieves details and places in details modal
function getDetails(modalTitle, getHandlerUrl, modalSize) {

    const modalBody = $('#details-modal-body');
    const modalDialog = $('#details-modal-dialog');
    const sizeClasses = ['modal-xl', 'modal-lg', 'modal-sm'];

    for (const sizeClass of sizeClasses) {
        $(modalDialog).removeClass(sizeClass);
    }

    if (modalSize) {
        $(modalDialog).addClass('modal-' + modalSize);
    }

    // Clear modal body
    $(modalBody).text('Loading...');

    // Add modal title
    $('#details-modal-title').text(modalTitle);

    // Get form HTML and intialize client-side validation
    $(modalBody).load(getHandlerUrl, null, () => { $.validator.unobtrusive.parse('form'); });
}

$(document).on('hide.bs.modal', '.modal', function () {
    // CONDITION: Page refresh is queued. 
    if (doRefresh) {
        window.location.reload();
    }
});