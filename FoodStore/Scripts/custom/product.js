

var updateProduct = function () {
    var product = {
        "ID": $("#ID").val(),
        "Name": $("#Name").val(),
        "Description": $("#Description").val(),
        "Price": $("#Price").val(),
        "Perishble": $("#Perishble").val(),
        "OnHandQty": $("#OnHandQty").val()
    };
    
    $.ajax({
        type: "POST",
        url: "/product/edit/" + $("#ID").val(),
        data: JSON.stringify(product),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.message == "ok")
            {
                alert("Edit OK");
            }
        },

        error: function (e) {
            alert(e);
        }
    });
}