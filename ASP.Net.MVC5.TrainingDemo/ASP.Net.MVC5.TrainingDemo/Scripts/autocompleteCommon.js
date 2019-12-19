/// <reference path="jquery-1.7.1-vsdoc.js" />
/// <reference path="jquery.form.js" />


// apply site global js
$(function () {
    $autocomplete();
});

$autocomplete = function () {
    $(".autocomplete").each(function () {
        var $ac = $(this);
        var date = new Date();
        var url = $ac.attr("targeturl") + "?tempData =" + date.getTime();
        $ac.autocomplete({
            source: url,
            minLength: 1,
            cacheLength: 1,
            matchSubset: false,
            messages: {
                noResults: "",
                results: function () { }
            }
        });
    });
}
