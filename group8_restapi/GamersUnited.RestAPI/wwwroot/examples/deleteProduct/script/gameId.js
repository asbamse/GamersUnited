$( document ).ready(dfillGameIdDropDown());

function dfillGameIdDropDown() {
    // Call Web API to get a list of post
    $.ajax({
        url: 'https://gamersunited.azurewebsites.net/api/games',
        type: 'GET',
        dataType: 'json',
        success: function (games) {
            donGetGameIdSuccess(games);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function donGetGameIdSuccess(games) {
    // Iterate over the collection of data
    $.each(games, function (index, game) {
        // Add a row to the post table
        daddGameIdOption(game);
    });
}

function daddGameIdOption(game) {
    // Append row to <table>
    $("#deleteGameId").append(
        dbuildGameIdOption(game));
}

function dbuildGameIdOption(game) {
    var ret =
        "<option value='" + game.gameId + "'>" + game.gameId + "</option>";
    return ret;
}