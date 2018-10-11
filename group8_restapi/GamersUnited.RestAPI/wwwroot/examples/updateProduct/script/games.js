$('#updateForm').on('submit',function(e){
  e.preventDefault();
  var gameId = $( "#updateGameDrop" ).val();
  var gameName = $( "#updateGameName" ).val();
  var gameCategoryDrop = $( "#updateGameCategoryDrop" ).val();
  var gameCategoryField = $( "#updateGameCategoryField" ).val();
  var gameGenre = $( "#updateGameGenre" ).val();
  var gameGenreDrop = $("#updateGameGenreDrop").val();
  var gamePrice = $( "#updateGamePrice" ).val();
  var gameImage = $( "#updateGameImage" ).val();
  var gameDescription = $( "#updateGameDescription" ).val();

  var json = {
      "Product":
          {
              "ProductId":gameId,
              "Name":gameName,
              "Price":gamePrice,
              "ImageUrl":gameImage,
              "Description":gameDescription
          }
  };

  if(gameCategoryField.length > 0)
  {
      json.Product.Category = {
          "Name":gameCategoryField
      };
  }
  else
  {
      json.Product.Category = {
          "ProductCategoryId":gameCategoryDrop
      };
  }

    if(gameGenre.length > 0)
    {
        json.Genre = {
            "Name":gameGenre
        };
    }
    else
    {
        json.Genre = {
            "GameGenreId":gameGenreDrop
        };
    }

  $.ajax({
    url: "https://gamersunited.azurewebsites.net/api/games/" + gameId,
    type: 'PUT',
    data: JSON.stringify(json),
    processData: false,
    contentType: 'application/json',
    success: function (comments) {
      alert("Success: " + comments);
    },
    error: function (request, message, error) {
      handleException(request, message, error);
    }
  });
});