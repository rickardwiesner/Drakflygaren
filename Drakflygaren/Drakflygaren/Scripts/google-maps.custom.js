﻿jQuery(function ($) {
    // Asynchronously Load the map API
    var script = document.createElement('script');
    script.src = "//maps.googleapis.com/maps/api/js?key=AIzaSyDOP-hCNyiygNNOURvqXC4A9VssSMieFGo&sensor=false&callback=initialize";
    document.body.appendChild(script);
});

function initialize() {
    var map;
    var bounds = new google.maps.LatLngBounds();
    var mapOptions = {
        mapTypeId: 'roadmap'
    };

    // Display a map on the page
    map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
    map.setTilt(45);

    // Multiple Markers
    var markers = [];

    var locations = $('.location-row');

    locations.each(function () {
        var location = $(this);
        var lat = location.find('.cityLat').val();
        var long = location.find('.cityLong').val();
        var name = location.find('.locationName').html();
        var city = location.find('.cityName').html();
        markers.push([name, lat, long, city]);
    });

    // Display multiple markers on a map
    var infoWindow = new google.maps.InfoWindow(), marker, i;

    // Loop through our array of markers & place each one on the map
    for (i = 0; i < markers.length; i++) {
        var position = new google.maps.LatLng(markers[i][1], markers[i][2]);
        bounds.extend(position);
        marker = new google.maps.Marker({
            position: position,
            map: map,
            title: markers[i][0],
            city: markers[i][3]
        });

        // Allow each marker to have an info window
        google.maps.event.addListener(marker, 'click', (function (marker, i) {
            var temp;
            var wind;
            var direction;

            $.simpleWeather({
                location: marker.getPosition(),
                unit: 'c',
                success: function (weather) {
                    temp = weather.temp;
                    wind = weather.wind.speed;
                    direction = weather.wind.direction;
                },
                error: function (error) {
                    console.log(error);
                }
            });

            return function () {
                infoWindow.setContent('<div class="info_content">' +
            '<h3>' + marker.get('title') + '</h3>' +
            '<p>Stad: ' + marker.get('city') + '</p>' +
            '<p>Temperatur: ' + temp + '</p>' +
            '<p>Vind: ' + wind + ' - Riktning: ' + direction + '</p>' +
            '</div>');
                infoWindow.open(map, marker);
            }
        })(marker, i));

        // Automatically center the map fitting all markers on the screen
        map.fitBounds(bounds);
    }

    // Override our map zoom level once our fitBounds function runs (Make sure it only runs once)
    var boundsListener = google.maps.event.addListener((map), 'bounds_changed', function (event) {
        //this.setZoom(14);
        google.maps.event.removeListener(boundsListener);
    });
}