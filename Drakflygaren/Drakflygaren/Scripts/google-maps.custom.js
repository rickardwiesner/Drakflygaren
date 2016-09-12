// This example displays an address form, using the autocomplete feature
// of the Google Places API to help users fill in the information.
$(document).ready(function () {
	$("#autocomplete").on('focus', function () {
		geolocate();
	});

	var placeSearch, autocomplete;
	var componentForm = {
		//street_number: 'short_name',
		route: 'long_name',
		locality: 'long_name'
		//administrative_area_level_1: 'short_name',
		//country: 'long_name',
		//postal_code: 'short_name'
	};

	function initialize() {
		// Create the autocomplete object, restricting the search
		// to geographical location types.
		autocomplete = new google.maps.places.Autocomplete(
        document.getElementById('autocomplete'), { types: ['geocode'] });
		// When the user selects an address from the dropdown,
		// populate the address fields in the form.
		google.maps.event.addListener(autocomplete, 'place_changed', function () {
			fillInAddress();
		});
	}

	// [START region_fillform]
	function fillInAddress() {
		// Get the place details from the autocomplete object.
		var place = autocomplete.getPlace();

		document.getElementById("latitude").value = place.geometry.location.lat();
		document.getElementById("longitude").value = place.geometry.location.lng();

		for (var component in componentForm) {
			document.getElementById(component).value = '';
			document.getElementById(component).disabled = false;
		}

		// Get each component of the address from the place details
		// and fill the corresponding field on the form.
		for (var i = 0; i < place.address_components.length; i++) {
			var addressType = place.address_components[i].types[0];
			if (componentForm[addressType]) {
				var val = place.address_components[i][componentForm[addressType]];
				document.getElementById(addressType).value = val;
			}
		}
	}
	// [END region_fillform]

	// [START region_geolocation]
	// Bias the autocomplete object to the user's geographical location,
	// as supplied by the browser's 'navigator.geolocation' object.
	function geolocate() {
		if (navigator.geolocation) {
			navigator.geolocation.getCurrentPosition(function (position) {
				var geolocation = new google.maps.LatLng(
                position.coords.latitude, position.coords.longitude);

				var latitude = position.coords.latitude;
				var longitude = position.coords.longitude;
				document.getElementById("latitude").value = latitude;
				document.getElementById("longitude").value = longitude;

				autocomplete.setBounds(new google.maps.LatLngBounds(geolocation, geolocation));
			});
		}

	}

	initialize();
	// [END region_geolocation]

	//var myCenter = new google.maps.LatLng(51.508742, -0.120850);

	//function initializeMap() {

	//	var myCenter = new google.maps.LatLng(51.508742, -0.120850);

	//	var mapProp = {
	//		center: myCenter,
	//		zoom: 5,
	//		mapTypeId: google.maps.MapTypeId.ROADMAP
	//	};

	//	var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);


	//}

	//google.maps.event.addDomListener(window, 'load', initializeMap);

});