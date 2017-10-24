$(function () {

    var checkboxes = $('input[type=checkbox]');
    checkboxes.each(function (i, e) {

        //$(e).attr('checked', 'checked');
        $(e).prop('checked', true);
    })
})



