function DatableSearchColumnWise(tableClassName) {

    $('.tdFilter').each(function () {
        $(this).html('<input type="text" maxlength="9" class="form-control" placeholder="Search.." />');
    });

    var table = $('.' + tableClassName).DataTable();
    table.columns().eq(0).each(function (colIdx) {
        $('input', $('.filters td')[colIdx]).bind('keyup', function () {
            var coltext = this.value; // typed value in the search column
            var colindex = colIdx; // column index
            //  alert(colIdx);
            // alert(coltext);
            // delay(function () {
            table
                .column(colindex)
                .search(coltext)
                .draw();
            // }, 500);
        });
    });

}

function HasValue(checkVal) {
    if (checkVal == null || checkVal.trim().length == 0) {
        return false;
    }
    else {
        return true;
    }
};

function SetBorderColorOfErrorField(inputElement) {
    if ($(inputElement).hasClass("selectpicker")) {
        $(inputElement).closest('div').find('.dropdown-toggle').addClass("errorInputBorderColor");
    }
    else {
        $(inputElement).addClass("errorInputBorderColor");
    }
}
function RemoveErrorFieldBorderColor(inputElement) {
    $(inputElement).removeClass("errorInputBorderColor");
}
function RemoveAllFieldErrorBorderColor() {
    $('.errorInputBorderColor').each(function () {
        $(this).removeClass("errorInputBorderColor");
    });
}
function CheckOnlyIsRequired(element, Fieldname) {
    RemoveErrorFieldBorderColor(element);
    var inputValue = $(element).val();
    if (inputValue == null) {
        SetBorderColorOfErrorField(element);
        return false;
    }
    else if (!HasValue(inputValue.trim())) {
        SetBorderColorOfErrorField(element);
        return false;
    }
    return true;
}
function HasValueOnInputField(inputValue) {
    if (inputValue == null || inputValue.trim().length == 0) {
        return false;
    }
    else {
        return true;
    }
}
function AllowOnlyNumber(evt) {
    var iKeyCode = (evt.which) ? evt.which : evt.keyCode;
    if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
        return false;
    return true;
}
function CommonErrorAlert(errorMessage) {
    $('#CommonErrorAlertModel').modal('show');
    $('#txtCommonErrorMsg').text(errorMessage);
}
function CommonSuccessAlert(Message) {
    $('#CommonSuccessAlertModel').modal('show');
    $('#txtcommonSuccessMsg').text(Message);
}

function CurrentdateTime(){
    var d = new Date();
    d = new Date();
    utc = d.getTime() + (d.getTimezoneOffset() * 60000);
    nd = new Date(utc + (3600000 * +5.5));
    var ist = nd.toLocaleString();

    return new Date(ist);
}