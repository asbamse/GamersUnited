$('#deleteForm').on('submit',function(e){
  e.preventDefault();
  var gameId = $( "#gameId" ).val();

  $.ajax({
    url: "https://gamersunited.azurewebsites.net/api/games/" + gameId,
    type: 'DELETE',
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