$(function () {
    var placeholderElement = $('#modal-placeholder');

    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        var search = $(this).data('search');
        var themoviedbid = $(this).data('movieid');
        var newname = url + search + themoviedbid;
        $.get(newname).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        });
    });

    //placeholderElement.on('click', '[data-save="modal"]', function (event) {
    //    event.preventDefault();

    //    var form = $(this).parents('.modal').find('form');
    //    var actionUrl = form.attr('action');
    //    var dataToSend = form.serialize();

    //    $.post(actionUrl, dataToSend).done(function (data) {
    //        var newBody = $('.modal-body', data);
    //        placeholderElement.find('.modal-body').replaceWith(newBody);

            
    //    });
    //});
});
