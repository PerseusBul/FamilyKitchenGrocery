// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let connection = null;

setupConnection = () => {
    connection = new SignalR.HubConnectionBuilder()
        .withUrl("/hubRoute")
        .build();

    // Some examples

    connection.on("ReceiveOrderUpdate", (update) => {
        document.getElementById("status").innerHTML = update;
    });

    connection.on("NewOrder", function (order) {
        document.getElementById("status").innerHTML = "Someone order a " + order.product;
    });

    connection.on("Finished", function () {
        // connection.stop();
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
};

setupConnection();

document.getElementById("submit").addEventListener("click", e => {
    e.preventDefault();
    const product = document.getElementById("product").value;
    const size = document.getElementById("size").value;

    fetch("/Coffee",
        {
            method: "POST",
            body: JSON.stringify({ product, size }),
            headers: {
                'content-type': 'application/json'
            }
        })
        .then(response => response.text())
        .then(id => connection.invoke("GetUpdateForOrder", parseInt(id)));
});