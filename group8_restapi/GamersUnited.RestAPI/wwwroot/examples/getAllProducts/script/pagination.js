$( document ).ready(fillPaging());

function fillPaging() {
    // Call Web API to get a list of post
    $.ajax({
        url: 'https://gamersunited.azurewebsites.net/api/games/count',
        type: 'GET',
        dataType: 'json',
        success: function (gameCount) {
            onGetPagingSuccess(gameCount);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function onGetPagingSuccess(gameCount) {
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

    var start = 0;
    if(parseInt($page)-2 > 0)
    {
        if(Math.ceil(gameCount / parseFloat($limit))-2 < parseInt($page))
        {
            start = Math.ceil(gameCount / parseFloat($limit)) - 4;
        }
        else
        {
            start = parseInt($page)-2;
        }
    }
    else
    {
        start = 1;
    }

    var end = 0;
    if(3 < parseInt($page))
    {
        if(Math.ceil(gameCount / parseFloat($limit))<parseInt($page)+2)
        {
            end = Math.ceil(gameCount / parseFloat($limit));
        }
        else
        {
            end = parseInt($page)+2;
        }
    }
    else
    {
        end = 5;
    }

    // Iterate over the collection of data
    for (i = start; i <= end; i++) {
        if(i == parseInt($page)) {
            addPagingOption(true, i, $limit, $sortBy, $sortOrder);
        }
        else {
            addPagingOption(false, i, $limit, $sortBy, $sortOrder);
        }
    }
}

function addPagingOption(isActive, page, limit, sortBy, sortOrder) {
    // Append row to <table>
    $(".pagination").append(
        fillPagingDropDown(isActive, page, limit, sortBy, sortOrder));
}

function fillPagingDropDown(isActive, page, limit, sortBy, sortOrder) {
    var ret;
    if(isActive)
    {
        ret = "<a href='?page=" + page + "&limit=" + limit + sortBy + sortOrder + "' class='active'>" + page + "</a>";
    }
    else
    {
        ret = "<a href='?page=" + page + "&limit=" + limit + sortBy + sortOrder + "'>" + page + "</a>";
    }
    return ret;
}