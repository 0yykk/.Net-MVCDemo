//upload file
function SelectFile(obj) {

    //check if the file has been selected
    if (document.getElementById("uploadFile").value == "") {
        return;
    }

    //reset error msg
    document.getElementById("uploadFileErrorMsg").innerHTML = "&nbsp;";

    //set file name
    document.getElementById("uploadFileName").value = obj.value; // text area

    var fileObj;
    fileObj = document.getElementById("uploadFile").files;

    for (var i = 0; i < fileObj.length; i++) {
        var fileSize = fileObj[0].size;
        var maxSize = 10 * 1024 * 1024; //10M
        if (fileSize > maxSize) {
            document.getElementById("uploadFileErrorMsg").innerHTML = "Invalid file size, the max file size is 10M.";
            return;
        }
    }

    document.getElementById("btnExport").style.display = "none";
}

function AddOTPremiumPopUp() {
    document.getElementById("addModalButton").style.display = "block";
    document.getElementById("addUpload").style.display = "block";
    document.getElementById("addWaitWarning").style.display = "none";
    $("#btnAddOTOermiumInfo").click();
}

function GetTemplate() {
    var UrlExport = $("#TemplateUrl").val();
    window.location.href = UrlExport;
}

function SaveUploadFileForQuery() {
    document.getElementById("btnExport").style.display = "none";
    if (document.getElementById("uploadFileName").value == "No File Choosen" || document.getElementById("uploadFileName").value == "") {
        document.getElementById("lblFailText").innerHTML = "<i class='icon icon-exclamation-circle' style='margin-right:10px;'></i>" + "Please select a file!";
        $("#btnActionFail").click();
        return;
    }
    //waiting picture
    document.getElementById("pictureWait").style.display = "block";
    $("#btnWaitPopup").click();
    //reset error msg
    document.getElementById("uploadFileErrorMsg").innerHTML = "&nbsp;";
    var fileName = $("#uploadFileName").val();

    //upload url 
    var url = $("#uploadFileUrl").val();

    var formdata = new FormData();
    formdata.append("fileName", fileName);

    var fileObj;
    fileObj = document.getElementById("uploadFile").files;

    for (var i = 0; i < fileObj.length; i++) {
        var fileSize = fileObj[0].size;
        var maxSize = 10 * 1024 * 1024; //10M
        if (fileSize > maxSize) {
            document.getElementById("uploadFileErrorMsg").innerHTML = "Invalid file size, the max file size is 10M.";
            document.getElementById("pictureWait").style.display = "none";
            return;
        }
        else {
            formdata.append("file" + 0, fileObj[i]);
        }

    }

    var i = Math.random();
    formdata.append("random", i);

    String.prototype.ToString = function (format) {
        var dateTime = new Date(parseInt(this.substring(6, this.length - 2)));
        format = format.replace("yyyy", dateTime.getFullYear());
        format = format.replace("yy", dateTime.getFullYear().toString().substr(2));
        var month = dateTime.getMonth() + 1;
        if (month < 10) {
            format = format.replace("MM", "0" + month);
        }
        else {
            format = format.replace("MM", month);
        }
        var day = dateTime.getDate();
        if (day < 10) {
            format = format.replace("dd", "0" + day);
        }
        else {
            format = format.replace("dd", day);
        }
        return format;
    }


    $.ajax({
        type: 'POST',
        url: url,
        data: formdata,
        cache:false,
        contentType: false,
        processData: false,
        success: function (result) {
            if (result.IsSuccess && result.Msg != undefined && result.Msg != "") {
                var successIcon = "<i class='icon icon-check-circle' style='margin-right:10px;'></i>";
                document.getElementById("uploadFile").value = "";
                document.getElementById("uploadFileName").value = "";
                var queryItems = result.OTPremiumInfoList;

                var queryContent = "<table class=\"table table-striped table-bordered table-hover\" style=\"width:98%;\"><thead><tr><th>EID</th><th>Store ID</th><th>Start Date</th><th>End Date</th><th>Regular Hours</th><th>OT Hours</th><th>DT Hours</th></tr></thead><tbody id=\"scrollRefresh\">";
                var divQueryResult = document.getElementById("divQueryResult");
                if (queryItems && queryItems.length != 0) {
                    for (var i = 0; i < queryItems.length; i++) {

                        queryContent += "<tr class=\"loadtr\"><td title=\"EID\">" + queryItems[i].EID + "</td><td title=\"Store ID\">" + queryItems[i].StoreID + "</td><td title=\"Start Date\">" + queryItems[i].StartDate.ToString("MM/dd/yyyy") + "</td><td title=\"End Date\">" + queryItems[i].EndDate.ToString("MM/dd/yyyy") + "</td><td title=\"Regular Hours\">" + queryItems[i].RegularHours + "</td><td title=\"OT Hours\">" + queryItems[i].OTHours + "</td><td title=\"DT Hours\">" + queryItems[i].DThours + "</td></tr>";

                    }
                }
                else {
                    queryContent += "<tr><td colspan=\"7\" class=\"text-center\">No Data.</td></tr>";
                }
                divQueryResult.innerHTML = queryContent + "</tbody></table>";
                document.getElementById("divQueryResult").style.display = "block";
                document.getElementById("divAddResult").style.display = "none";

                setTimeout(function () { $("#divWaitPopup").modal('hide'); }, 1000);
            }
            else // fail
            {
                if (result.Flag != undefined && result.Flag != "") {
                    if (result.Flag == "0") {
                        $("#btnGoBackHome").click();
                        return false;
                    }
                }
                else if (result.Msg != undefined && result.Msg != "") {
                    document.getElementById("uploadFile").value = "";
                    document.getElementById("uploadFileName").value = "";
                    var failIcon = "<i class='icon icon-exclamation-circle' style='margin-right:10px;'></i>";
                    document.getElementById("lblFailText").innerHTML = failIcon + result.Msg;

                    setTimeout(function () { $("#divWaitPopup").modal('hide'); $("#btnActionFail").click(); }, 1000);
                    
                }

            }
        }
    });

    document.getElementById("pictureWait").style.display = "none";
}

function SaveUploadFileForAdd() {
    if (document.getElementById("uploadFileName").value == "No File Choosen" || document.getElementById("uploadFileName").value == "") {
        document.getElementById("lblFailText").innerHTML = "<i class='icon icon-exclamation-circle' style='margin-right:10px;'></i>" + "Please select a file!";
        $("#btnActionFail").click();
        return;
    }
    var objPaycode = document.getElementById("DimPaycode");
    var ddlPaycodeVal = objPaycode.options[objPaycode.selectedIndex].value;
    if (ddlPaycodeVal == 0) {
        document.getElementById("lblFailText").innerHTML = "<i class='icon icon-exclamation-circle' style='margin-right:10px;'></i>" + "Please select a paycode!";
        $("#btnActionFail").click();
        return;
    }
    //waiting picture
    document.getElementById("Add_pictureWait").style.display = "block";
    document.getElementById("addModalButton").style.display = "none";
    document.getElementById("addUpload").style.display = "none";
    document.getElementById("addWaitWarning").style.display = "block";

    //reset error msg
    document.getElementById("Add_UploadFileErrorMsg").innerHTML = "&nbsp;";
    var fileName = $("#uploadFileName").val();

    //upload url 
    var url = $("#uploadFileUrlForAdd").val();

    var formdata = new FormData();
    formdata.append("fileName", fileName);

    var fileObj;
    fileObj = document.getElementById("uploadFile").files;

    for (var i = 0; i < fileObj.length; i++) {
        var fileSize = fileObj[0].size;
        var maxSize = 10 * 1024 * 1024; //10M
        if (fileSize > maxSize) {
            document.getElementById("Add_UploadFileErrorMsg").innerHTML = "Invalid file size, the max file size is 10M.";
            document.getElementById("Add_pictureWait").style.display = "none";
            return;
        }
        else {
            formdata.append("file" + 0, fileObj[i]);
        }

    }

    var i = Math.random();
    formdata.append("random", i);

    formdata.append("paycodeID", ddlPaycodeVal);

    String.prototype.ToString = function (format) {
        var dateTime = new Date(parseInt(this.substring(6, this.length - 2)));
        format = format.replace("yyyy", dateTime.getFullYear());
        format = format.replace("yy", dateTime.getFullYear().toString().substr(2));
        var month = dateTime.getMonth() + 1;
        if (month < 10) {
            format = format.replace("MM", "0" + month);
        }
        else {
            format = format.replace("MM", month);
        }
        var day = dateTime.getDate();
        if (day < 10) {
            format = format.replace("dd", "0" + day);
        }
        else {
            format = format.replace("dd", day);
        }
        return format;
    }

    $.ajax({
        type: 'POST',
        url: url,
        data: formdata,
        cache:false,
        contentType: false,
        processData: false,
        success: function (result) {
            if (result.IsSuccess && result.Msg != undefined && result.Msg != "") {
                var successIcon = "<i class='icon icon-check-circle' style='margin-right:10px;'></i>";
                document.getElementById("uploadFile").value = "";
                document.getElementById("uploadFileName").value = "";
                
                var addItems = result.OTPremiumInfoList;

                var addContent = "<table class=\"table table-striped table-bordered table-hover\" style=\"width:98%;\"><thead><tr><th>EID</th><th>Store ID</th><th>Start Date</th><th>End Date</th><th>Regular Hours</th><th>OT Hours</th><th>DT Hours</th><th>Gross Amount</th><th>OT Amount</th><th>DT Amount</th><th>OT Premium</th><th>Comments</th></tr></thead><tbody id=\"scrollRefresh\">";
                var divAddResult = document.getElementById("divAddResult");
                if (addItems && addItems.length != 0) {
                    for (var i = 0; i < addItems.length; i++) {

                        addContent += "<tr class=\"loadtr\"><td title=\"EID\">" + addItems[i].EID + "</td><td title=\"Store ID\">" + addItems[i].StoreID + "</td><td title=\"Start Date\">" + addItems[i].StartDate.ToString("MM/dd/yyyy") + "</td><td title=\"End Date\">" + addItems[i].EndDate.ToString("MM/dd/yyyy") + "</td><td title=\"Regular Hours\">" + addItems[i].RegularHours + "</td><td title=\"OT Hours\">" + addItems[i].OTHours + "</td><td title=\"DT Hours\">" + addItems[i].DThours + "</td><td title=\"Gross Amount\">" + addItems[i].GrossAmount + "</td><td title=\"OT Amount\">" + parseInt(addItems[i].OTamount * 100) / 100 + "</td><td title=\"DT Amount\">" + parseInt(addItems[i].DTamount * 100) / 100 + "</td><td title=\"OT Premium\">" + parseInt(addItems[i].OTPremium * 100) / 100 + "</td><td title=\"Comments\">" + "~" + addItems[i].StartDate.ToString("MM/dd/yy") + "-" + addItems[i].EndDate.ToString("MM/dd/yy") + "~ " + addItems[i].Comments + "</td></tr>";

                    }
                }
                else {
                    addContent += "<tr><td colspan=\"12\" class=\"text-center\">No Data.</td></tr>";
                }
                divAddResult.innerHTML = addContent + "</tbody></table>";
                document.getElementById("divAddResult").style.display = "block";
                document.getElementById("divQueryResult").style.display = "none";
                document.getElementById("btnExport").style.display = "block";

                $("#divAddOTOermiumInfo").modal('hide');
            }
            else // fail
            {
                if (result.Flag != undefined && result.Flag != "") {
                    if (result.Flag == "0") {
                        $("#btnGoBackHome").click();
                        return false;
                    }
                }
                else if (result.Msg != undefined && result.Msg != "") {
                    document.getElementById("uploadFile").value = "";
                    document.getElementById("uploadFileName").value = "";
                    var failIcon = "<i class='icon icon-exclamation-circle' style='margin-right:10px;'></i>";
                    document.getElementById("lblFailText").innerHTML = failIcon + result.Msg;

                    setTimeout(function () { $("#divAddOTOermiumInfo").modal('hide'); $("#btnActionFail").click(); }, 1000);
                }

            }
        }
    });

    document.getElementById("Add_pictureWait").style.display = "none";
}

function ExportToExcel() {
    document.getElementById("ExportErrorMsg").innerText = "";
    var Url = $("#CheckExportDataUrl").val();
    $.ajax({
        type: 'post',
        url: Url,
        traditional: true,
        success: function (result) {
            if (!result.IsSuccess) {
                if (typeof (result.FlagCode) != undefined && result.FlagCode != "") {
                    if (result.FlagCode == "0") {
                        $("#btnGotoHome").click();
                        return false;
                    }
                }
                if (result.Msg != undefined) {
                    document.getElementById("ExportErrorMsg").innerText = result.Msg;
                }
            }
            if (result.IsSuccess) {
                var UrlExport = $("#ExportUrl").val();
                window.location.href = UrlExport;
            }
        }
    });
}

function ExportDownloadedFile(oTPFileLogIdVal) {
    document.getElementById("ExportErrorMsg").innerText = "";
    var Url = $("#CheckExportDataUrl").val();
    var OTPFileLogID = 
    $.ajax({
        type: 'post',
        url: Url,
        data: { oTPFileLogID: oTPFileLogIdVal },
        traditional: true,
        success: function (result) {
            if (!result.IsSuccess) {
                if (typeof (result.FlagCode) != undefined && result.FlagCode != "") {
                    if (result.FlagCode == "0") {
                        $("#btnGotoHome").click();
                        return false;
                    }
                }
                if (result.Msg != undefined) {
                    document.getElementById("ExportErrorMsg").innerText = result.Msg;
                }
            }
            if (result.IsSuccess) {
                var UrlExport = $("#ExportUrl").val();
                window.location.href = UrlExport;
            }
        }
    });
}

function ExportUploadedFile(oTPFileLogIdVal) {
    document.getElementById("ExportErrorMsg").innerText = "";
    var Url = $("#CheckExportDataUrl").val();
    var OTPFileLogID =
    $.ajax({
        type: 'post',
        url: Url,
        data: { oTPFileLogID: oTPFileLogIdVal },
        traditional: true,
        success: function (result) {
            if (!result.IsSuccess) {
                if (typeof (result.FlagCode) != undefined && result.FlagCode != "") {
                    if (result.FlagCode == "0") {
                        $("#btnGotoHome").click();
                        return false;
                    }
                }
                if (result.Msg != undefined) {
                    document.getElementById("ExportErrorMsg").innerText = result.Msg;
                }
            }
            if (result.IsSuccess) {
                var UrlExport = $("#ExportUrlForUploadedFile").val();
                window.location.href = UrlExport;
            }
        }
    });
}