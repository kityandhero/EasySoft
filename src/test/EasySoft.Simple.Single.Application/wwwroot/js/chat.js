"use strict"

const connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    const li = document.createElement("li");

    li.textContent = `${user}:${message}`;

    document.getElementById("messagesList").appendChild(li);
})

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (error) {
    return console.log(error);
})

document.getElementById("sendButton").addEventListener("click", function (event) {
    const user = document.getElementById("userInput").value;
    const message = document.getElementById("messageInput").value;

    connection.invoke("SendMessage", user, message).catch(function (error) {
        return console.log(error);
    })

    event.preventDefault();
})