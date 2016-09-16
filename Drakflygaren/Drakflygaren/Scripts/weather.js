//Reads each place from table in Index View & returns its respective data

//function that takes a location & on success fills requested data
function loadWeather(location) {
    $.simpleWeather({
        location: location,
        unit: 'c',
        success: function (weather) {
            $('.cityTemp').html(weather.temp);
            $('.cityWind').html(weather.wind.speed);
            $('.direction').html(weather.wind.direction);
        },
        error: function (error) {
            $("#cityTemp").html('<p>' + error + '</p>');
        }
    });
}

//loading all cities from foreach in index view in a list
var list = document.getElementsByClassName('cityName');

//iterating all cities one by one to load their data
for (var i = 0; i < list.length; i++)
{
    loadWeather(list[i].innerHTML);
}




