$( document ).ready(listGames());

function listGames() {
    // Call Web API to get a list of post
    $.ajax({
        url: 'https://gamersunited.azurewebsites.net/api/games',
        type: 'GET',
        dataType: 'json',
        success: function (games) {
            onGetGamesSuccess(games);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function onGetGamesSuccess(games) {
    if ($("#gamesTable tbody").length == 0) {
        $("#gamesTable").append("<tbody></tbody>");
    }
    $("#gamesTable tbody").empty();
    // Iterate over the collection of data
    $.each(games, function (index, game) {
        // Add a row to the post table
        addGameRow(game);
    });
}

function addGameRow(game) {
    // Check if <tbody> tag exists, add one if not
    // Append row to <table>
    $("#gamesTable tbody").append(
        buildGameRow(game));
}

function buildGameRow(game) {
    var ret =
        "<tr>" +
        "<td>" + game.product.productId + "</td>" +
        "<td>" + game.product.name + "</td>" +
        "<td>" + game.product.category.name + "</td>" +
        "<td>" + game.genre.name + "</td>" +
        "<td>" + game.product.price + "</td>" +
        "<td><img src='" + game.product.imageUrl + "'></td>" +
        "<td>" + game.product.description + "</td>" +
        "</tr>";
    return ret;
}

function handleException(request, message, error) {
    var msg = "";
    msg += "Code: " + request.status + "\n";
    msg += "Text: " + request.statusText + "\n";
    if (request.responseJSON != null) {
        msg += "Message" +
            request.responseJSON.Message + "\n";
    }
    alert(msg);
}