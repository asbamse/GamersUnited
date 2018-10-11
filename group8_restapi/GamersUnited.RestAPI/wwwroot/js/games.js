var page;
var limit;
var isBlocked;
var isLeft;
var isSecond;
var columnNumber;
var containerNumber;

$( document ).ready(function() {
    page = 1;
    limit = 8;
    isBlocked = false;
    isLeft = true;
    isSecond = false;
    columnNumber = 1;
    containerNumber = 1;
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
    // Iterate over the collection of data
    $.each(games, function (index, game) {
        buildGameRow(game);
    });
}

function buildGameRow(game) {
    if(isLeft && !isSecond)
    {
        $("#product-store-view").append("<!--- ////////////////////////////////// Start of Column ////////////////////////////////// --->\n" +
            "<div class='product-column-list' id='column" + columnNumber + "'>\n" +
            "</div>\n" +
            "        <!--- ////////////////////////////////// End of Column ////////////////////////////////// --->\n" +
            "\n" +
            "<!--- ////////////////////////////////// Space ////////////////////////////////// --->\n" +
            "              <div class=\"product-column-space-white light_gradient shadow\">\n" +
            "              </div>\n" +
            "              <!--- ////////////////////////////////// End of Space ////////////////////////////////// --->");
    }
    if(isLeft)
    {
        $("#column" + columnNumber).append("<!--- ////////////////////////////////// Start of Container ////////////////////////////////// --->\n" +
            "        <!-- Product Container -->\n" +
            "        <div class='product-container' id='container" + containerNumber + "'>\n" +
            "        </div>\n" +
            "        <!--- ////////////////////////////////// End of Container ////////////////////////////////// --->\n");

        $("#container" + containerNumber).append("<!--- ////////////////////////////////// Start of Half Card ////////////////////////////////// --->\n" +
            "        <!-- Product Left Card -->\n" +
            "        <div class=\"product-listed-body-half-card left\" onclick='location.href=\"product.html?id=" + game.gameId + "\";'>\n" +
            "\n" +
            "        <div class=\"product-listed-image\">\n" +
            "        <img src=\"" + game.product.imageUrl + "\" alt=\"\">\n" +
            "        </div>\n" +
            "        <div class=\"product-listed-cover light_gradient shadow\">\n" +
            "\n" +
            "        <!-- Product-Title -->\n" +
            "        <p class=\"product-listed-title\">" + game.product.name + "</p>\n" +
            "        <!-- Genres -->\n" +
            "        <span>\n" +
            "        <p class=\"product-listed-genre\">\n" +
            "        " + game.genre.name + "\n" +
            "        </p>\n" +
            "        </span>\n" +
            "\n" +
            "        </div>\n" +
            "        </div>\n" +
            "        <!--- ////////////////////////////////// End of Half Card ////////////////////////////////// --->");
    }
    else
    {
        $("#container" + containerNumber).append("<!--- ////////////////////////////////// Start of Half Card ////////////////////////////////// --->\n" +
            "        <!-- Product Right Card -->\n" +
            "        <div class=\"product-listed-body-half-card right\" onclick='location.href=\"product.html?id=" + game.gameId + "\";'>\n" +
            "\n" +
            "        <div class=\"product-listed-image\">\n" +
            "        <img src=\"" + game.product.imageUrl + "\" alt=\"\">\n" +
            "        </div>\n" +
            "        <div class=\"product-listed-cover light_gradient shadow\">\n" +
            "\n" +
            "        <!-- Product-Title -->\n" +
            "        <p class=\"product-listed-title\">" + game.product.name + "</p>\n" +
            "        <!-- Genres -->\n" +
            "        <span>\n" +
            "        <p class=\"product-listed-genre\">\n" +
            "        " + game.genre.name + "\n" +
            "        </p>\n" +
            "        </span>\n" +
            "\n" +
            "        </div>\n" +
            "        </div>\n" +
            "        <!--- ////////////////////////////////// End of Half Card ////////////////////////////////// --->");

        if(isSecond)
        {
            columnNumber++;
        }
        isSecond = !isSecond;
        containerNumber++;
    }

    isLeft = !isLeft;
}