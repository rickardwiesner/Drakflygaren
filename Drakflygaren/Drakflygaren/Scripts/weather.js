var locations = $('.location-row');

locations.each(function () {
    var location = $(this);
    var long = location.find('.cityLong').val();
    var lat = location.find('.cityLat').val();
    var locationId = $(this).attr('data-id');
    loadWeather(lat + ',' + long, locationId);
});


function loadWeather(location, locationId) {
    $.simpleWeather({
        location: location,
        unit: 'c',
        success: function (weather) {
            $('[data-id=' + locationId + '] .cityTemp').html(weather.temp);
            $('[data-id=' + locationId + '] .cityWind').html(weather.wind.speed);
            $('[data-id=' + locationId + '] .direction').html(weather.wind.direction);
        },
        error: function (error) {
            console.log(error);
        }
    });
}





