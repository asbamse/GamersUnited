$( document ).ready(ufillProductCategoryDropDown());

function ufillProductCategoryDropDown() {
    // Call Web API to get a list of post
    $.ajax({
        url: 'https://gamersunited.azurewebsites.net/api/productCategories',
        type: 'GET',
        dataType: 'json',
        success: function (productCategories) {
            uonGetProductCategorySuccess(productCategories);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function uonGetProductCategorySuccess(productCategories) {
    // Iterate over the collection of data
    $.each(productCategories, function (index, productCategory) {
        // Add a row to the post table
        uaddProductCategoryOption(productCategory);
    });
}

function uaddProductCategoryOption(productCategory) {
    // Append row to <table>
    $("#updateGameCategoryDrop").append(
        ubuildProductCategoryOption(productCategory));
}

function ubuildProductCategoryOption(productCategory) {
    var ret =
        "<option value='" + productCategory.productCategoryId + "'>" + productCategory.name + "</option>";
    return ret;
}