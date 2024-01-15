

// Category Drop Down

$(document).ready(function () {

    // project master
    $('#ddProjectMaster').select2({
        theme: 'modern',
        placeholder: 'Select here.....',
        allowClear: false,
    });
    $('#ddProjectMaster').on('select2:select', function (e) {
        __doPostBack('<%= ddProjectMaster.ClientID %>', '');
    });

    // ao details
    $('#ddAODetails').select2({
        theme: 'modern',
        placeholder: 'Select here.....',
        allowClear: false,
    });
    $('#ddAODetails').on('select2:select', function (e) {
        __doPostBack('<%= ddAODetails.ClientID %>', '');
    });

    // work order no
    $('#ddWorkOrder').select2({
        theme: 'modern',
        placeholder: 'Select here.....',
        allowClear: false,
    });
    $('#ddWorkOrder').on('select2:select', function (e) {
        __doPostBack('<%= ddWorkOrder.ClientID %>', '');
    });

    // work order / tender value
    $('#ddWoTendorValue').select2({
        theme: 'modern',
        placeholder: 'Select here.....',
        allowClear: false,
    });
    $('#ddWoTendorValue').on('select2:select', function (e) {
        __doPostBack('<%= ddWoTendorValue.ClientID %>', '');
    });
});