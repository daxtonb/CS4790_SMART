// Retrieves form and places in form modal
function getForm(modalTitle, getHandlerUrl, postHandlerUrl) {

    // Add form action
    if (postHandlerUrl) {
        $('#modal-form').attr('action', postHandlerUrl);
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