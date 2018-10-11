$(function() {
    getGame();
});

function getGame() {
    var id = $_GET("id");
    // Call Web API to get a list of post
    $.ajax({
        url: 'https://gamersunited.azurewebsites.net/api/games/id/' + id,
        type: 'GET',
        dataType: 'json',
        success: function (game) {
            onGetGameSuccess(game);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function onGetGameSuccess(game) {
    document.title = game.product.name + " - Gamers United";
    $("#category").text(game.product.category.name);
    $("#product_page_title").text(game.product.name);
    $("#product_page_category").text(game.genre.name);
    $("#image").attr("src",game.product.imageUrl);
    $("#description").text(game.product.description);
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