$( document ).ready(ufillGameGenreDropDown());

function ufillGameGenreDropDown() {
    // Call Web API to get a list of post
    $.ajax({
        url: 'https://gamersunited.azurewebsites.net/api/gameGenres',
        type: 'GET',
        dataType: 'json',
        success: function (gameGenres) {
            uonGetGameGenreSuccess(gameGenres);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function uonGetGameGenreSuccess(gameGenres) {
    // Iterate over the collection of data
    $.each(gameGenres, function (index, gameGenre) {
        // Add a row to the post table
        uaddGameGenreOption(gameGenre);
    });
}

function uaddGameGenreOption(gameGenre) {
    // Append row to <table>
    $("#updateGameGenreDrop").append(
        ubuildGameGenreOption(gameGenre));
}

function ubuildGameGenreOption(gameGenre) {
    var ret =
        "<option value='" + gameGenre.gameGenreId + "'>" + gameGenre.name + "</option>";
    return ret;
}