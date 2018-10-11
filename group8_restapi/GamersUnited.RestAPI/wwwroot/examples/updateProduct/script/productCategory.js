$( document ).ready(fillProductCategoryDropDown());

function fillProductCategoryDropDown() {
    // Call Web API to get a list of post
    $.ajax({
        url: 'https://gamersunited.azurewebsites.net/api/productCategories',
        type: 'GET',
        dataType: 'json',
        success: function (productCategories) {
            onGetProductCategorySuccess(productCategories);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function onGetProductCategorySuccess(productCategories) {
    // Iterate over the collection of data
    $.each(productCategories, function (index, productCategory) {
        // Add a row to the post table
        addProductCategoryOption(productCategory);
    });
}

function addProductCategoryOption(productCategory) {
    // Append row to <table>
    $("#gameCategoryDrop").append(
        buildProductCategoryOption(productCategory));
}

function buildProductCategoryOption(productCategory) {
    var ret =
        "<option value='" + productCategory.productCategoryId + "'>" + productCategory.name + "</option>";
    return ret;
}