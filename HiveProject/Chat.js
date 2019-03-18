var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

document.getElementById("sendButton").disabled = true;
connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("ReceiveMessage",
    (model) => {
        var msg = model.body.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
        var encodedMsg = model.senderName + " says " + msg;
        var li = document.createElement("li");
        li.textContent = encodedMsg;
        document.getElementById("messagesList").appendChild(li);
    });

function SendMessage(userID, userName, receiversId, receiversName) {
    connection.invoke("SendMessage", {
        senderId: userID,
        senderName: userName,
        receiverId: receiversId,
        receiverName: receiversName,
        // Get HTML tag with id = messageInput and read it's value - User's Message
        body: document.getElementById("messageInput").value,
        hasBeenRead: false
    });

    // When user presses enter and there is text in input
    document.addEventListener('keydown', (e) => {
        if (e.which == 13) {
            SendMessage();
            return false;
        }
    });