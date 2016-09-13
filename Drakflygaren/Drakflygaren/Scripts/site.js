(function () {
    $('.toggle-favorite').on('click', function () {
        var id = $(this).attr('id'); 
        $.ajax({
            url: '/Locations/ToggleFavorite',
            type: 'POST',
            data: { 'locationId': id },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log('Something went wrong!\nStatus: ' + textStatus + "\nError: " + errorThrown);
            },
            success: function () {
                var icon = $('#' + id + '.toggle-favorite' + ' i.fa');
                icon.toggleClass("fa-star fa-star-o");
            }
        });
    });
})();

