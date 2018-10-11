$( document ).ready(fillGameGenreDropDown());

function fillGameGenreDropDown() {
    // Call Web API to get a list of post
    $.ajax({
        url: 'https://gamersunited.azurewebsites.net/api/gameGenres',
        type: 'GET',
        dataType: 'json',
        success: function (gameGenres) {
            onGetGameGenreSuccess(gameGenres);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function onGetGameGenreSuccess(gameGenres) {
    // Iterate over the collection of data
    $.each(gameGenres, function (index, gameGenre) {
        // Add a row to the post table
        addGameGenreOption(gameGenre);
    });
}

function addGameGenreOption(gameGenre) {
    // Append row to <table>
    $("#gameGenreDrop").append(
        buildGameGenreOption(gameGenre));
}

function buildGameGenreOption(gameGenre) {
    var ret =
        "<option value='" + gameGenre.gameGenreId + "'>" + gameGenre.name + "</option>";
    return ret;
}