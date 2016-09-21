$(document).ready(function () {
    $('.like-topic-button').click(function () {
        var topicId = $(this).parents('tr').attr('data-id');
        $.ajax({
            url: '/Topic/TopicLike',
            data: { 'topicId': topicId },
            type: 'POST',
            error: function (jqXHR, textStatus, errorThrown) {
                console.log('Something went wrong!\nStatus: ' + textStatus + "\nError: " + errorThrown);

            },
            success: function (data) {
                var icon = $('[data-id=' + topicId + '] .like-topic-button i.fa');
                icon.toggleClass('fa-thumbs-up fa-thumbs-o-up')

                var element = $('[data-id=' + topicId + '] .topic-likes-count');
                element.html(data);
            }
        });
    });
});
