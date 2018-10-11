$('#deleteForm').on('submit',function(e){
  e.preventDefault();
  var gameId = $( "#deleteGameId" ).val();

  $.ajax({
    url: "https://gamersunited.azurewebsites.net/api/games/" + gameId,
    type: 'DELETE',
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