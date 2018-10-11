$( document ).ready(ufillGameIdDropDown());

function ufillGameIdDropDown() {
    // Call Web API to get a list of post
    $.ajax({
        url: 'https://gamersunited.azurewebsites.net/api/games',
        type: 'GET',
        dataType: 'json',
        success: function (games) {
            uonGetGameIdSuccess(games);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function uonGetGameIdSuccess(games) {
    // Iterate over the collection of data
    $.each(games, function (index, game) {
        // Add a row to the post table
        uaddGameIdOption(game);
    });
}

function uaddGameIdOption(game) {
    // Append row to <table>
    $("#updateGameDrop").append(
        ubuildGameIdOption(game));
}

function ubuildGameIdOption(game) {
    var ret =
        "<option value='" + game.gameId + "'>" + game.gameId + "</option>";
    return ret;
}