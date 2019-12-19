function ClickDatePicker(obj) {
    var id = obj.id;
    var imgId = "#" + id.substring(1);
    $(imgId).click();
}

//shared contect number
function MaxInputOnlyNumber(control, maxLength) {
    control.value = control.value.replace(/[^\d]/g, '')
    if (control.value.length > maxLength) {
        control.value = control.value.substring(0, maxLength);
    }
    if (control.value == 0) {
        control.value = "";
    }
}

//Replace Special Value to empty
function ReplaceSepcialValue(object) {
    object.value = object.value.replace(eval("/" + "<" + "/gi"), "").replace(eval("/" + ">" + "/gi"), "");
    return object;
}

//Replace Special Value to empty and control max length
function ReplaceSepcialValueAndMaxInput(object, maxLength) {
    object.value = object.value.replace(eval("/" + "<" + "/gi"), "").replace(eval("/" + ">" + "/gi"), "");
    if (object.value.length > maxLength) {
        // alert("You can input 50 characters in comments at most.");
        object.value = object.value.substring(0, maxLength);
    }
    return object;
}

//for comment commentErrorMsg
function ReplaceSepcialValueAndMaxInputForComment(object, maxLength) {
    object.value = object.value.replace(eval("/" + "<" + "/gi"), "").replace(eval("/" + ">" + "/gi"), "");
    if (object.value.length > maxLength) {
        // alert("You can input 50 characters in comments at most.");
        object.value = object.value.substring(0, maxLength);
    }
    //if (object.value.length > 0) {
    //    document.getElementById("commentErrorMsg").innerHTML = "&nbsp;";
    //    document.getElementById("commentErrorMsg").style.display = "none";
    //}

    return object;
}

function showDivLoading()
{
    $("#div_loading").show();
}

function WaitlistSizeMaxInput(control, maxLength) {
    control.value = control.value.replace(/[^\d]/g, '')
    if (control.value.length > maxLength) {
        control.value = control.value.substring(0, maxLength);
    }
    //if (control.value == 0) {
    //    control.value = "";
    //}
}

//phone and contact number
function CheckInt(control, maxLength) {
    control.value = control.value.replace(/[^\d]/g, '')
    if (control.value.length > maxLength) {
        control.value = control.value.substring(0, maxLength);
    }
}
