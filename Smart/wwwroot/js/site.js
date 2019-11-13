// Retrieves form and places in form modal
function getForm(modalTitle, getHandlerUrl, postHandlerUrl) {

    // Add form action
    if (postHandlerUrl) {
        $('#modal-form').attr('action', postHandlerUrl);
    }

    const modalBody = $('#modal-body');

    // Clear modal body
    $(modalBody).text('Loading...');

    // Add modal title
    $('#modal-title').text(modalTitle);

    // Get form HTML and intialize client-side validation
    $(modalBody).load(getHandlerUrl, null, () => { $.validator.unobtrusive.parse('form'); });
}