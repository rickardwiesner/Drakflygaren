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
    $('.join-event-button').click(function () {
        var eventId = $(this).parents('tr').attr('data-id');
        $.ajax({
            url: '/Events/EventJoin',
            data: { 'eventId': eventId },
            type: 'POST',
            error: function (jqXHR, textStatus, errorThrown) {
                console.log('Something went wrong!\nStatus: ' + textStatus + "\nError: " + errorThrown);
            },
            success: function (data) {
                var icon = $('[data-id=' + eventId + '] .join-event-button i.fa');
                icon.toggleClass('fa-toggle-on fa-toggle-off')

                var element = $('[data-id=' + eventId + '] .event-participants-count');
                element.html(data);
            }
        });
    });
});