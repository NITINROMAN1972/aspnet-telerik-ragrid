

// Category Drop Down
$(document).ready(function () {

    // project master
    $('#ddProjectMaster').select2({
        placeholder: 'Select here.....',
        allowClear: false,
    });
    $('#ddProjectMaster').on('select2:select', function (e) {
        __doPostBack('<%= ddProjectMaster.ClientID %>', '');
    });

    // ao details
    $('#ddAODetails').select2({
        placeholder: 'Select here.....',
        allowClear: false,
    });
    $('#ddAODetails').on('select2:select', function (e) {
        __doPostBack('<%= ddAODetails.ClientID %>', '');
    });

    // work order
    $('#ddWorkOrder').select2({
        placeholder: 'Select here.....',
        allowClear: false,
    });
    $('#ddWorkOrder').on('select2:select', function (e) {
        __doPostBack('<%= ddWorkOrder.ClientID %>', '');
    });

    // vendor name
    $('#ddVender').select2({
        placeholder: 'Select here.....',
        allowClear: false,
    });
    $('#ddVender').on('select2:select', function (e) {
        __doPostBack('<%= ddVender.ClientID %>', '');
    });

    // milestone
    $('#ddMileStone').select2({
        placeholder: 'Select here.....',
        allowClear: false,
    });
    $('#ddMileStone').on('select2:select', function (e) {
        __doPostBack('<%= ddMileStone.ClientID %>', '');
    });

    // abstract no
    $('#ddAbstractNo').select2({
        placeholder: 'Select here.....',
        allowClear: false,
    });
    $('#ddAbstractNo').on('select2:select', function (e) {
        __doPostBack('<%= ddAbstractNo.ClientID %>', '');
    });

    // doc type
    $('#ddDocType').select2({
        placeholder: 'Select here.....',
        allowClear: false,
    });

    // stage
    $('#ddStage').select2({
        placeholder: 'Select here.....',
        allowClear: false,
    });



    // Reinitialize Select2 after UpdatePanel partial postback
    var prm = Sys.WebForms.PageRequestManager.getInstance();

    // Reinitialize Select2 for all dropdowns
    prm.add_endRequest(function () {

        setTimeout(function () {

        }, 0);

        // project
        $('#ddProjectMaster').select2({
            placeholder: 'Select here.....',
            allowClear: false,
        });

        // work order
        $('#ddWorkOrder').select2({
            placeholder: 'Select here.....',
            allowClear: false,
        });
        $('#ddWorkOrder').on('select2:select', function (e) {
            __doPostBack('<%= ddWorkOrder.ClientID %>', '');
        });

        // vendor name
        $('#ddVender').select2({
            placeholder: 'Select here.....',
            allowClear: false,
        });

        // milestone
        $('#ddMileStone').select2({
            placeholder: 'Select here.....',
            allowClear: false,
        });

        // abstract no
        $('#ddAbstractNo').select2({
            placeholder: 'Select here.....',
            allowClear: false,
        });

        // doc type
        $('#ddDocType').select2({
            placeholder: 'Select here.....',
            allowClear: false,
        });

        // stage
        $('#ddStage').select2({
            placeholder: 'Select here.....',
            allowClear: false,
        });
    });
});

//------======={ final submit choosen file grid check }=======------
function validateAndSubmit() {
    var fileUpload = document.getElementById('<%= fileDoc.ClientID %>');
    var gridView = document.getElementById('<%= GridDocument.ClientID %>');

    // Check if at least one file is uploaded and the GridView has rows
    if (fileUpload.files.length > 0 && gridView.rows.length > 0) {
        // Allow the form submission
        return true;
    } else {
        // Display an alert if the conditions are not met
        alert('Please choose at least one file and make sure it is present in the GridView.');
        return false;
    }
}




// clearing the dropdowns is parent dropdown is de-selected

// clearing project dd
function ClearProjectDropdown() {
    // Assuming the "------Select Vendor------" option is at index 0
    $('#ddProjectMaster').prop('selectedIndex', 0);

    // Trigger Select2 to update its display
    $('#ddProjectMaster').trigger('change');
}

// clearing work order dd
function ClearWorkOrderDropdown() {
    // Assuming the "------Select Vendor------" option is at index 0
    $('#ddWorkOrder').prop('selectedIndex', 0);

    // Trigger Select2 to update its display
    $('#ddWorkOrder').trigger('change');
}

// clearing vendor dd
function clearVendorDropdown() {
    // Assuming the "------Select Vendor------" option is at index 0
    $('#ddVender').prop('selectedIndex', 0);

    // Trigger Select2 to update its display
    $('#ddVender').trigger('change');
}

