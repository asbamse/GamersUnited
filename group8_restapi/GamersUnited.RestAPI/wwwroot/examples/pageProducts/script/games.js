$( document ).ready(listGames());

function listGames() {
    var $page = $_GET("page");
    var $limit = $_GET("limit");
    var $sortBy = $_GET("sortBy");
    var $sortOrder = $_GET("sortOrder");

    if($page == null)
    {
        $page = 1;
    }

    if($limit == null)
    {
        $limit = 1;
    }

    if($sortBy != null)
    {
        $sortBy = '&sortBy=' + $sortBy;
    }
    else
    {
        $sortBy = '';
    }

    if($sortOrder != null)
    {
        $sortOrder = '&sortOrder=' + $sortOrder;
    }
    else
    {
        $sortOrder = '';
    }

    // Call Web API to get a list of post
    $.ajax({
        url: 'https://gamersunited.azurewebsites.net/api/games/filtered?page=' + $page + '&limit=' + $limit + $sortBy + $sortOrder,
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
        "<td>" + game.gameId + "</td>" +
        "<td>" + game.product.name + "</td>" +
        "<td>" + game.product.category.name + "</td>" +
        "<td>" + game.genre.name + "</td>" +
        "<td>" + game.product.price + "</td>" +
        "<td><img src='" + game.product.imageUrl + "' width='100'></td>" +
        "<td>" + game.product.description + "</td>" +
        "</tr>";
    return ret;
}

function $_GET(param) {
    var vars = {};
    window.location.href.replace( location.hash, '' ).replace(
        /[?&]+([^=&]+)=?([^&]*)?/gi, // regexp
        function( m, key, value ) { // callback
            vars[key] = value !== undefined ? value : '';
        }
    );

    if ( param ) {
        return vars[param] ? vars[param] : null;
    }
    return vars;
}