//function initMap() {

//    var locations = $('.map-item');
//    //var bounds = new google.maps.LatLngBounds;

//    locations.each(function () {
//        var location = $(this);
//        var long = location.find('.cityLong').val();
//        var lat = location.find('.cityLat').val();
//        var locationId = location.attr('data-id');
//        console.log(long, lat)
//    });

//    var origins = { lat: 57.71, lng: 11.97 };
//    var destinations = { lat: 50.087, lng: 14.421 };

//    var service = new google.maps.DistanceMatrixService;
//    service.getDistanceMatrix({
//        origins: [origins],
//        destinations: [destinations],
//        travelMode: 'DRIVING',
//        unitSystem: google.maps.UnitSystem.METRIC,
//        avoidHighways: false,
//        avoidTolls: false
//    },
//    function (response, status) {
//        if (status !== 'OK') {
//            alert('Error was: ' + status);
//        }
//        else {
//            var originList = response.originAddresses;
//            var destinationList = response.destinationAddresses;
//            var outputDiv = document.getElementById('result-distance');
//            outputDiv.innerHTML = '';

//            for (var i = 0; i < originList.length; i++) {
//                var results = response.rows[i].elements;
//                for (var j = 0; j < results.length; j++) {
//                    outputDiv.innerHTML +=
//                        results[j].distance.text;
//                }
//            }
//        }
//    });
//}