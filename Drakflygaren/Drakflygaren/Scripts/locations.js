(function () {
    var listAndMap = $('#locations-list,#locations-map');
    $('#toggle-between-list-and-map').on('click', function () {
        listAndMap.toggleClass('hide-content');
    });
})();