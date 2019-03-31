const chat = $.connection.chatHub;
$.connection.hub.logging = true;


chat.client.receiveMessage = (model) => {
    console.log(model);
    var msg = model.Body.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");

    let divaki = document.createElement("div");
    divaki.className = "d-flex justify-content-start mb-4";

    let divoMinima = document.createElement("div");
    divoMinima.className = "msg_cotainer";
    divoMinima.textContent = msg;

    let dt = new Date();
    let time = dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();

    let spanoTime = document.createElement("span");
    spanoTime.className = "msg_time";
    spanoTime.innerHTML = time;
    divoMinima.appendChild(spanoTime);
    divoMinima.appendChild(spanoTime);
    divaki.appendChild(divoMinima);
    document.getElementById("messageBox").appendChild(divaki);
};

$.connection.hub.start().done(() =>
    console.log("client is here ConnectionId =", $.connection.chatHub));

document.addEventListener('keydown', (e) => {
    if (e.which === 13) {
        SendMessage();
        e.preventDefault();
        return false;
    }
});

async function SendMessage() {
    var audio = new Audio('/Content/sounds/notification.mp3');
    audio.play();

    var msg = document.getElementById("messageInput").value.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");

    let divaki = document.createElement("div");
    divaki.className = "d-flex justify-content-start mb-4";

    let divoMinima = document.createElement("div");
    divoMinima.className = "msg_cotainer_send";
    divoMinima.textContent = msg;

    let dt = new Date();
    let time = dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();

    let spanoTime = document.createElement("span");
    spanoTime.className = "msg_time_send";
    spanoTime.innerHTML = time;
    divoMinima.appendChild(spanoTime);
    divoMinima.appendChild(spanoTime);
    divaki.appendChild(divoMinima);
    document.getElementById("messageBox").appendChild(divaki);

    console.log(
    document.getElementById("senderName").innerHTML, document.getElementById("receiverName").innerHTML);
    chat.invoke("SendMessage", {
        senderId: document.getElementById("senderId").innerHTML,
        senderName: document.getElementById("senderName").innerHTML,
        receiverId: document.getElementById("receiverId").innerHTML,
        receiverName: document.getElementById("receiverName").innerHTML,
        // Get HTML tag with id = messageInput and read it's value - User's Message
        body: document.getElementById("messageInput").value,
        hasBeenRead: false
    });
}

chat.client.getProfileInfo = function (displayName, avatar) {
    model.myName(displayName);
    model.myAvatar(avatar);
};

