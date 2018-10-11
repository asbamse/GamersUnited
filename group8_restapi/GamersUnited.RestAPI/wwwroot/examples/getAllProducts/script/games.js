var page;
var limit;
var isBlocked;

$( document ).ready(function() {
    page = 1;
    limit = 2;
    isBlocked = false;
    listGames();
});

$(window).scroll(function() {
    if($(window).scrollTop() + $(window).height() == $(document).height()) {
        if(!isBlocked) {
            listGames();
        }
    }
});

$("#loadMore").click( function() {
        if(!isBlocked) {
            listGames();
        }
    }
);

function listGames() {
    isBlocked = true;
    // Call Web API to get a list of post
    $.ajax({
        url: 'https://gamersunited.azurewebsites.net/api/games/filtered?page=' + page + '&limit=' + limit,
        type: 'GET',
        dataType: 'json',
        success: function (games) {
            onGetGamesSuccess(games);
            getCount();
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function getCount() {
    // Call Web API to get a list of post
    $.ajax({
        url: 'https://gamersunited.azurewebsites.net/api/games/count',
        type: 'GET',
        dataType: 'json',
        success: function (gameCount) {
            removeButton(gameCount);
            page++;
            isBlocked = false;
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function removeButton(count) {
    if(count < (page * limit)) {
        $("#loadMore").remove();
    }
}

function onGetGamesSuccess(games) {
    if ($("#gamesTable tbody").length == 0) {
        $("#gamesTable").append("<tbody></tbody>");
    }
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