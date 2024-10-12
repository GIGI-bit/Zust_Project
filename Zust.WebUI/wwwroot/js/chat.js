"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/chathub")
    .build();

connection.start().then(function () {
    GetAllUsers();
})
    .catch(function (err) {
        return console.error(err.toString());
    })


connection.on("Connect", function (info) {
    GetAllUsers();
    console.log("connected");
 
})

connection.on("Disconnect", function (info) {
    GetAllUsers();
})
connection.on("ReceiveNotification", function () {
    GetMyRequests();
    GetAllUsers();
});


async function SendFollowCall(id) {
    await connection.invoke("SendFollow", id);
}



