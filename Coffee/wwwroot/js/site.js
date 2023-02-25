"use strict"

let connection = null;
setupConnection = () => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/coffeeHub")
        .build();

    connection.on("ReceiveOrderUpdate", (update) => {
        document.getElementById("status").innerHTML = update;
    });

    connection.on("NewOrder", function (order){
        document.getElementById("status").innerHTML = "someone ordered a " + order.product;
    });
    connection.on("Finished", function () {
        //connection.stop();
    })
}

setupConnection();

document.getElementById("submit").addEventListener("click", e=> {
    e.preventDefault();
    const product = document.getElementById("product").value;
    const size = document.getElementById("size").value;

    fetch("/Coffee", {
        method: "POST",
        body = JSON.stringify({ product, size }),
        headers: {
            "content-type": "application/json"
        }
    })
        .then(response => response.text())
        .Then(id => connection.invoke("GetUpdateForOrder", id));
});