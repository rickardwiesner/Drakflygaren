(function () {
    var listAndMap = $('#locations-list,#locations-map');
    $('#toggle-between-list-and-map').on('click', function () {
        listAndMap.toggleClass('hide-content');
    });

    $('.toggle-favorite').on('click', function () {
        var locationId = $(this).parents('tr').attr('data-id');
        $.ajax({
            url: '/Locations/ToggleFavorite',
            type: 'POST',
            data: { 'locationId': locationId },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log('Something went wrong!\nStatus: ' + textStatus + "\nError: " + errorThrown);
            },
            success: function (data) {
                var icon = $('[data-id=' + locationId + '] .toggle-favorite i.fa');
                icon.toggleClass("fa-heart fa-heart-o");
                $("#favorite-locations").html(data);
            }
        });
    });

    $('.rate-location').on('click', function () {
        var locationId = $(this).parents('tr').attr('data-id');
        var rating = $(this).attr('data-rating');
        $.ajax({
            url: '/Locations/RateLocation',
            type: 'POST',
            data: {
                'locationId': locationId,
                'rating': rating
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log('Something went wrong!\nStatus: ' + textStatus + "\nError: " + errorThrown);
            },
            success: function (data) {
                var elements = $('[data-id=' + locationId + '] .rate-location');
                var currentStars = elements.children('i.fa-star');
                if (currentStars.length == rating) {
                    currentStars.each(function () {
                        var currentStar = $(this);
                        currentStar.toggleClass('fa-star fa-star-o');
                    });
                }
                else {
                    elements.each(function () {
                        var element = $(this);
                        var icon = element.children('i.fa')
                        if (element.attr('data-rating') <= rating) {
                            if (icon.hasClass('fa-star-o')) {
                                icon.toggleClass('fa-star fa-star-o')
                            }
                        }
                        else {
                            if (icon.hasClass('fa-star')) {
                                icon.toggleClass('fa-star fa-star-o')
                            }
                        }
                    });
                }
                $('[data-id=' + locationId + '] .location-rating').html(data);
            }
        });
    });
})();