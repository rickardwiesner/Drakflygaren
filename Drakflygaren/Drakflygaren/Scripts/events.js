$(document).ready(function () {
    $('.like-event-button').click(function () {
        var eventId = $(this).parents('tr').attr('data-id');
        $.ajax({
            url: '/Events/EventLike',
            data: { 'eventId': eventId },
            type: 'POST',
            error: function (jqXHR, textStatus, errorThrown) {
                console.log('Something went wrong!\nStatus: ' + textStatus + "\nError: " + errorThrown);
            },
            success: function (data) {
                var icon = $('[data-id=' + eventId + '] .like-event-button i.fa');
                icon.toggleClass('fa-thumbs-up fa-thumbs-o-up')

                var element = $('[data-id=' + eventId + '] .event-likes-count');
                element.html(data);
            }
        });
    });
});