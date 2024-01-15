

 // Category Drop Down
$(document).ready(function () {

    // category
    $('#ddCat').select2({
        placeholder: 'Select here.....',
        allowClear: false,
    });
    
    // sub category
    $('#ddSubCat').select2({
        placeholder: 'Select here.....',
        allowClear: false,
    });
    $('#ddSubCat').on('select2:select', function (e) {
        __doPostBack('<%= ddSubCat.ClientID %>', '');
    });

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






    // Reinitialize Select2 after UpdatePanel partial postback
    var prm = Sys.WebForms.PageRequestManager.getInstance();

    // Reinitialize Select2 for all dropdowns
    prm.add_endRequest(function () {

        setTimeout(function () {

        }, 0);

        // category
        $('#ddCat').select2({
            placeholder: 'Select here.....',
            allowClear: false,
        });

        // sub category
        $('#ddSubCat').select2({
            placeholder: 'Select here.....',
            allowClear: false,
        });

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
    });
});




// clearing the dropdowns is parent dropdown is de-selected

// clearing sub category dd
function ClearSubCategoryDropdown() {
    // Assuming the "------Select Vendor------" option is at index 0
    $('#ddSubCat').prop('selectedIndex', 0);

    // Trigger Select2 to update its display
    $('#ddSubCat').trigger('change');
}

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

