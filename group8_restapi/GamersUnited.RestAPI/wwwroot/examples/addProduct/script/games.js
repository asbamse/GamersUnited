$('#addForm').on('submit',function(e){
  e.preventDefault();
  var gameName = $( "#gameName" ).val();
  var gameCategory = $( "#gameCategory" ).val();
  var gameGenre = $( "#gameGenre" ).val();
  var gamePrice = $( "#gamePrice" ).val();
  var gameImage = $( "#gameImage" ).val();
  var gameDescription = $( "#gameDescription" ).val();

  $.ajax({
    url: "https://gamersunited.azurewebsites.net/api/games",
    type: 'POST',
    data: JSON.stringify({
		"Product":
		{
			"Name":gameName,
			"Category":{
				"Name": gameCategory
			},
			"Price":gamePrice,
			"ImageUrl":gameImage,
			"Description":gameDescription
		},
		"Genre":
		{
			"Name":gameGenre
		}
	}),
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