jQuery(function ($) {
    // Asynchronously Load the map API
    var script = document.createElement('script');
    script.src = "//maps.googleapis.com/maps/api/js?key=AIzaSyDOP-hCNyiygNNOURvqXC4A9VssSMieFGo&sensor=false&callback=initialize";
    document.body.appendChild(script);
});

function initialize() {
    var map;

    // Multiple Markers
    var markers = [];

    var locations = $('.map-item');

    locations.each(function () {
        var location = $(this);
        var lat = location.find('.cityLat').val();
        var long = location.find('.cityLong').val();
        var name = location.find('.locationName').html();
        var city = location.find('.cityName').html();
        markers.push([name, lat, long, city]);

        //Travel
        var latLng = new google.maps.LatLng(lat, long);
        
        var origins = { lat: 57.71, lng: 11.97 };
        var destinations = { lat: latLng.lat(), lng: latLng.lng() };

        var service = new google.maps.DistanceMatrixService;
        service.getDistanceMatrix({
            origins: [origins],
            destinations: [destinations],
            travelMode: 'DRIVING',
            unitSystem: google.maps.UnitSystem.METRIC,
            avoidHighways: false,
            avoidTolls: false
        },
        function (response, status) {
            if (status !== 'OK') {
                alert('Error was: ' + status);
            }
            else {
                var originList = response.originAddresses;
                var destinationList = response.destinationAddresses;

                for (var i = 0; i < originList.length; i++) {
                    var results = response.rows[i].elements;
                    for (var j = 0; j < results.length; j++) {
                        $('[data-id=' + location.attr('data-id') + ']' + ' .distance').html(results[j].distance.text);
                    }
                }
            }
        });

    });

    //var bounds = new google.maps.LatLngBounds();
    var mapOptions = {
        mapTypeId: 'roadmap',
        center: new google.maps.LatLng(markers[0][1], markers[0][2]),
        zoom: 10
    };

    // Display a map on the page
    map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
    map.setTilt(45);

    // Display multiple markers on a map
    var infoWindow = new google.maps.InfoWindow(), marker, i;


    // Loop through our array of markers & place each one on the map
    for (i = 0; i < markers.length; i++) {
        var position = new google.maps.LatLng(markers[i][1], markers[i][2]);
        //bounds.extend(position);
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
            var html;

            $.simpleWeather({
                location: marker.getPosition(),
                unit: 'c',
                success: function (weather) {
                    temp = weather.temp;
                    wind = weather.wind.speed;
                    direction = weather.wind.direction;
                    
                    html = '<div class="info_content text-center">' +
                                '<h2>' + marker.get('title') + '</h2>' +
                                '<h4>' + marker.get('city') + '</h4>' +
                                '<div class="col-md-6 col-md-offset-3"><img class="weather-image" src="' + weather.image + '" alt="weather" /></div>' +
                                '<div class="col-md-4">' +
                                    '<h5>' + temp + '&deg;' + weather.units.temp + '</h5>' +
                                '</div>' +
                                '<div class="col-md-4">' +
                                    '<span class="fa fa-2x fa-location-arrow ' + getRotationClass(weather.wind.direction) + '"></span>' +
                                '</div>' +
                                '<div class="col-md-4">' +
                                    '<h5>' + (weather.wind.speed / 3.6).toFixed(2) + ' m/s</h5>' +
                                '</div></div>' 

                    //html = '<div class="info_content weather"><h2><i class="icon-' + weather.code + '"></i> ' + weather.temp + '&deg;' + weather.units.temp + '</h2>';
                    //html += '<ul><li>' + weather.city + ', ' + weather.region + '</li>';
                    //html += '<li class="currently">' + weather.currently + '</li>';
                    //html += '<li>' + weather.wind.direction + ' ' + weather.wind.speed + ' ' + weather.units.speed + '</li></ul></div>';
                },
                error: function (error) {
                    console.log(error);
                }
            });

            return function () {
            //    infoWindow.setContent('<div class="info_content">' +
            //'<h3>' + marker.get('title') + '</h3>' +
            //'<p>Stad: ' + marker.get('city') + '</p>' +
            //'<p>Temperatur: ' + temp + '</p>' +
            //'<p>Vind: ' + wind + ' - Riktning: ' + direction + '</p>' +
                //'</div>');



                infoWindow.setContent(html);
                infoWindow.open(map, marker);
            }
        })(marker, i));

        // Automatically center the map fitting all markers on the screen
        //map.fitBounds(bounds);
    }

    // Override our map zoom level once our fitBounds function runs (Make sure it only runs once)
    var boundsListener = google.maps.event.addListener((map), 'bounds_changed', function (event) {
        //this.setZoom(14);
        google.maps.event.removeListener(boundsListener);
    });
}