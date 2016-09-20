var locations = $('.location-row');

locations.each(function () {
    var location = $(this);
    var long = location.find('.cityLong').val();
    var lat = location.find('.cityLat').val();
    var locationId = location.attr('data-id');
    loadWeather(lat + ',' + long, locationId);
});


function loadWeather(location, locationId) {
    $.simpleWeather({
        location: location,
        unit: 'c',
        success: function (weather) {
            var row = '[data-id=' + locationId + ']';
            $(row + ' .cityTemp').html(weather.temp);
            $(row + ' .cityWind').html((weather.wind.speed / 3.6).toFixed(2));
            $(row + ' .direction').addClass(getRotationClass(weather.wind.direction));
            $(row + ' .weatherIcon').attr('src', weather.thumbnail);
        },
        error: function (error) {
            console.log(error);
        }
    });
}

function getRotationClass(direction) {
    var rotationClass = 'fa-rotate-';
    
    if (direction === 'E' || direction === 'ESE') {
        rotationClass += '45';
    }

    else if (direction === 'SE' || direction === 'SSE') {
        rotationClass += '90';
    }

    else if (direction === 'S' || direction === 'SSW') {
        rotationClass += '135';
    }

    else if (direction === 'SW' || direction === 'WSW') {
        rotationClass += '180';
    }

    else if (direction === 'W' || direction === 'WNW') {
        rotationClass += '215';
    }

    else if (direction === 'NW' || direction === 'NNW') {
        rotationClass += '270';
    }

    else if (direction === 'N' || direction === 'NNE') {
        rotationClass += '315';
    }

    return rotationClass;
}






