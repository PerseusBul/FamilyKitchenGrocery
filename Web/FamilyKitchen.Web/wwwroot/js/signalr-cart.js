var connection =
    new signalR.HubConnectionBuilder()
        .withUrl("/carthub")
        .build();

var anchors = document.querySelectorAll(".ion-ios-menu");

for (let i = 0; i < anchors.length; i++) {
    anchors[i].addEventListener('click', (e) => {
        e.preventDefault();
        let icon = e.target;
        let id = icon.getAttribute("productId");
        connection.invoke("Add", parseInt(id));
    });
}

connection.on("RenderProduct", function (product) {
    
    let domProduct = `<tr class="text-center">
                                        <td class="image-prod">
                                            <img class="img-fluid" src="~/images/image_${product.id}.jpg" alt="">
                                        </td>

                                        <td class="product-name">
                                            <h3 id="productName">${product.name}</h3>
                                            <p>Far far away, behind the word mountains, far from the countries</p>
                                        </td>

                                        <td class="price" id="productSalePrice">${product.price}</td>

                                        <td class="quantity">
                                            <div class="input-group mb-3">
                                                <input type="text" name="quantity" class="quantity form-control input-number" id="productQuantity" value="1" min="1" max="100">
                                            </div>
                                        </td>
                                        <td class="total" id="cartTotal">${product.salePrice}</td>
                                    </tr>`
    let element = document.getElementById("tbodyCart");
    console.log(element);
    element.innerHTML += domProduct;
    console.log(element);
});

connection.start()
    .catch(err => console.error(err.toString()));

function escapeHtml(unsafe) {
    return unsafe
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;")
        .replace(/"/g, "&quot;")
        .replace(/'/g, "&#039;");
}