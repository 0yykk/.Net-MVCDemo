$(document).ready(function(){
    $("#cartSubmit").click(function (event) {
        debugger;
        var tr = $("#cartTable tr");
        var result = [];
        for (var i = 0; i < tr.length; i++) {
            var tds = $(tr[i]).find("td");
            if (tds.length > 0) {
                result.push({
                    "AlbumId": $(tds[1]).find("label").text(),
                    "Title": $(tds[2]).find("label").text(),
                    "ArtistName": $(tds[3]).find("label").text(),
                    "GenreName": $(tds[4]).find("label").text(),
                    "Price": $(tds[5]).find("label").text()
                })
            }
        }

        $.ajax({
            type: "POST",
            url: "../Cart/OrderRequest",
            contentType: "application/json;charset=UTF-8",
            data: JSON.stringify(result),
            dataType: "json",
            contentType: "application/json",
            success: function (data) {

            }

        });
    })
    })