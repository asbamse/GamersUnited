$( document ).ready(fillGameIdDropDown());

function fillGameIdDropDown() {
    // Call Web API to get a list of post
    $.ajax({
        url: 'https://gamersunited.azurewebsites.net/api/games',
        type: 'GET',
        dataType: 'json',
        success: function (games) {
            onGetGameIdSuccess(games);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function onGetGameIdSuccess(games) {
    // Iterate over the collection of data
    $.each(games, function (index, game) {
        // Add a row to the post table
        addGameIdOption(game);
    });
}

function addGameIdOption(game) {
    // Append row to <table>
    $("#gameDrop").append(
        buildGameIdOption(game));
}

function buildGameIdOption(game) {
    var ret =
        "<option value='" + game.gameId + "'>" + game.gameId + "</option>";
    return ret;
}