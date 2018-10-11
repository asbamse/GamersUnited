$('#addForm').on('submit',function(e){
  e.preventDefault();
  var gameName = $( "#gameName" ).val();
  var gameCategoryDrop = $( "#gameCategoryDrop" ).val();
  var gameCategoryField = $( "#gameCategoryField" ).val();
  var gameGenre = $( "#gameGenre" ).val();
  var gameGenreDrop = $("#gameGenreDrop").val();
  var gamePrice = $( "#gamePrice" ).val();
  var gameImage = $( "#gameImage" ).val();
  var gameDescription = $( "#gameDescription" ).val();

  var json = {
      "Product":
          {
              "Name":gameName,
              // "Category":{},
              "Price":gamePrice,
              "ImageUrl":gameImage,
              "Description":gameDescription
          },
     // "Genre":
          //{}
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
    url: "https://gamersunited.azurewebsites.net/api/games",
    type: 'POST',
    data: JSON.stringify(json),
    processData: false,
    contentType: 'application/json',
    success: function (comments) {
      console.log("Success");
    },
    error: function (request, message, error) {
      handleException(request, message, error);
    }
  });
});